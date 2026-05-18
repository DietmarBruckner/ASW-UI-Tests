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

namespace FlaUILibrary
{
    public class FlaUILibraryServer
    {
        private readonly HttpListener _listener;
        private Application _app;
        private UIA2Automation _automation;
        private Window _mainWindow;
        private ConditionFactory _cf;
        // IDE pane references (mirrors IDE_Main.cs)
        private AutomationElement _projectExplorer;
        private AutomationElement _workspace;
        private AutomationElement _toolbox;
        private AutomationElement _propertyWindow;

        public FlaUILibraryServer(string prefix = "http://localhost:5000/")
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add(prefix);
        }

        public void Start()
        {
            _listener.Start();
            Task.Run(() => ListenLoop());
            Console.WriteLine("Server listening on: " + string.Join(",", _listener.Prefixes));
        }

        public void Stop()
        {
            try
            {
                _listener.Stop();
            }
            catch { }
        }

        private async Task ListenLoop()
        {
            while (_listener.IsListening)
            {
                try
                {
                    var context = await _listener.GetContextAsync();
                    _ = Task.Run(() => HandleRequest(context));
                }
                catch (HttpListenerException) { break; }
                catch (Exception ex)
                {
                    Console.WriteLine("Listener loop error: " + ex.Message);
                }
            }
        }

        private void HandleRequest(HttpListenerContext context)
        {
            try
            {
                var request = context.Request;
                var response = context.Response;
                string path = request.Url.AbsolutePath.Trim('/');

                if (request.HttpMethod == "POST")
                {
                    using (var sr = new StreamReader(request.InputStream, request.ContentEncoding))
                    {
                        var body = sr.ReadToEnd();
                        var json = string.IsNullOrEmpty(body) ? null : JObject.Parse(body);

                        if (path.StartsWith("keyword/"))
                        {
                            var keyword = path.Substring("keyword/".Length);
                            var result = ExecuteKeyword(keyword, json);
                            var outBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(result));
                            response.ContentType = "application/json";
                            response.OutputStream.Write(outBytes, 0, outBytes.Length);
                            response.StatusCode = 200;
                        }
                        else
                        {
                            response.StatusCode = 404;
                        }
                    }
                }
                else
                {
                    response.StatusCode = 405;
                }

                response.OutputStream.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("HandleRequest error: " + ex.Message);
            }
        }

        private object ExecuteKeyword(string keyword, JObject args)
        {
            try
            {
                string A(string key, string def = null) => args?[key] != null ? (string)args[key] : def;
                int    Ai(string key, int def = 0)    => args?[key] != null ? (int)args[key] : def;
                bool   Ab(string key, bool def=false) => args?[key] != null ? (bool)args[key] : def;

                switch (keyword.ToLowerInvariant().Replace("-", "_"))
                {
                    // IDE lifecycle
                    case "initialize_automation_studio": return KwInitAS(A("app_path"), Ai("timeout", 30));
                    case "close_application":            return KwCloseApp(Ab("save_changes", true));
                    case "is_project_loaded":            return KwIsProjectLoaded();
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
                    case "type_into_field":       return KwTypeIntoField(A("field_label"), A("text"));
                    case "set_field_value":        return KwTypeIntoField(A("field_label"), A("value"));
                    case "get_text_from_element":  return KwGetText(A("identifier"), A("search_by","name"));
                    // Dialog
                    case "click_dialog_button": return KwClickDialogButton(A("button_name","OK"), A("dialog_title"));
                    case "wait_for_dialog":      return KwWaitForDialog(A("dialog_title"), Ai("timeout",15));
                    case "get_dialog_text":      return KwGetDialogText(A("field_label"), A("dialog_title"));
                    // Menu
                    case "invoke_menu":             return KwInvokeMenu(A("menu_name"), A("menu_item"), A("submenu_item"));
                    case "open_context_menu":       return KwOpenContextMenu(A("identifier"), A("search_by","name"));
                    case "select_context_menu_item": return KwSelectContextMenuItem(A("menu_item"), A("submenu_item"));
                    // Tree
                    case "activate_tree_leaf":  return KwActivateTreeLeaf(A("tree_path"), Ab("double_click"));
                    case "expand_tree_node":    return KwExpandCollapse(A("node_name"), true);
                    case "collapse_tree_node":  return KwExpandCollapse(A("node_name"), false);
                    // Property panel
                    case "set_property_value": return KwSetProperty(A("property_name"), A("value"));
                    case "get_property_value": return KwGetProperty(A("property_name"));
                    // Wait
                    case "wait_for_build":    return KwWaitStatus(Ai("timeout",60),  "Build", "Compil");
                    case "wait_for_transfer": return KwWaitStatus(Ai("timeout",120), "Transfer", "Download");
                    case "wait_for_idle":     { _app?.WaitWhileBusy(TimeSpan.FromSeconds(Ai("timeout",30))); return Ok("idle"); }
                    // Screenshot
                    case "take_screenshot":   return KwScreenshot(A("filename"), A("outputdir"));
                    default: return Err("Unknown keyword: " + keyword);
                }
            }
            catch (Exception ex) { return Err(ex.Message); }
        }

        // ── IDE lifecycle ────────────────────────────────────────────────────

        private object KwInitAS(string appPath, int timeout)
        {
            if (string.IsNullOrEmpty(appPath)) return Err("app_path is required");
            if (_app != null) return Ok("already_initialized", _mainWindow?.Title);

            try { _app = Application.Attach(appPath); } catch { _app = Application.Launch(appPath); }

            _automation = new UIA2Automation();
            _cf = new ConditionFactory(new UIA2PropertyLibrary());
            _app.WaitWhileMainHandleIsMissing(TimeSpan.FromSeconds(timeout));
            _app.WaitWhileBusy(TimeSpan.FromSeconds(timeout));
            _mainWindow = _app.GetMainWindow(_automation);
            if (_mainWindow == null) return Err("IDE main window not found within " + timeout + "s");

            _mainWindow.Focus();
            ResolveIDEPanes();

            // Wait until status bar stops showing "Opening..."
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < timeout)
            {
                var sb = _mainWindow.FindFirstChild(_cf.ByControlType(ControlType.StatusBar));
                if (sb == null || sb.Name.IndexOf("Opening", StringComparison.OrdinalIgnoreCase) < 0) break;
                Thread.Sleep(500);
            }
            return Ok("initialized", _mainWindow.Title);
        }

        private object KwCloseApp(bool saveChanges)
        {
            if (_app == null) return Ok("no_app");
            try { _app.Close(); } catch { _app.Kill(); }
            _app = null; _automation?.Dispose(); _automation = null;
            _mainWindow = null; _projectExplorer = _workspace = _toolbox = _propertyWindow = null;
            return Ok("closed");
        }

        private object KwIsProjectLoaded()
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

        private static Point Center(AutomationElement el)
        {
            var r = el.BoundingRectangle;
            return new Point(r.Left + r.Width / 2, r.Top + r.Height / 2);
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

        private object KwTypeIntoField(string fieldLabel, string text)
        {
            var el = _mainWindow.FindFirstDescendant(cf =>
                cf.ByControlType(ControlType.Edit).And(cf.ByName(fieldLabel)))
                ?? _mainWindow.FindFirstDescendant(cf =>
                cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId(fieldLabel)));
            if (el == null) return Err("Field not found: " + fieldLabel);
            Mouse.Click(Center(el));
            Keyboard.TypeSimultaneously(new[] { FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A });
            Keyboard.Type(text);
            return Ok("typed_into_field", fieldLabel);
        }

        private object KwGetText(string id, string by)
        {
            var el = FindOrThrow(id, by);
            return new { result = el.AsTextBox()?.Text ?? el.Name ?? "" };
        }

        // ── Dialog handling ──────────────────────────────────────────────────

        private Window GetModalWindow(string title, int timeout = 15)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            while (sw.Elapsed.TotalSeconds < timeout)
            {
                var w = _mainWindow?.ModalWindows?.FirstOrDefault(x =>
                    title == null || x.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0);
                if (w != null) return w;
                Thread.Sleep(500);
            }
            return null;
        }

        private object KwWaitForDialog(string title, int timeout)
        {
            var w = GetModalWindow(title, timeout);
            return w != null ? Ok("found", w.Title) : Err($"Dialog not found: {title} within {timeout}s");
        }

        private object KwClickDialogButton(string buttonName, string dialogTitle)
        {
            Window dialog = dialogTitle != null ? GetModalWindow(dialogTitle) : _mainWindow;
            if (dialog == null) return Err("Dialog not found: " + dialogTitle);
            var btn = dialog.FindFirstDescendant(cf =>
                cf.ByControlType(ControlType.Button).And(cf.ByName(buttonName)))
                ?? dialog.FindFirstDescendant(cf =>
                cf.ByControlType(ControlType.Button).And(cf.ByName(buttonName + " >")));
            if (btn == null) return Err($"Button '{buttonName}' not found");
            btn.AsButton().Invoke(); Thread.Sleep(500);
            return Ok("clicked_button", buttonName);
        }

        private object KwGetDialogText(string fieldLabel, string dialogTitle)
        {
            Window dialog = dialogTitle != null ? GetModalWindow(dialogTitle) : _mainWindow;
            if (dialog == null) return Err("Dialog not found: " + dialogTitle);
            var el = dialog.FindFirstDescendant(cf =>
                cf.ByControlType(ControlType.Edit).And(cf.ByName(fieldLabel)));
            return el != null ? (object)new { result = el.AsTextBox()?.Text ?? "" } : Err("Field not found: " + fieldLabel);
        }

        // ── Menu interaction ─────────────────────────────────────────────────

        private object KwInvokeMenu(string menuName, string menuItem, string submenuItem)
        {
            var menuBar = _mainWindow.FindFirstChild(_cf.Menu()).AsMenu();
            if (menuBar == null) return Err("Menu bar not found");

            var topMenu = menuBar.FindAllChildren()
                .FirstOrDefault(m => m.Name != null && m.Name.IndexOf(menuName, StringComparison.OrdinalIgnoreCase) >= 0);
            if (topMenu == null) return Err("Menu not found: " + menuName);
            topMenu.AsMenuItem().Click(); Thread.Sleep(800);
            if (menuItem == null) return Ok("menu_opened", menuName);

            var popup = _mainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByName(menuName)))
                ?? _mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Menu));
            var toolbar = popup?.FindFirstChild(cf => cf.ByControlType(ControlType.ToolBar));
            var target = (toolbar ?? popup)?.FindAllChildren()
                .FirstOrDefault(c => c.Name != null && c.Name.IndexOf(menuItem, StringComparison.OrdinalIgnoreCase) >= 0);
            if (target == null) return Err($"Menu item '{menuItem}' not found in '{menuName}'");
            target.AsMenuItem().Click(); Thread.Sleep(500);
            if (submenuItem == null) return Ok("menu_item_clicked", menuItem);

            Thread.Sleep(400);
            var subPopup = _mainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByName(menuItem)));
            var subToolbar = subPopup?.FindFirstChild(cf => cf.ByControlType(ControlType.ToolBar));
            var subTarget = (subToolbar ?? subPopup)?.FindAllChildren()
                .FirstOrDefault(c => c.Name != null && c.Name.IndexOf(submenuItem, StringComparison.OrdinalIgnoreCase) >= 0);
            if (subTarget == null) return Err($"Submenu item '{submenuItem}' not found");
            Mouse.MoveTo(Center(subTarget)); subTarget.AsMenuItem().Click(); Thread.Sleep(500);
            return Ok("submenu_item_clicked", submenuItem);
        }

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

        private object KwActivateTreeLeaf(string treePath, bool doubleClick)
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

        // ── Private helpers ──────────────────────────────────────────────────

        private void ResolveIDEPanes()
        {
            if (_mainWindow == null) return;
            foreach (var pane in _mainWindow.FindAllChildren(_cf.ByControlType(ControlType.Pane)))
            {
                string name = pane.Name ?? "";
                string autoId; try { autoId = pane.AutomationId; } catch { autoId = ""; }
                if (name.IndexOf("View", StringComparison.OrdinalIgnoreCase) >= 0) _projectExplorer = pane;
                if (autoId == "59648") _workspace = pane;
                else if (autoId == "6154") _toolbox = pane;
                else if (autoId == "6155") _propertyWindow = pane;
            }
        }

        private static object Ok(string result, string detail = null) =>
            detail != null ? (object)new { result, detail } : new { result };
        private static object Err(string message) => new { error = message };
    }
}
