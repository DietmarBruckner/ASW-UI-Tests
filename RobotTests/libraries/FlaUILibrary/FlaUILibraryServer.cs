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
                        else {
                            response.StatusCode = 404;
                        }
                    }
                }
                else {
                    response.StatusCode = 405;
                }
                response.OutputStream.Close();
            }
            catch (Exception ex) { Console.WriteLine("HandleRequest error: " + ex.Message); }
        }

        private object ExecuteKeyword(string keyword, JObject args) {
            try {
                string A(string key, string def = null) => args?[key] != null ? (string)args[key] : def;
                int    Ai(string key, int def = 0)    => args?[key] != null ? (int)args[key] : def;
                bool   Ab(string key, bool def=false) => ParseBool(args?[key], def);

                switch (keyword.ToLowerInvariant().Replace("-", "_"))
                {
                    // IDE lifecycle
                    case "initialize_automation_studio":    return KwInitAS(Ai("timeout", 30));
                    case "close_application":               return KwCloseApp(Ab("save_changes", true));
                    case "invoke_menu":                     return KwInvokeMenu(A("menu_name"), A("menu_item"), A("submenu_item"));
                    case "wait_for_dialog":                 return KwWaitForDialog(A("dialog_title"), Ai("timeout",15));
                    case "type_into_field":                 return KwTypeIntoField(A("field_label"), A("text"));
                    case "type_slowly_into_field":          return KwTypeIntoField(A("field_label"), A("text"), slow:true);
                    case "set_field_value":                 return KwTypeIntoField(A("field_label"), A("value"), check:true);
                    case "click_dialog_button":             return KwClickDialogButton(A("button_name","OK"), A("dialog_title"), Ab("dialog_close", false));
//                    case "activate_tree_leaf":              return KwActivateTreeLeaf(A("tree_path"), Ab("double_click"));
                    case "select_from_combo_box":           return KwSelectFromComboBox(A("combo_label"), A("item_text"));
                    case "wait_for_idle":                   { _app?.WaitWhileBusy(TimeSpan.FromSeconds(Ai("timeout",30))); return Util.Util.Ok("idle"); }
                    case "wait_for_message":                return KwWaitForMessage(A("message"), Ai("timeout",30));
/*                     case "is_project_loaded":            return KwIsProjectLoaded();
                    case "get_window_title":             return new { result = _mainWindow?.Title ?? "" };
                    // Element finding
                    case "find_element":    return KwFindElement(A("identifier"), A("search_by","name"), Ai("timeout",10));
                    case "wait_for_element": return KwFindElement(A("identifier"), A("search_by","name"), Ai("timeout",10));
                    case "element_exists":   return new { result = Resolve(A("identifier"), A("search_by","name")) != null };
                    // Element interaction
                    case "click_element":        return KwClick(A("identifier"), A("search_by","name"), Ab("double_click"));
                    case "double_click_element": return KwClick(A("identifier"), A("search_by","name"), true);
                    case "right_click_element":  return KwRightClick(A("identifier"), A("search_by","name"));
                    case "hover_element":         return KwHover(A("identifier"), A("search_by","name"));
                    case "type_text":             return KwTypeText(A("text"));
                    case "get_text_from_element":  return KwGetText(A("identifier"), A("search_by","name"));
                    // Dialog
                    case "get_dialog_text":      return KwGetDialogText(A("field_label"), A("dialog_title"));
                    // Menu
                    case "open_context_menu":       return KwOpenContextMenu(A("identifier"), A("search_by","name"));
                    case "select_context_menu_item": return KwSelectContextMenuItem(A("menu_item"), A("submenu_item"));
                    // Tree
                    case "expand_tree_node":    return KwExpandCollapse(A("node_name"), true);
                    case "collapse_tree_node":  return KwExpandCollapse(A("node_name"), false);
                    // Property panel
                    case "set_property_value": return KwSetProperty(A("property_name"), A("value"));
                    case "get_property_value": return KwGetProperty(A("property_name"));
                    // Wait
                    case "wait_for_build":    return KwWaitStatus(Ai("timeout",60),  "Build", "Compil");
                    case "wait_for_transfer": return KwWaitStatus(Ai("timeout",120), "Transfer", "Download");
                    // Screenshot
                    case "take_screenshot":   return KwScreenshot(A("filename"), A("outputdir"));
 */                    default: return Util.Util.Err("Unknown keyword: " + keyword);
                }
            }
            catch (Exception ex) { return Util.Util.Err(ex.Message); }
        }

        // ── IDE lifecycle ────────────────────────────────────────────────────

        private object KwInitAS(int timeout) {
            string appPath = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\BR_AS_AS6_L001", "BuRSharedFilesPath", null) as string;
            if (string.IsNullOrEmpty(appPath)) return Util.Util.Err("Automation Studio 6 installation path not found in registry.");
            if ( _app != null) return Util.Util.Ok("already initialized", IDE_Main.MainWindow.Title);
            try {_app = Application.Attach(appPath + "\\bin-en\\pg.exe"); } catch { _app = Application.Launch(appPath + "\\bin-en\\pg.exe"); }
            if ( _app == null) return Util.Util.Err("Could not find or start Automation Studio 6 process.");
            _app.WaitWhileMainHandleIsMissing(TimeSpan.FromSeconds(timeout));
            _app.WaitWhileBusy(TimeSpan.FromSeconds(timeout));
            Ide_Main = new IDE_Main(_app, timeout);
            TreeConfig.IdeMain = Ide_Main;
            if (Ide_Main.IsProjectLoaded()) {
                Project = new AppProject(Ide_Main);
                TreeConfig.CurrentProject = Project;
                Project.LoadActiveProject();
            }
            return Util.Util.Ok("Automation Studio 6 initialized", IDE_Main.MainWindow.Title);
        }
        private object KwCloseApp(bool saveChanges) {
            if (_app == null) return Util.Util.Ok("Automation Studio 6 not running, nothing to close.");
            try {
                _app.Close();
                TryHandleSavePrompt(saveChanges);
            }
            catch { try { _app.Kill(); } catch { } }
            _app = null; Ide_Main = null; Project = null;
            return Util.Util.Ok("Automation Studio 6 closed");
        }
        private object KwInvokeMenu(string menuName, string menuItem, string submenuItem) {
            return Ide_Main.InvokeMenuItem(Ide_Main.GetMenu(menuName), menuItem, submenuItem);
        }
        private object KwWaitForMessage(string message, int timeout) {
            return Ide_Main.WaitForMessage(message, timeout);
        }
        private object KwWaitForDialog(string title, int timeout) {
            var w = GetModalWindow(title, timeout);
            if (w != null) {
                _modalWindows = _modalWindows ?? new List<Window>();
                if (!_modalWindows.Contains(w)) {
                    _modalWindows.Add(w);
                }
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
                if (_modalWindows != null) _modalWindows.Remove(dialog);
            }
            return Util.Util.Ok("clicked_button", buttonName);
        }
/*         private object KwActivateTreeLeaf(string treePath, bool doubleClick)
        {
            if (_projectExplorer == null) ResolveIDEPanes();
            var root = _projectExplorer ?? _mainWindow;
            var segments = treePath.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries);
            AutomationElement current = root;

            foreach (var segment in segments)
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                AutomationElement found = null;
                while (sw.Elapsed.TotalSeconds < 5)
                {
                    found = current.FindFirstDescendant(cf =>
                        cf.ByControlType(ControlType.TreeItem).And(cf.ByName(segment)));
                    if (found != null) break;
                    Thread.Sleep(200);
                }
                if (found == null) return Err($"Tree node not found: '{segment}' in path '{treePath}'");
                Mouse.Click(Center(found)); Thread.Sleep(300); current = found;
            }
            if (doubleClick) Mouse.DoubleClick(Center(current));
            return Ok("tree_leaf_activated", treePath);
        }
 */        private object KwSelectFromComboBox(string comboLabel, string text, Window window = null) {
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

        
        
        /*        private object KwIsProjectLoaded()
        {
            var tb = _mainWindow?.TitleBar;
            bool loaded = tb != null && !string.IsNullOrEmpty(tb.Name) &&
                          tb.Name.IndexOf("Automation Studio", StringComparison.OrdinalIgnoreCase) > 10;
            return new { result = loaded, title = tb?.Name ?? "" };
        }

        // ── Element finding ──────────────────────────────────────────────────

        private AutomationElement Resolve(string identifier, string searchBy)
        {
            if (_mainWindow == null) throw new InvalidOperationException("IDE not initialized");
            switch ((searchBy ?? "name").ToLowerInvariant())
            {
                case "id":
                case "automationid":
                    return _mainWindow.FindFirstDescendant(cf => cf.ByAutomationId(identifier));
                case "controltype":
                    Enum.TryParse<ControlType>(identifier, true, out var ct);
                    return _mainWindow.FindFirstDescendant(cf => cf.ByControlType(ct));
                default:
                    return _mainWindow.FindFirstDescendant(cf => cf.ByName(identifier));
            }
        }

        private object KwFindElement(string identifier, string searchBy, int timeout)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            AutomationElement el = null;
            while (sw.Elapsed.TotalSeconds < timeout)
            {
                el = Resolve(identifier, searchBy);
                if (el != null) break;
                Thread.Sleep(200);
            }
            return el != null
                ? (object)new { result = "found", name = el.Name, control_type = el.ControlType.ToString() }
                : Err($"Element not found: {identifier} (by {searchBy}) within {timeout}s");
        }

        // ── Interaction ──────────────────────────────────────────────────────

        private AutomationElement FindOrThrow(string identifier, string searchBy)
        {
            var el = Resolve(identifier, searchBy);
            if (el == null) throw new Exception($"Element not found: {identifier} (by {searchBy})");
            return el;
        }

        private object KwClick(string id, string by, bool dbl)
        {
            var el = FindOrThrow(id, by); var c = Center(el);
            if (dbl) Mouse.DoubleClick(c); else Mouse.Click(c);
            Thread.Sleep(200); return Ok(dbl ? "double_clicked" : "clicked", id);
        }

        private object KwRightClick(string id, string by)
        {
            Mouse.RightClick(Center(FindOrThrow(id, by)));
            Thread.Sleep(400); return Ok("right_clicked", id);
        }

        private object KwHover(string id, string by)
        {
            Mouse.MoveTo(Center(FindOrThrow(id, by)));
            Thread.Sleep(300); return Ok("hovered", id);
        }

        private object KwTypeText(string text)
        {
            if (text == null) return Err("text is required");
            Keyboard.Type(text); return Ok("typed", text);
        }


        private object KwGetText(string id, string by)
        {
            var el = FindOrThrow(id, by);
            return new { result = el.AsTextBox()?.Text ?? el.Name ?? "" };
        }

        // ── Dialog handling ──────────────────────────────────────────────────





        private object KwGetDialogText(string fieldLabel, string dialogTitle)
        {
            Window dialog = dialogTitle != null ? GetModalWindow(dialogTitle) : _mainWindow;
            if (dialog == null) return Err("Dialog not found: " + dialogTitle);
            var el = dialog.FindFirstDescendant(cf =>
                cf.ByControlType(ControlType.Edit).And(cf.ByName(fieldLabel)));
            return el != null ? (object)new { result = el.AsTextBox()?.Text ?? "" } : Err("Field not found: " + fieldLabel);
        }

        // ── Menu interaction ─────────────────────────────────────────────────


        private object KwOpenContextMenu(string id, string by)
        {
            Mouse.RightClick(Center(FindOrThrow(id, by))); Thread.Sleep(600);
            return Ok("context_menu_opened", id);
        }

        private object KwSelectContextMenuItem(string menuItem, string submenuItem)
        {
            var menu = _mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Menu));
            if (menu == null) return Err("No context menu visible");
            var item = menu.FindFirstChild(cf => cf.ByName(menuItem))
                ?? menu.FindFirstDescendant(cf => cf.ByName(menuItem));
            if (item == null) return Err("Context menu item not found: " + menuItem);
            item.AsMenuItem().Click(); Thread.Sleep(400);
            if (submenuItem == null) return Ok("context_item_clicked", menuItem);
            Thread.Sleep(400);
            var subItem = item.FindFirstDescendant(cf => cf.ByName(submenuItem));
            if (subItem == null) return Err("Submenu item not found: " + submenuItem);
            Mouse.MoveTo(Center(subItem)); subItem.AsMenuItem().Click(); Thread.Sleep(400);
            return Ok("context_submenu_clicked", submenuItem);
        }

        // ── Tree navigation ──────────────────────────────────────────────────

        private object KwExpandCollapse(string nodeName, bool expand)
        {
            var node = _mainWindow.FindFirstDescendant(cf =>
                cf.ByControlType(ControlType.TreeItem).And(cf.ByName(nodeName)));
            if (node == null) return Err("Tree node not found: " + nodeName);
            try
            {
                var ecp = node.Patterns.ExpandCollapse.PatternOrDefault;
                if (ecp != null)
                {
                    if (expand) ecp.Expand();
                    else ecp.Collapse();
                }
                else
                {
                    // Fallback: double-click to toggle
                    Mouse.DoubleClick(Center(node));
                }
            }
            catch { Mouse.DoubleClick(Center(node)); }
            Thread.Sleep(300); return Ok(expand ? "expanded" : "collapsed", nodeName);
        }

        // ── Property panel ───────────────────────────────────────────────────

        private object KwSetProperty(string propertyName, string value)
        {
            if (_propertyWindow == null) ResolveIDEPanes();
            var root = _propertyWindow ?? _mainWindow;
            var nameCell = root.FindFirstDescendant(cf => cf.ByName(propertyName));
            if (nameCell == null) return Err("Property not found: " + propertyName);
            var row = nameCell.Parent;
            var valueCell = row?.FindFirstChild(cf => cf.ByControlType(ControlType.Edit))
                ?? row?.FindFirstChild(cf => cf.ByControlType(ControlType.ComboBox));
            if (valueCell == null) return Err("Value cell not found for property: " + propertyName);
            Mouse.Click(Center(valueCell)); Thread.Sleep(200);
            Keyboard.TypeSimultaneously(new[] { FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A });
            Keyboard.Type(value);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.RETURN);
            Thread.Sleep(300); return Ok("property_set", propertyName);
        }

        private object KwGetProperty(string propertyName)
        {
            if (_propertyWindow == null) ResolveIDEPanes();
            var root = _propertyWindow ?? _mainWindow;
            var nameCell = root.FindFirstDescendant(cf => cf.ByName(propertyName));
            if (nameCell == null) return Err("Property not found: " + propertyName);
            var valueCell = nameCell.Parent?.FindFirstChild(cf => cf.ByControlType(ControlType.Edit));
            return new { result = valueCell?.AsTextBox()?.Text ?? "" };
        }

        // ── Status bar wait ──────────────────────────────────────────────────

        private object KwWaitStatus(int timeout, params string[] busyKeywords)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < timeout)
            {
                var sb = _mainWindow?.FindFirstChild(_cf.ByControlType(ControlType.StatusBar));
                var name = sb?.Name ?? "";
                if (!busyKeywords.Any(k => name.IndexOf(k, StringComparison.OrdinalIgnoreCase) >= 0))
                    return Ok("done");
                Thread.Sleep(1000);
            }
            return Ok("timeout_done");
        }

        // ── Screenshot ───────────────────────────────────────────────────────

        private object KwScreenshot(string filename, string outputDir)
        {
            if (_mainWindow == null) return Err("IDE not initialized");
            if (string.IsNullOrEmpty(filename))
                filename = $"screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            if (string.IsNullOrEmpty(outputDir))
                outputDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "resources");
            Directory.CreateDirectory(outputDir);
            var fullPath = Path.GetFullPath(Path.Combine(outputDir, filename));
            using (var bmp = _mainWindow.Capture())
            {
                bmp.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);
            }
            return new { result = "saved", path = fullPath };
        }
 */
        // ── Private helpers ──────────────────────────────────────────────────

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