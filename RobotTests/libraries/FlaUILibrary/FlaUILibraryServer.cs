using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.UIA2;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Keyboard = FlaUI.Core.Input.Keyboard;
using Mouse = FlaUI.Core.Input.Mouse;
using FlaUILibrary.Util;
using Microsoft.Win32;

namespace FlaUILibrary
{
    public class FlaUILibraryServer
    {
        private readonly HttpListener _listener;
        private Application _app;
        private List<Window> _modalWindows;
        private IDE_Main Ide_Main { get; set; }
        private AppProject Project { get; set; }

        // Crash-detection state
        private volatile bool _appCrashed;
        private string _crashDetail;
        private Thread _monitorThread;
        private volatile bool _intentionalStop;
        private int _appProcessId = -1;

        public FlaUILibraryServer(string prefix = "http://localhost:5000/") {
            _listener = new HttpListener();
            _listener.Prefixes.Add(prefix);
        }
        public void Start() {
            _listener.Start();
            Task.Run(() => ListenLoop());
            Console.WriteLine("Server listening on: " + string.Join(",", _listener.Prefixes));
        }
        public void Stop() {
            try { _listener.Stop(); } catch { }
        }
        private async Task ListenLoop() {
            while (_listener.IsListening) {
                try {
                    var context = await _listener.GetContextAsync();
                    _ = Task.Run(() => HandleRequest(context));
                }
                catch (HttpListenerException) { break; }
                catch (Exception ex) { Console.WriteLine("Listener loop error: " + ex.Message); }
            }
        }
        private void HandleRequest(HttpListenerContext context) {
            try {
                var request = context.Request;
                var response = context.Response;
                string path = request.Url.AbsolutePath.Trim('/');
                if (request.HttpMethod == "GET" && path.Equals("ping", StringComparison.OrdinalIgnoreCase)) {
                    var pingBytes = Encoding.UTF8.GetBytes("{\"result\":\"pong\"}");
                    response.ContentType = "application/json";
                    response.StatusCode = 200;
                    response.OutputStream.Write(pingBytes, 0, pingBytes.Length);
                    response.OutputStream.Close();
                    return;
                }
                if (request.HttpMethod == "POST") {
                    using (var sr = new StreamReader(request.InputStream, request.ContentEncoding)) {
                        var body = sr.ReadToEnd();
                        var json = string.IsNullOrEmpty(body) ? null : JObject.Parse(body);
                        if (path.StartsWith("keyword/")) {
                            var keyword = path.Substring("keyword/".Length);
                            var result = ExecuteKeyword(keyword, json);
                            var outBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                            response.ContentType = "application/json";
                            response.OutputStream.Write(outBytes, 0, outBytes.Length);
                            response.StatusCode = 200;
                        }
                        else { response.StatusCode = 404; }
                    }
                }
                else { response.StatusCode = 405; }
                response.OutputStream.Close();
            }
            catch (Exception ex) { Console.WriteLine("HandleRequest error: " + ex.Message); }
        }

        private object ExecuteKeyword(string keyword, JObject args) {
            if (_appCrashed)
                return Util.Util.Err("CRASH: " + _crashDetail);
            try {
                string A(string key, string def = null) => args?[key] != null ? (string)args[key] : def;
                int    Ai(string key, int def = 0)    => args?[key] != null ? (int)args[key] : def;
                bool   Ab(string key, bool def=false) => ParseBool(args?[key], def);

                switch (keyword.ToLowerInvariant().Replace("-", "_"))
                {
                    // IDE lifecycle
                    case "initialize_automation_studio":    return KwInitAS(Ai("timeout", 30), A("verbose"));
                    case "close_application":               return KwCloseApp(Ab("save_changes", true));
                    case "invoke_menu":                     return KwInvokeMenu(A("menu_name"), A("menu_item"), A("submenu_item"));
                    case "wait_for_dialog":                 return KwWaitForDialog(A("dialog_title"), Ai("timeout",15));
                    case "type_into_field":                 return KwTypeIntoField(A("field_label"), A("text"));
                    case "type_slowly_into_field":          return KwTypeIntoField(A("field_label"), A("text"), slow:true);
                    case "set_field_value":                 return KwTypeIntoField(A("field_label"), A("value"), check:true);
                    case "click_dialog_button":             return KwClickDialogButton(A("button_name","OK"), A("dialog_title"), Ab("dialog_close", false));
                    case "click_toolbar_button":            return KwClickToolbarButton(A("button_name"), Ab("activate", false));
                    case "activate_tree_leaf":              return KwActivateTreeLeaf(A("viewtype"), A("tree_path"), A("editorname"), A("rootname"), Ab("program", false), Ai("shortcut", -1), Ab("single_click", false), A("filename"), A("filetree"), A("version"));
                    case "select_from_combo_box":           return KwSelectFromComboBox(A("combo_label"), A("item_text"));
                    case "select_from_tree_combo_box":      return KwSelectFromTreeComboBox(A("item_label"), Ai("item_number", -1));
                    case "wait_for_idle":                   { _app?.WaitWhileBusy(TimeSpan.FromSeconds(Ai("timeout",30))); return Util.Util.Ok("idle"); }
                    case "wait_for_message":                return KwWaitForMessage(A("message"), Ai("timeout",30));
                    case "activate_simulation_mode":        return KwActivateSimulationMode();
                    case "click_ide":                       return KwClickIDE(Ab("editor", false), Ab("position", false), Ai("position_x",0), Ai("position_y",0));
                    case "select_component_version":        return KwSelectComponentVersion(A("component_name"), A("version"));
                    case "get_window_title":                return new { result = IDE_Main.MainWindow?.Title ?? "" };
                    case "insert_from_toolbox":             return KwInsertFromToolbox(A("view"), A("category"), A("component_name"), Ab("drag", false), Ai("xoffset", 0), Ai("yoffset", 0));
                    case "add_role":                        return KwAddRole(A("rolename"), Ab("add_role", true));
                    case "add_user":                        return KwAddUser(A("username"), A("password"), A("role"), Ab("add_user", true));
                    case "close_active_editor":             return KwCloseActiveEditor(Ab("save_changes", true));
                    case "switch_to_view":                  return KwSwitchToView(A("view_type"), Ai("sizeX",400), Ai("sizeY",400));
                    case "find_and_select_item":            return KwFindAndSelectItem(A("item_name"), Ab("doubleclick", false));
                    case "get_configtree_xpath":            return KwGetConfigTreeXpath();
                    case "get_editor_xpath":                return KwGetEditorXpath(A("editor_name"));
                    case "get_propertywindow_xpath":        return KwGetPropertyWindowXPath();
                    case "rename_editor":                   return KwRenameEditor(A("new_name"));
                    case "set_workspace_min_size":          return KwSetWorkspaceMinSize(A("editor_name"), Ab("percent", false));
                    case "select_from_mappview_dropdown":   return KwSelectFromMappViewDropDown(A("property_name"), A("subproperty"), A("value"));
                    case "is_project_loaded":            return KwIsProjectLoaded();
                    case "check_app_alive":              return KwCheckAppAlive();
                    case "get_dialog_text":      return KwGetDialogText(A("field_label"), A("dialog_title"));
                    case "open_context_menu":       return KwOpenContextMenu(A("identifier"), A("search_by","name"));
                    case "select_context_menu_item": return KwSelectContextMenuItem(A("menu_item"), A("submenu_item"));
                    case "set_property_value": return KwSetProperty(A("property_name"), A("value"));
                    case "get_property_value": return KwGetProperty(A("property_name"));
                    case "wait_for_build":    return KwWaitStatus(Ai("timeout",60),  "Build", "Compil");
                    case "wait_for_transfer": return KwWaitStatus(Ai("timeout",120), "Transfer", "Download");
                    case "take_screenshot":   return KwScreenshot(A("filename"), A("outputdir"));
                    default: return Util.Util.Err("Unknown keyword: " + keyword);
                }
            }
            catch (Exception ex) { return Util.Util.Err(ex.Message); }
        }

        // ── IDE lifecycle ────────────────────────────────────────────────────

        private object KwInitAS(int timeout, string verbose) {
            if (_app != null)
            {
                if (_appCrashed)
                    return Util.Util.Err("CRASH: " + _crashDetail + " – call close_application first.");
                return Util.Util.Ok("already initialized", IDE_Main.MainWindow.Title);
            }
            string appPath = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\BR_AS_AS6_L001", "BuRSharedFilesPath", null) as string;
            if (string.IsNullOrEmpty(appPath)) return Util.Util.Err("Automation Studio 6 installation path not found in registry.");
            try {_app = Application.Attach(appPath + "\\bin-en\\pg.exe"); } catch { _app = Application.Launch(appPath + "\\bin-en\\pg.exe"); }
            if ( _app == null) return Util.Util.Err("Could not find or start Automation Studio 6 process.");
            _app.WaitWhileMainHandleIsMissing(TimeSpan.FromSeconds(timeout));
            _app.WaitWhileBusy(TimeSpan.FromSeconds(timeout));
            Ide_Main = new IDE_Main(_app, timeout, appPath);
            TreeConfig.IdeMain = Ide_Main;
            if (Ide_Main.IsProjectLoaded()) {
                Project = new AppProject(Ide_Main);
                TreeConfig.CurrentProject = Project;
                Project.LoadActiveProject();
            }
            switch (verbose?.Trim().ToLowerInvariant()) {
                case "none":  Util.Util.Environment.verbose = Util.Util.Verbose.NONE; break;
                case "light": Util.Util.Environment.verbose = Util.Util.Verbose.LIGHT; break;
                case "steps": Util.Util.Environment.verbose = Util.Util.Verbose.STEPS; break;
                case "full":  Util.Util.Environment.verbose = Util.Util.Verbose.FULL; break;
                default:      Util.Util.Environment.verbose = Util.Util.Verbose.STEPS; break;
            }
            _appCrashed = false;
            _intentionalStop = false;
            _appProcessId = _app.ProcessId;
            StartCrashMonitoring();
            return Util.Util.Ok("Automation Studio 6 initialized", IDE_Main.MainWindow.Title);
        }
        private object KwCloseApp(bool saveChanges) {
            _intentionalStop = true;
            if (_app == null) return Util.Util.Ok("Automation Studio 6 not running, nothing to close.");
            try {
                _app.Close();
                TryHandleSavePrompt(saveChanges);
            }
            catch { try { _app.Kill(); } catch { } }
            _app = null; Ide_Main = null; Project = null; _appCrashed = false; _crashDetail = null;
            return Util.Util.Ok("Automation Studio 6 closed");
        }
        private object KwInvokeMenu(string menuName, string menuItem, string submenuItem) {
            return IDE_Main.InvokeMenuItem(IDE_Main.GetMenu(menuName), menuItem, submenuItem);
        }
        private object KwSetWorkspaceMinSize(string editorName, bool percent) {
            var editor = IDE_Main.ActiveEditor;
            if (editor == null) return Util.Util.Err("No active editor.");
            AutomationElement page_editor;
            while ((page_editor = editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document).And(cf.ByName(editorName)))) == null) 
                Thread.Sleep(500);
            IDE_Main.SetIWorkspaceMinSize(page_editor, percent);
            return Util.Util.Ok("Workspace minimum size set", editorName);
        }
        private object KwSelectFromMappViewDropDown(string propertyName, string subproperty, string value) {
            MappView.SelectFromMappViewDropDown(propertyName, subproperty, value);
            return Util.Util.Ok("Selected from MappView dropdown", value);
        }
        private object KwRenameEditor(string newName) {
            IDE_Main.ActiveEditor = IDE_Main.ActiveEditor.Rename(newName);
            return Util.Util.Ok("editor_renamed", newName);
        }
        private object KwSwitchToView(string viewType, int x, int y) {
            TreeConfig.ViewType vtype;
            switch (viewType.Trim().ToLowerInvariant()) {
                case "logical view":       vtype = TreeConfig.ViewType.LogicalView; break;
                case "configuration view": vtype = TreeConfig.ViewType.ConfigurationView; break;
                case "physical view":      vtype = TreeConfig.ViewType.PhysicalView; break;
                case "binding window":     vtype = TreeConfig.ViewType.BindingWindow; break;
                case "workspace":          vtype = TreeConfig.ViewType.Workspace; break;
                default: return Util.Util.Err("Unknown view type: " + viewType);
            }
            IDE_Main.SwitchView(vtype, x, y);
            return Util.Util.Ok("Switched to view", viewType);
        }
        private object KwWaitForMessage(string message, int timeout) {
            return Ide_Main.WaitForMessage(message, timeout);
        }
        private object KwWaitForDialog(string title, int timeout) {
            var w = GetModalWindow(title, timeout);
            if (w != null) {
                _modalWindows = _modalWindows ?? new List<Window>();
                if (!_modalWindows.Contains(w))
                    _modalWindows.Add(w);
            }
            return w != null
                ? Util.Util.Ok("found", w.Title)
                : new { result = "not_found", detail = $"Dialog not found: {title} within {timeout}s" };
        }
        private Window GetModalWindow(string title, int timeout = 15) {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < timeout) {
                var w = IDE_Main.MainWindow?.ModalWindows?.FirstOrDefault(x => x.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0);
                if (w != null) {
                    IDE_Main.CheckResizeWindowWithinScreen(w);
                    return w;
                }
                Thread.Sleep(500);
            }
            return null;
        }
        private object KwTypeIntoField(string fieldLabel, string text, Window window = null, bool check = false, bool slow = false) {
            var source = window ?? (_modalWindows?.LastOrDefault()) ?? IDE_Main.MainWindow;
            if (source == null) return Util.Util.Err("No active window to type into.");
            var el = source.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByName(fieldLabel)))
                ?? source.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId(fieldLabel)));
            if (el == null) return Util.Util.Err("Field not found: " + fieldLabel);
            if (check && el.AsTextBox()?.Text == text) return Util.Util.Ok("already_has_value", fieldLabel);
            Mouse.Click(Center(el));
            Keyboard.TypeSimultaneously(new[] { FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A });
            if (slow) {
                foreach (char ch in text) {
                    Keyboard.Type(ch);
                    Thread.Sleep(500);
                }
            }
            else
                Keyboard.Type(text);
            return Util.Util.Ok("typed_into_field", fieldLabel);
        }
        private static Point Center(AutomationElement el) {
            var r = el.BoundingRectangle;
            return new Point(r.Left + r.Width / 2, r.Top + r.Height / 2);
        }
        private object KwClickDialogButton(string buttonName, string dialogTitle, bool dialogClose) {
            Window dialog = dialogTitle != null ? GetModalWindow(dialogTitle) : (_modalWindows?.LastOrDefault()) ?? IDE_Main.MainWindow;
            if (dialog == null) return Util.Util.Err("Dialog not found: " + dialogTitle);
            var btn  = dialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName(buttonName)))
                    ?? dialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName(buttonName + " >")))
                    ?? dialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByAutomationId(buttonName + " >")))
                    ?? dialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByAutomationId(buttonName + " >")));
            if (btn == null) return Util.Util.Err($"Button '{buttonName}' not found");
            btn.AsButton().Invoke(); Thread.Sleep(500);
            if (dialogClose) {
                IDE_Main.LooseModalWindow(dialog);
                _modalWindows?.Remove(dialog);
                TreeConfig.ClickAutomationElement(IDE_Main.MainWindow.TitleBar);
            }
            return Util.Util.Ok("button_clicked", buttonName);
        }
        private object KwFindAndSelectItem(string itemName, bool doubleclick = false) {
            Window dialog = _modalWindows?.Last() ?? IDE_Main.MainWindow;
            var item = dialog.FindAllDescendants().FirstOrDefault(cf => cf.Name.IndexOf(itemName, StringComparison.OrdinalIgnoreCase) >= 0) 
            ?? dialog.FindAllDescendants().FirstOrDefault(cf => cf.AutomationId.IndexOf(itemName, StringComparison.OrdinalIgnoreCase) >= 0);
            if (item == null) return Util.Util.Err("Item not found: " + itemName);
            TreeConfig.ClickAutomationElement(item, doubleClick: doubleclick);
            return Util.Util.Ok("item_selected", itemName);
        }
        private object KwSelectComponentVersion(string componentName, string version) {
            return Ide_Main.SelectComponentVersion(_modalWindows.Last(), componentName, version);
        }
        private object KwActivateTreeLeaf(string viewType, string treePath, string editorName, string rootName, bool program, int shortcut, bool singleClick, string filename, string filetree, string version) {
            AutomationElement ConfigRoot = null;
            string file; List<string> fileTreePath = null; string [] segments = null;
            if (filename != null) {
                switch (filename) {
                    case "OPCUACS": file = Util.Util.Environment.InstallationPath + Util.Util.Environment.EditorPathOPCUACS + version + "\\Editors\\" + "uacfg.xml"; break;
                    case "OPCUACSCONF": file = Util.Util.Environment.InstallationPath + Util.Util.Environment.EditorPathOPCUACS + version + "\\Editors\\" + "uadcfg.xml"; break;
                    case "TEXTSYSTEM": file = Util.Util.Environment.InstallationPath + Util.Util.Environment.EditorPathTS + "TextConfig.xml"; break;
                    case "MAPPVIEW": file = Util.Util.Environment.InstallationPath + Util.Util.Environment.EditorPathMV + version + "\\Editors\\" + "mappviewcfg.xml"; break;
                    default : file = null; break;
                }
                string [] s = filetree.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                fileTreePath = TreeConfig.FindXMLPath(file, s[0], s.Skip(1).ToArray());
            }
            else
                segments = treePath?.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            List<string> ls = (filename != null) ? fileTreePath : segments?.ToList();
            TreeConfig.ViewType vtype;
            switch (viewType) {
                case "Logical View":         vtype = TreeConfig.ViewType.LogicalView; break;
                case "Configuration View":   vtype = TreeConfig.ViewType.ConfigurationView; break;
                case "Binding Window":       vtype = TreeConfig.ViewType.BindingWindow; break;
                case "Workspace":            vtype = TreeConfig.ViewType.Workspace; break;
                default:                     vtype = TreeConfig.ViewType.LogicalView; break;
            }
            if (rootName != null)
                ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(IDE_Main.ActiveEditor, rootName);
            if (editorName != "e")
                TreeConfig.ActivateTreeLeaf(vtype, ls, out IDE_Main.ActiveEditor, root:ConfigRoot, shortcut: shortcut);
            else
                TreeConfig.ActivateTreeLeaf(vtype, ls, out var e, root:ConfigRoot, shortcut: shortcut);

            return Util.Util.Ok("tree_leaf_activated", treePath);
        }
        private object KwSelectFromComboBox(string comboLabel, string text, Window window = null) {
            var source = window ?? (_modalWindows?.LastOrDefault()) ?? IDE_Main.MainWindow;
            if (source == null) return Util.Util.Err("No active window to select from.");
            var el = source.FindFirstDescendant(cf => cf.ByControlType(ControlType.ComboBox).And(cf.ByName(comboLabel)))
                ?? source.FindFirstDescendant(cf => cf.ByControlType(ControlType.ComboBox).And(cf.ByAutomationId(comboLabel)));
            if (el == null) return Util.Util.Err("Combobox not found: " + comboLabel);
            if (el.AsComboBox().Value != text) {
                Mouse.Click(Center(el));
                Thread.Sleep(500);
                TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, text);
            }
            return Util.Util.Ok("typed_into_combobox", comboLabel);
        }
        private object KwSelectFromTreeComboBox(string label=null, int number=-1) {
            if (label == null && number < 0) return Util.Util.Err("Either item_label or item_number must be provided.");
            if (label != null) 
                TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, label);
            else
                TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, number);
            return Util.Util.Ok("selected_from_tree_combobox", label ?? number.ToString());
        }
        private object KwActivateSimulationMode() {
            return IDE_Main.ActivateSimulation();
        }
        private object KwClickIDE(bool editor = false, bool position = false, int position_x = 0, int position_y = 0) {
            if (position)
                Mouse.Click();
            else {
                if (position_x != 0 || position_y != 0)
                    Mouse.Click(new Point(position_x, position_y));
                else {
                    if (editor == false)
                        TreeConfig.ClickAutomationElement(IDE_Main.MainWindow.TitleBar);
                    else if (IDE_Main.ActiveEditor != null)
                        TreeConfig.ClickAutomationElement(IDE_Main.ActiveEditor.ConfigWorkspace);
                    else
                        return Util.Util.Err("No active editor to click on.");
                }
            }
            return Util.Util.Ok("clicked_IDE", editor ? "editor" : position ? "current position" : "titlebar");
        }
        private object KwAddRole(string name, bool addRole) {
            IDE_Main.AddRole(name, addRole);
            return Util.Util.Ok("role_added", name);
        }
        private object KwAddUser(string name, string password, string role, bool addUser) {
            IDE_Main.AddUser(name, password, role, addUser);
            return Util.Util.Ok("user_added", name);
        }
        private object KwGetConfigTreeXpath() {
            var editor = IDE_Main.ActiveEditor;
            if (editor == null) return Util.Util.Err("No active editor.");
            var tree = editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            if (tree == null) return Util.Util.Err("Configuration tree not found in active editor.");

            bool reachedMainWindow;
            var xpath = BuildControlTypeXPath(tree, IDE_Main.MainWindow, out reachedMainWindow);
            if (!reachedMainWindow)
                return Util.Util.Err("Could not build XPath to main window from configuration tree.");

            return new { result = xpath };
        }
        private object KwGetEditorXpath(string editor_name) {
            var editor = IDE_Main.ActiveEditor;
            if (editor == null) return Util.Util.Err("No active editor.");
            AutomationElement iateditor;
            while ((iateditor = editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document))) == null) 
                Thread.Sleep(500);
            if (iateditor.Name != editor_name) return Util.Util.Err("Editor not found in active editor.");

            bool reachedMainWindow;
            var xpath = BuildControlTypeXPath(iateditor, IDE_Main.MainWindow, out reachedMainWindow);
            if (!reachedMainWindow)
                return Util.Util.Err("Could not build XPath to main window from editor.");

            return new { result = xpath };
        }
        private object KwGetPropertyWindowXPath() {
            Ide_Main.InitializeViews(propertyWindow: true);
            var pw = IDE_Main.PropertyWindow;
            if (pw == null) return Util.Util.Err("Property Window not found.");
            var table = pw.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            if (table == null) return Util.Util.Err("Property table not found inside Property Window.");

            bool reachedMainWindow;
            var xpath = BuildControlTypeXPath(table, IDE_Main.MainWindow, out reachedMainWindow);
            if (!reachedMainWindow)
                return Util.Util.Err("Could not build XPath to main window from property table.");

            return new { result = xpath };
        }
        private object KwCloseActiveEditor(bool saveChanges) {
            if (IDE_Main.ActiveEditor == null) return Util.Util.Err("No active editor to close.");
            if (saveChanges)
                Ide_Main.Save();
            IDE_Main.ActiveEditor.Close();
            return Util.Util.Ok("active_editor_closed");
        }

        private object KwClickToolbarButton(string buttonName, bool activate) {
            var found = IDE_Main.ToolbarButtons.TryGetValue(buttonName, out var toolbar) ? toolbar : null;
            if (toolbar == null) toolbar = TreeConfig.IdeMain.GetWorkspaceToolbar(IDE_Main.ActiveEditor);
            if (toolbar == null) return Util.Util.Err("Toolbar not found for: " + buttonName);
            var btn = toolbar.FindAllDescendants(cf => cf.ByControlType(ControlType.Button)).FirstOrDefault(cf => cf.Name.IndexOf(IDE_Main.SanitizeButtonNames(buttonName)) >= 0).AsButton();
            if (btn == null) return Util.Util.Err($"Button '{buttonName}' not found");
            if (!activate || (activate && !IDE_Main.IsButtonActive(btn))) {
                Mouse.Click(Center(btn));
                Thread.Sleep(500);
            }
            TreeConfig.ClickAutomationElement(IDE_Main.MainWindow.TitleBar);
            return Util.Util.Ok(activate ? "button_activated" : "button_clicked", buttonName);
        }
        private object KwIsProjectLoaded() {
            return new { result = Ide_Main != null && Ide_Main.IsProjectLoaded() };
        }
        private object KwInsertFromToolbox(string view, string category, string componentName, bool drag, int xoffset, int yoffset) {
            TreeConfig.ViewType vtype;
            switch (view) {
                case "Logical View":         vtype = TreeConfig.ViewType.LogicalView; break;
                case "Configuration View":   vtype = TreeConfig.ViewType.ConfigurationView; break;
                case "Binding Window":       vtype = TreeConfig.ViewType.BindingWindow; break;
                case "Workspace":            vtype = TreeConfig.ViewType.Workspace; break;
                default:                     vtype = TreeConfig.ViewType.LogicalView; break;
            }
            string cat = category ?? "";
            Point point = new Point();
            if (drag) {
                if (xoffset == 0 && yoffset == 0)
                point = IDE_Main.ActiveEditor.ConfigWorkspace.BoundingRectangle.Center();
            }
            else
                point = new Point {X = xoffset, Y = yoffset};
            TreeConfig.IdeMain.InsertObjectFromToolBox(vtype, cat, componentName, drag, point);
            return Util.Util.Ok("inserted_from_toolbox", componentName);
        }
        private object KwGetDialogText(string fieldLabel, string dialogTitle)
        {
            var dialog = dialogTitle != null ? GetModalWindow(dialogTitle) : (_modalWindows?.LastOrDefault() ?? IDE_Main.MainWindow);
            if (dialog == null) return Util.Util.Err("Dialog not found: " + dialogTitle);
            var el = dialog.FindFirstDescendant(cf =>
                cf.ByControlType(ControlType.Edit).And(cf.ByName(fieldLabel)));
            return el != null ? (object)new { result = el.AsTextBox()?.Text ?? "" } : Util.Util.Err("Field not found: " + fieldLabel);
        }

        private object KwOpenContextMenu(string id, string by)
        {
            var element = ResolveElement(id, by);
            if (element == null)
                return Util.Util.Err("Element not found: " + id + " (by " + by + ")");

            Mouse.RightClick(Center(element));
            Thread.Sleep(600);
            return Util.Util.Ok("context_menu_opened", id);
        }

        private object KwSelectContextMenuItem(string menuItem, string submenuItem)
        {
            TreeConfig.ClickContextMenuItem(IDE_Main.MainWindow, menuItem, submenuItem);
            return Util.Util.Ok(submenuItem == null ? "context_item_clicked" : "context_submenu_clicked", submenuItem ?? menuItem);
        }

        private object KwSetProperty(string propertyName, string value)
        {
            Ide_Main.InitializeViews(propertyWindow: true);
            var root = IDE_Main.PropertyWindow ?? IDE_Main.MainWindow;
            var nameCell = root.FindFirstDescendant(cf => cf.ByName(propertyName));
            if (nameCell == null) return Util.Util.Err("Property not found: " + propertyName);
            var row = nameCell.Parent;
            var valueCell = row?.FindFirstChild(cf => cf.ByControlType(ControlType.Edit))
                ?? row?.FindFirstChild(cf => cf.ByControlType(ControlType.ComboBox));
            if (valueCell == null) return Util.Util.Err("Value cell not found for property: " + propertyName);

            Mouse.Click(Center(valueCell));
            Thread.Sleep(200);
            if (valueCell.ControlType == ControlType.ComboBox) {
                TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, value);
            }
            else {
                Keyboard.TypeSimultaneously(new[] { FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A });
                Keyboard.Type(value);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.RETURN);
            }

            Thread.Sleep(300);
            return Util.Util.Ok("property_set", propertyName);
        }

        private object KwGetProperty(string propertyName)
        {
            Ide_Main.InitializeViews(propertyWindow: true);
            var root = IDE_Main.PropertyWindow ?? IDE_Main.MainWindow;
            var nameCell = root.FindFirstDescendant(cf => cf.ByName(propertyName));
            if (nameCell == null) return Util.Util.Err("Property not found: " + propertyName);
            var row = nameCell.Parent;
            var editValue = row?.FindFirstChild(cf => cf.ByControlType(ControlType.Edit));
            if (editValue != null)
                return new { result = editValue.AsTextBox()?.Text ?? "" };

            var comboValue = row?.FindFirstChild(cf => cf.ByControlType(ControlType.ComboBox));
            return new { result = comboValue?.Name ?? comboValue?.Patterns.Value.PatternOrDefault?.Value ?? "" };
        }

        private object KwWaitStatus(int timeout, params string[] busyKeywords)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < timeout)
            {
                var sb = IDE_Main.MainWindow?.FindFirstChild(cf => cf.ByControlType(ControlType.StatusBar));
                var name = sb?.Name ?? "";
                if (!busyKeywords.Any(k => name.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0))
                    return Util.Util.Ok("done");
                Thread.Sleep(1000);
            }
            return Util.Util.Ok("timeout_done");
        }

        private object KwScreenshot(string filename, string outputDir)
        {
            if (IDE_Main.MainWindow == null) return Util.Util.Err("IDE not initialized");
            if (string.IsNullOrEmpty(filename))
                filename = $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            if (string.IsNullOrEmpty(outputDir))
                outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "resources");
            Directory.CreateDirectory(outputDir);
            var fullPath = Path.GetFullPath(Path.Combine(outputDir, filename));
            using (var bmp = IDE_Main.MainWindow.Capture())
            {
                bmp.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
            }
            return new { result = "saved", path = fullPath };
        }

        // ── Crash monitoring ─────────────────────────────────────────────────

        /// <summary>
        /// Starts a background thread that polls the Automation Studio process every 3 s.
        /// Also subscribes to the Process.Exited event for immediate notification.
        /// Sets _appCrashed when an unexpected exit is detected.
        /// </summary>
        private void StartCrashMonitoring()
        {
            if (_appProcessId < 0) return;

            // Immediate notification via Process.Exited event
            try
            {
                var proc = System.Diagnostics.Process.GetProcessById(_appProcessId);
                proc.EnableRaisingEvents = true;
                proc.Exited += (sender, e) =>
                {
                    if (_intentionalStop) return;
                    int code = -1;
                    try { code = proc.ExitCode; } catch { }
                    _appCrashed = true;
                    _crashDetail = $"Automation Studio crashed (exit code: {code})";
                    Console.WriteLine("[CRASH MONITOR] " + _crashDetail);
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine("[CRASH MONITOR] Could not subscribe to process exit event: " + ex.Message);
            }

            // Polling thread as defensive fallback
            _monitorThread = new Thread(() =>
            {
                while (!_intentionalStop)
                {
                    Thread.Sleep(3000);
                    if (_intentionalStop) break;
                    try
                    {
                        var proc = System.Diagnostics.Process.GetProcessById(_appProcessId);
                        if (proc.HasExited && !_intentionalStop)
                        {
                            _appCrashed = true;
                            _crashDetail = $"Automation Studio process (PID {_appProcessId}) has exited";
                            Console.WriteLine("[CRASH MONITOR] " + _crashDetail);
                            break;
                        }
                    }
                    catch (ArgumentException)
                    {
                        // ArgumentException means the PID no longer exists in the OS
                        if (!_intentionalStop)
                        {
                            _appCrashed = true;
                            _crashDetail = $"Automation Studio process (PID {_appProcessId}) is no longer running";
                            Console.WriteLine("[CRASH MONITOR] " + _crashDetail);
                        }
                        break;
                    }
                    catch { /* transient OS errors – continue polling */ }
                }
            })
            {
                IsBackground = true,
                Name = "AppCrashMonitor"
            };
            _monitorThread.Start();
        }

        /// <summary>
        /// Keyword: check_app_alive
        /// Returns {result:"alive"}, {result:"crashed", detail:"..."}, or {result:"not_initialized"}.
        /// Intended for use in RF suite teardowns or long-running keyword loops.
        /// </summary>
        private object KwCheckAppAlive()
        {
            if (_app == null)
                return new { result = "not_initialized" };
            if (_appCrashed)
                return new { result = "crashed", detail = _crashDetail };
            try
            {
                var proc = System.Diagnostics.Process.GetProcessById(_appProcessId);
                if (proc.HasExited)
                {
                    _appCrashed = true;
                    _crashDetail = $"Automation Studio process (PID {_appProcessId}) has exited";
                    return new { result = "crashed", detail = _crashDetail };
                }
                return new { result = "alive" };
            }
            catch (ArgumentException)
            {
                _appCrashed = true;
                _crashDetail = $"Automation Studio process (PID {_appProcessId}) is no longer running";
                return new { result = "crashed", detail = _crashDetail };
            }
            catch (Exception ex)
            {
                return new { result = "unknown", detail = ex.Message };
            }
        }

        private AutomationElement ResolveElement(string identifier, string searchBy) {
            var root = (_modalWindows != null && _modalWindows.Count > 0)
                ? (AutomationElement)_modalWindows.Last()
                : (AutomationElement)IDE_Main.MainWindow;

            switch ((searchBy ?? "name").ToLowerInvariant()) {
                case "id":
                case "automationid":
                    return root.FindFirstDescendant(cf => cf.ByAutomationId(identifier));
                case "controltype":
                    ControlType controlType;
                    if (Enum.TryParse(identifier, true, out controlType))
                        return root.FindFirstDescendant(cf => cf.ByControlType(controlType));
                    return null;
                default:
                    return root.FindFirstDescendant(cf => cf.ByName(identifier));
            }
        }

        // ── Private helpers ──────────────────────────────────────────────────

        private static string BuildControlTypeXPath(AutomationElement element, AutomationElement root, out bool reachedRoot) {
            if (element == null) {
                reachedRoot = false;
                return string.Empty;
            }
            var segment = BuildControlTypeSegment(element);
            if (root != null && AreSameElement(element, root)) {
                reachedRoot = true;
                return "/" + segment;
            }
            var parentPath = BuildControlTypeXPath(element.Parent, root, out reachedRoot);
            if (string.IsNullOrEmpty(parentPath))
                return "/" + segment;
            return parentPath + "/" + segment;
        }
        private static string BuildControlTypeSegment(AutomationElement element) {
            var segment = element.ControlType.ToString();
            var parent = element.Parent;
            if (parent == null)
                return segment;
            if (element.Name == "Chrome Legacy Window")
                return "";
            var sameTypeSiblings = parent.FindAllChildren().Where(child => child.ControlType == element.ControlType).ToList();
            // it happens that element is not found under parent.FindAllChildren(); otherwise == 1 would suffice
            if (sameTypeSiblings.Count <= 1)
                return segment;
            var index = sameTypeSiblings.IndexOf(element);
            //FindIndex(child => AreSameElement(child, element));
            return segment + "[" + (index + 1).ToString() + "]";
        }
        private static bool AreSameElement(AutomationElement left, AutomationElement right) {
            if (left == null || right == null)
                return false;
            bool hasLeftId = left.Properties.RuntimeId.TryGetValue(out int[] leftRuntimeId) && leftRuntimeId != null;
            bool hasRightId = right.Properties.RuntimeId.TryGetValue(out int[] rightRuntimeId) && rightRuntimeId != null;
            if (hasLeftId && hasRightId)
                return leftRuntimeId.SequenceEqual(rightRuntimeId);
            return ReferenceEquals(left, right);
        }

        private static bool ParseBool(JToken token, bool fallback)
        {
            if (token == null)
                return fallback;

            if (token.Type == JTokenType.Boolean)
                return token.Value<bool>();

            if (token.Type == JTokenType.Integer)
                return token.Value<int>() != 0;

            if (token.Type == JTokenType.String)
            {
                var value = token.Value<string>()?.Trim().ToLowerInvariant();
                if (value == null)
                    return fallback;

                if (value == "true" || value == "1" || value == "yes" || value == "y" || value == "on")
                    return true;
                if (value == "false" || value == "0" || value == "no" || value == "n" || value == "off")
                    return false;
            }

            return fallback;
        }
        private void TryHandleSavePrompt(bool saveChanges)
        {
            if (IDE_Main.MainWindow == null)
                return;

            Thread.Sleep(300);
            Window modal = null;
            try
            {
                modal = IDE_Main.MainWindow.ModalWindows.FirstOrDefault();
            }
            catch
            {
                return;
            }

            if (modal == null)
                return;

            var candidates = saveChanges
                ? new[] { "Save", "Yes", "&Save", "&Yes" }
                : new[] { "Don't Save", "Do&n't Save", "No", "&No", "Discard" };

            foreach (var caption in candidates)
            {
                var button = modal.FindFirstDescendant(cf =>
                    cf.ByControlType(ControlType.Button).And(cf.ByName(caption)));
                if (button == null)
                    continue;

                try
                {
                    button.AsButton().Invoke();
                    Thread.Sleep(200);
                    return;
                }
                catch
                {
                    // Try next matching button caption.
                }
            }
        }
    }
}