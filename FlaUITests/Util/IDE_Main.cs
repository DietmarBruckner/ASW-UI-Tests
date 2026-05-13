using System;
using System.Linq;
using System.Collections.Generic;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA2;
using Menu = FlaUI.Core.AutomationElements.Menu;
using System.Drawing;
using Tesseract;
using FlaUI.Core.Capturing;
using Mouse = FlaUI.Core.Input.Mouse;
using Keyboard = FlaUI.Core.Input.Keyboard;
using System.Globalization;
using System.Windows.Forms;
using FlaUI.Core.Tools;
using Application = FlaUI.Core.Application;
using Button = FlaUI.Core.AutomationElements.Button;
using MenuItem = FlaUI.Core.AutomationElements.MenuItem;
using TextBox = FlaUI.Core.AutomationElements.TextBox;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections;

namespace FlaUITests.Util {
    public class IDE_Main {
        public static Application App { get; private set; }
        private readonly UIA2Automation _automation;
        public static Window MainWindow { get; private set; }
        private readonly ConditionFactory _cf;
        private static Menu _fileMenu, _editMenu, _viewMenu, _insertMenu, _openMenu, _projectMenu, _debugMenu, _onlineMenu, _toolsMenu, _windowMenu, _helpMenu;
        private static Dictionary<string, Menu> MenuNames { get {
            Dictionary<string, Menu> dm = new Dictionary<string, Menu> {
                {"File", _fileMenu}, {"Edit", _editMenu}, {"View", _viewMenu}, {"Insert", _insertMenu}, {"Open", _openMenu}, {"Project", _projectMenu}, {"Debug", _debugMenu}, {"Online", _onlineMenu}, {"Tools", _toolsMenu}, {"Window", _windowMenu}, {"Help", _helpMenu}};
            return dm;
        } }
        public static AutomationElement ProjectExplorer { get; private set; }
        public static AutomationElement Toolbox { get; private set; }
        public static AutomationElement PropertyWindow { get; private set; }
        public static AutomationElement OutputWindow { get; private set; }
        public static AutomationElement StatusBar { get; private set; }
        public static AutomationElement Workspace { get; private set; }
        private static TitleBar _titleBar;
        private static AutomationElement _toolBars;
        public static AutomationElement ToolBarStandard { get; private set; }
        public static AutomationElement ToolBarBuild { get; private set; }
        private static AutomationElement _onlineToolBar;
        private static AutomationElement _unittestToolBar;
        private static AutomationElement _editToolBar;
        private static AutomationElement _formatToolBar;
        private static AutomationElement _zoomToolBar;
        private static AutomationElement _debugToolBar;
        private Screen _screen;
        public Dictionary<string, Rectangle> UIElementsBounds { get {
                AutomationElement a;
                Dictionary<string, Rectangle> bounds = new Dictionary<string, Rectangle> {
                    { "MainWindow", MainWindow.BoundingRectangle } };
                if ((a = MainWindow.TitleBar) != null)
                    bounds.Add("TitleBar", a.BoundingRectangle);
                if ((a = MainWindow.FindFirstChild(_cf.Menu()).AsMenu()) != null)
                    bounds.Add("Menus", a.BoundingRectangle);
                if ((a = MainWindow.FindFirstChild(_cf.ByControlType(ControlType.Pane).And(_cf.ByAutomationId("59419")))) != null)
                    bounds.Add("ToolBar", a.BoundingRectangle);
                if (ProjectExplorer != null)
                    bounds.Add("ProjectExplorer", ProjectExplorer.BoundingRectangle);
                if (Workspace != null)
                    bounds.Add("Workspace", Workspace.BoundingRectangle);
                if (Toolbox != null)
                    bounds.Add("Toolbox", Toolbox.BoundingRectangle);
                if (OutputWindow != null)
                    bounds.Add("OutputWindow", OutputWindow.BoundingRectangle);
                if (PropertyWindow != null)
                    bounds.Add("PropertyWindow", PropertyWindow.BoundingRectangle);
                if ((a = MainWindow.FindFirstChild(_cf.ByControlType(ControlType.StatusBar))) != null)
                    bounds.Add("StatusBar", a.BoundingRectangle);
                return bounds;
            } }
        public static List<Editor> Editors = new List<Editor>();
        public IDE_Main (Application app) {
            App = app;
            App.WaitWhileMainHandleIsMissing(TimeSpan.FromSeconds(20));
            App.WaitWhileBusy(TimeSpan.FromSeconds(20));
            _automation = new UIA2Automation();
            MainWindow = App.GetMainWindow(_automation);
            _cf = new ConditionFactory(new UIA2PropertyLibrary());
            MainWindow.Focus();
            Init();
            var rect = MainWindow.BoundingRectangle;
            _screen = Screen.FromHandle(MainWindow.Properties.NativeWindowHandle);
            bool isFullScreen = rect.Left <= _screen.WorkingArea.Left && rect.Top <= _screen.WorkingArea.Top && rect.Width >= _screen.WorkingArea.Width && rect.Height >= _screen.WorkingArea.Height;
            if (!isFullScreen) {
                if (TreeConfig.CurrentProject != null)
                    Util.ConsoleOut(Util.Verbose.FULL, "Maximizing main window");
                MainWindow.TitleBar.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Maximize"))).AsButton().Invoke();
            }

        }
        void InitMenues() {
            Menu menu = MainWindow.FindFirstChild(_cf.Menu()).AsMenu();
            AutomationElement[] menus = menu.FindAllChildren();
            foreach (AutomationElement m in menus) {
                string name = m.Name;
                if (name == null) continue;
                if (name.IndexOf("File") >= 0)
                    _fileMenu = m.AsMenu();
                else if (name.IndexOf("Edit") >= 0)
                    _editMenu = m.AsMenu();
                else if (name.IndexOf("View") >= 0)
                    _viewMenu = m.AsMenu();
                else if (name.IndexOf("Insert") >= 0)
                    _insertMenu = m.AsMenu();
                else if (name.IndexOf("Open") >= 0)
                    _openMenu = m.AsMenu();
                else if (name.IndexOf("Project") >= 0)
                    _projectMenu = m.AsMenu();
                else if (name.IndexOf("Debug") >= 0)
                    _debugMenu = m.AsMenu();
                else if (name.IndexOf("nline") >= 0)
                    _onlineMenu = m.AsMenu();
                else if (name.IndexOf("Tools") >= 0)
                    _toolsMenu = m.AsMenu();
                else if (name.IndexOf("Window") >= 0)
                    _windowMenu = m.AsMenu();
                else if (name.IndexOf("Help") >= 0)
                    _helpMenu = m.AsMenu();
            }
        }
        void InitUIElements() {
            AutomationElement [] allPanes = MainWindow.FindAllChildren(_cf.ByControlType(ControlType.Pane));
            foreach (AutomationElement a in allPanes) {
                string name = a.Name;
                string autoId;
                try {autoId = a.AutomationId;} catch (Exception) { autoId = ""; }
                if (name == null) continue;
                if (name != "") {
                    if (name.IndexOf("View", StringComparison.OrdinalIgnoreCase) >= 0)
                        ProjectExplorer = a;
                    else if (name.IndexOf("Output", StringComparison.OrdinalIgnoreCase) >= 0)
                        OutputWindow = a;
                }
                if (autoId == "59648") {
                    Workspace = a;
                    continue;
                }
                if (autoId == "6154") {
                    Toolbox = a;
                    continue;
                }
                if (autoId == "6155") {
                    PropertyWindow = a;
                    continue;
                }
                AutomationElement[] children = a.FindAllChildren();
                if(children.Any(c => c.ControlType == ControlType.ToolBar)) {
                    _toolBars = a;
                    foreach (AutomationElement child in children) {
                        string childName = child.Name;
                        if (childName == null) continue;
                        if (childName.IndexOf("Standard", StringComparison.OrdinalIgnoreCase) >= 0)
                            ToolBarStandard = child;
                        else if (childName.IndexOf("Build", StringComparison.OrdinalIgnoreCase) >= 0)
                            ToolBarBuild = child;
                        else if (childName.IndexOf("Online", StringComparison.OrdinalIgnoreCase) >= 0)
                            _onlineToolBar = child;
                        else if (childName.IndexOf("Unit", StringComparison.OrdinalIgnoreCase) >= 0)
                            _unittestToolBar = child;
                        else if (childName.IndexOf("Edit", StringComparison.OrdinalIgnoreCase) >= 0)
                            _editToolBar = child;
                        else if (childName.IndexOf("Format", StringComparison.OrdinalIgnoreCase) >= 0)
                            _formatToolBar = child;
                        else if (childName.IndexOf("Zoom", StringComparison.OrdinalIgnoreCase) >= 0)
                            _zoomToolBar = child;
                        else if (childName.IndexOf("Debug", StringComparison.OrdinalIgnoreCase) >= 0)
                            _debugToolBar = child;
                    }
                }
            }            
        }
        void Init() {
            InitMenues();
            AutomationElement[] allPanes = MainWindow.FindAllChildren(_cf.ByControlType(ControlType.StatusBar));
            if (allPanes.Length > 0)
                StatusBar = allPanes[0];
            while (StatusBar.Name.IndexOf("Opening", StringComparison.OrdinalIgnoreCase) >= 0);
            InitUIElements();
            _titleBar = MainWindow.TitleBar;
            Util.ConsoleOut(Util.Verbose.LIGHT, "Application opened successfully. Main elements initialized.");
            AutomationElement [] editors = Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window));
            foreach (var e in editors) {
                string s = e.Name;
                int i = s.IndexOf('[');
                if (i>0)
                    s = s.Substring(0, i-1);
                Editors.Add(new Editor().Open(s));
            }
        }
        public void InvokeMenuItem(Menu menu, string menuItemName, string subMenuItemName = null) {
            string nameMenu = menu.Name.Substring(3, menu.Name.Length - 3); // Remove the trailing 'BR&' from the menu name
            if (nameMenu == "&nline")
                nameMenu = "Online";
            int i = 4;
            while (i-- >= 0) {
                try {
                    menu.Click();
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(800));
                    Menu m = MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByName(nameMenu))).AsMenu();
                    AutomationElement toolBar = m.FindFirstChild(cf => cf.ByControlType(ControlType.ToolBar));
                    MenuItem mi = null;
                    bool notFound = true;
                    AutomationElement[] children = toolBar.FindAllChildren();
                    foreach (AutomationElement child in children) {
                        string name = child.Name;
                        if (name.IndexOf(menuItemName) >= 0) {
                            mi = child.AsMenuItem();
                            notFound = false;
                            break;
                        }
                    }
                    if (notFound) 
                        continue; 
                    mi.Click();
                    if (subMenuItemName != null) {
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(800));
                        Menu subMenu = MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByName(menuItemName))).AsMenu();
                        toolBar = subMenu.FindFirstChild(cf => cf.ByControlType(ControlType.ToolBar));
                        mi = null;
                        notFound = true;
                        AutomationElement[] subChildren = toolBar.FindAllChildren();
                        foreach (AutomationElement child in subChildren) {
                            string name = child.Name;
                            if (name.IndexOf(subMenuItemName) >= 0) {
                                mi = child.AsMenuItem();
                                notFound = false;
                                break;
                            }
                        }
                        if (notFound) 
                            continue;
                        Mouse.MoveTo(mi.BoundingRectangle.Center());
                        mi.Click();
                    }
                    break;
                }
                catch (Exception) { Util.ConsoleOut(Util.Verbose.LIGHT, "Error while trying to click " + menuItemName + " in menu " + nameMenu + ((subMenuItemName != null)? " in submenu " + subMenuItemName : "") + ". trys left: " + i); }
            }
        }
        public string[] GetProjectpath()
        {
            String titleString = _titleBar.Name;
            String configString = "";
            String projectString = "";
            String folder = "";
            int i = titleString.IndexOf("Automation Studio", StringComparison.OrdinalIgnoreCase);
            if (i >= 0)
                projectString = titleString.Substring(0, i - 3);
            i = projectString.LastIndexOf("/", StringComparison.OrdinalIgnoreCase);
            if (i >= 0)
                configString = projectString.Substring(i + 1, projectString.Length - i - 1);
            projectString = projectString.Substring(0, i);
            i = projectString.LastIndexOf("\\", StringComparison.OrdinalIgnoreCase);
            if (i >= 0)
                folder = projectString.Substring(0, i);
            projectString = projectString.Substring(i + 1, projectString.Length - i - 1);
            return new string[] { folder, configString, projectString };
        }
        public bool IsProjectLoaded() {
            return _titleBar != null && !string.IsNullOrEmpty(_titleBar.Name) && _titleBar.Name.IndexOf("Automation Studio", StringComparison.OrdinalIgnoreCase) >= 10;
        }
        public Menu GetMenu(string menuName) {
            InitMenues();
            if (MenuNames.ContainsKey(menuName)) {
                return MenuNames[menuName];
            }
            return null;
        }
        public Window GetModalWindow(String name) {
            Window w;
            while ((w = MainWindow.ModalWindows.FirstOrDefault(x => x.Title.Contains(name))) == null) {
                Util.ConsoleOut(Util.Verbose.FULL, "Waiting for window: " + name);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            CheckResizeWindowWithinScreen(w);
            return w;
        }
        public void LooseModalWindow(Window w) {
            while (MainWindow.ModalWindows.Contains(w)) {
                Util.ConsoleOut(Util.Verbose.FULL, "Waiting for closing of window: " + w.Name);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
        public void CheckResizeWindowWithinScreen (Window w) {
            Rectangle wbr = w.BoundingRectangle;
            Screen screen = Screen.FromHandle(w.Properties.NativeWindowHandle);
            Rectangle sr = screen.Bounds;
            bool isFullyVisible = wbr.Left >= sr.Left && wbr.Top >= sr.Top && wbr.Right <= sr.Right && wbr.Bottom <= sr.Bottom;
            if (!isFullyVisible) {
                Util.ConsoleOut(Util.Verbose.FULL, "Window not fully visible - trying to make it fit screen.");
                Rectangle tbr = w.TitleBar.BoundingRectangle;
                Point point = new Point { X = tbr.Left + tbr.Width / 2, Y = tbr.Top + tbr.Height / 2 };
                //just move or needs resize?
                bool fitsScreen = wbr.Width <= sr.Width && wbr.Height <= sr.Height;
                if (fitsScreen) {
                    Mouse.MoveTo(point);
                    Mouse.Drag(point, sr.Left + sr.Width / 2 - wbr.Left - wbr.Width / 2, 20 - wbr.Top - wbr.Height / 2);
                }
            }
        } 
        public void InitializeViews(bool projectExplorer = false, bool toolbox = false, bool propertyWindow = false, bool outputResults = false, bool statusBar = false) {
            if (TreeConfig.CurrentProject != null)
                Util.ConsoleOut(Util.Verbose.FULL, "Checking if necessary view(s) are there: " + (projectExplorer?"Project Explorer ":"") + (toolbox?"Object catalog ":"") + (propertyWindow?"Property window ":"") + (outputResults?"Output results ":"") + (statusBar?"Statusbar":""));
            if (projectExplorer) {
                ProjectExplorer = MainWindow.FindAllChildren(cf => cf.ByControlType(ControlType.Pane)).FirstOrDefault(c => c.Name.IndexOf("View") >= 0);
                if (ProjectExplorer == null) {
                    InvokeMenuItem(GetMenu("View"), "Project Explorer", "Logical View");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    ProjectExplorer = MainWindow.FindAllChildren(cf => cf.ByControlType(ControlType.Pane)).FirstOrDefault(c => c.Name.IndexOf("View") >= 0);
                }
            }
            if (toolbox) {
                Toolbox = MainWindow.FindAllChildren(cf => cf.ByControlType(ControlType.Pane)).FirstOrDefault(cf => cf.Name.IndexOf("Toolbox") >= 0);
                if (Toolbox == null) {
                    InvokeMenuItem(GetMenu("View"), "Toolbox");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    Toolbox = MainWindow.FindAllChildren(cf => cf.ByControlType(ControlType.Pane)).FirstOrDefault(cf => cf.Name.IndexOf("Toolbox") >= 0);
                }
            }
            if (propertyWindow) {
                PropertyWindow = MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Property Window")));
                if (PropertyWindow == null) {
                    InvokeMenuItem(GetMenu("View"), "Property Window");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    PropertyWindow = MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Property Window")));
                }
            }
            if (outputResults) {
                string [] tabs = {"Output Results", "Debugger Console", "Callstack", "Breakpoints", "Debugger Watch", "Contextual Watch", "Reference List", "Cross Reference", "Output", "Find In Files"};
                AutomationElement [] ae = MainWindow.FindAllChildren();
                foreach (var v in ae)
                    foreach (string s in tabs)
                        if (v.Name.IndexOf(s) >= 0)
                            OutputWindow = v;
                if (OutputWindow == null) {
                    InvokeMenuItem(GetMenu("View"), "Output", "Output Results");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    OutputWindow = MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Output Results")));
                }
                AutomationElement a = OutputWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Tab));
                a = a.FindFirstChild(cf => cf.ByControlType(ControlType.TabItem).And(cf.ByName("Output Results")));
                if (a == null) {
                    InvokeMenuItem(GetMenu("View"), "Output", "Output Results");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));                    
                }
            }
            if (statusBar) {
                StatusBar = MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.StatusBar));
                if (StatusBar == null) {
                    InvokeMenuItem(GetMenu("View"), "Status Bar");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    StatusBar = MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.StatusBar));
                }
            }
        }
        public void SetToolBoxMinSize(bool categories) {
            Rectangle splitviewRect = UIElementsBounds["Toolbox"];
            AutomationElement a = Toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByAutomationId("_splitContainer")));
            AutomationElement [] allChildren = a.FindAllChildren();
            Rectangle categoriesListViewRect = allChildren[0].FindAllDescendants().First(c => c.AutomationId == "_categoriesListView").BoundingRectangle;
            Rectangle elementsListViewRect = allChildren[1].FindAllDescendants().First(c => c.AutomationId == "_elementsListView").BoundingRectangle;
            //min size of 250 px (or height of categories list + 50) height and 400 px width for the Toolbox to ensure all elements are visible and can be clicked
            if (splitviewRect.Height < 250 || splitviewRect.Height < categoriesListViewRect.Height + 50) {
                Util.ConsoleOut(Util.Verbose.FULL, "Toolbox size too small to make toolbox elements visible - trying to make it bigger.");
                Point point = new Point { X = splitviewRect.Left + 30, Y = splitviewRect.Bottom + 1};
                Mouse.DragVertically(point, Math.Max(251, categoriesListViewRect.Height + 50) - splitviewRect.Height);
            }
            if (splitviewRect.Width < 400) {
                Util.ConsoleOut(Util.Verbose.FULL, "Toolbox size too small to make toolbox elements visible - trying to make it bigger.");
                Point point = new Point { X = splitviewRect.Left - 1, Y = categoriesListViewRect.Top + 30 };
                Mouse.DragHorizontally(point, splitviewRect.Width - 401);
            }
            //min size of 200 px height and 400 px width for the Categories list to ensure all elements are visible and can be clicked
            if (categories) {
                if (categoriesListViewRect.Height < 100) {
                    Util.ConsoleOut(Util.Verbose.FULL, "Categories list size too small to make elements visible - trying to make it bigger.");
                    Point point = new Point { X = categoriesListViewRect.Left + 30, Y = categoriesListViewRect.Bottom + 1};
                    Mouse.DragVertically(point, 101 - categoriesListViewRect.Height);
                }
            }
            else {
                if (elementsListViewRect.Height < 100) {
                    Util.ConsoleOut(Util.Verbose.FULL, "Elements list size too small to make elements visible - trying to make it bigger.");
                    Point point = new Point { X = categoriesListViewRect.Left + 30, Y = categoriesListViewRect.Bottom + 1};
                    //Mouse.MoveTo(point);
                    Mouse.DragVertically(point, elementsListViewRect.Height - 101);
                }
            }
        }
        public void SearchToolBox(string searchTerm) {
            Util.ConsoleOut(Util.Verbose.FULL, "Typing into Object catalog search field: " + searchTerm);
            AutomationElement searchTextBox = Toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("searchTermTextBox")));
            if (searchTextBox.Name != searchTerm) {
                Toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.ToolBar).And(cf.ByAutomationId("_toolStrip"))).FindAllChildren(cf => cf.ByControlType(ControlType.Button))[1].AsButton().Click();
                TreeConfig.ClickAutomationElement(searchTextBox);
                foreach (char ch in searchTerm) {
                    Keyboard.Type(ch);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(50));
                }
            }
        }
        public void WaitForMessage(string message, int timeout = 30) {
            InitializeViews(outputResults:true);
            if (TreeConfig.CurrentProject != null)
                Util.ConsoleOut(Util.Verbose.FULL, "Waiting for message: " + message + "; Timeout: " + timeout);
            DateTime now = DateTime.Now;
            bool done = false;
            AutomationElement outputListView = OutputWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("outputListView")));
            AutomationElement header = outputListView.FindFirstChild(cf => cf.ByControlType(ControlType.Header));
            AutomationElement [] items = header.FindAllChildren();
            //find the columns containing date and description
            AutomationElement dt = header.FindFirstChild(cf => cf.ByControlType(ControlType.HeaderItem).And(cf.ByName("Date/Time")));
            AutomationElement des = header.FindFirstChild(cf => cf.ByControlType(ControlType.HeaderItem).And(cf.ByName("Description")));
            int idt = 0, ides = 0;
            foreach (var i in items) {
                if (i.Name == dt.Name)
                    break;
                idt++;
            }
            foreach (var i in items) {
                if (i.Name == des.Name)
                    break;
                ides++;
            }
            //sort descriptions by datetime and parse through last 3 seconds for desired message
            while (!done && DateTime.Now < now.AddSeconds(timeout)) {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                outputListView = OutputWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("outputListView")));
                AutomationElement [] allMessages = outputListView.FindAllChildren(cf => cf.ByControlType(ControlType.DataItem));
                if (allMessages.Count() == 0)
                    continue;
                SortedDictionary<DateTime, AutomationElement> dictMessages = new SortedDictionary<DateTime, AutomationElement> ();
                foreach (AutomationElement a in allMessages) {
                    try {
                        dictMessages.Add(DateTime.Parse(a.FindAllChildren()[idt].Name.Replace(',', '.'), CultureInfo.GetCultureInfo("de-AT").DateTimeFormat), a);
                    } catch {} //if same timestamp already exists, ignore
                }
                DateTime latest = dictMessages.Keys.Max();
                List<string> latestDescriptions = new List<string> ();
                foreach (KeyValuePair<DateTime, AutomationElement> item in dictMessages) { 
                    if (item.Key.AddSeconds(3) >= latest)
                        latestDescriptions.Add(item.Value.FindAllChildren()[ides].Name);
                }
                foreach (string s in latestDescriptions)
                    if (s.IndexOf(message, StringComparison.OrdinalIgnoreCase) >= 0)
                        done = true;
            }
            if (!done)
                Util.ConsoleOut(Util.Verbose.FULL, "Waiting for message ran into timeout");
            else if (TreeConfig.CurrentProject != null)
                Util.ConsoleOut(Util.Verbose.FULL, "Message arrived");
        }
        public void SwitchView(TreeConfig.ViewType view, int x = 400, int y = 400) {
            Util.ConsoleOut(Util.Verbose.LIGHT, "Switching to view: " + view.ToString());
            Point point;
            Rectangle Rect = UIElementsBounds["ProjectExplorer"];
            bool dragX = true, dragY = true;
            switch (view) {
                case TreeConfig.ViewType.PropertyWindow:
                    Rect = UIElementsBounds["PropertyWindow"];
                    dragX = dragY = false;
                    break;
                case TreeConfig.ViewType.Workspace:
                    Rect = UIElementsBounds["Workspace"];
                    break;
            }
            if (Rect.Width < x) {
                Util.ConsoleOut(Util.Verbose.FULL, view.ToString() + " size too thin - trying to make it broader.");
                point = new Point { X = dragX?Rect.Right + 1:Rect.Left-1, Y = Rect.Top + 30};
                Mouse.DragHorizontally(point, (x+1 - Rect.Width)*(dragX?1:-1));
            }
            if (Rect.Height < y) {
                Util.ConsoleOut(Util.Verbose.FULL, view.ToString() + " size too small - trying to make it taller.");
                point = new Point { X = Rect.Left + 30, Y = dragY?Rect.Bottom + 1:Rect.Top - 1};
                Mouse.DragVertically(point, (y+1 - Rect.Height)*(dragY?1:-1));
            }
            AutomationElement ViewTab = ProjectExplorer.FindFirstChild();
            switch (view) {
                case TreeConfig.ViewType.LogicalView:
                    ViewTab = ViewTab.FindFirstChild(cf => cf.ByControlType(ControlType.TabItem).And(cf.ByName("Logical View")));
                    break;
                case TreeConfig.ViewType.ConfigurationView:
                    ViewTab = ViewTab.FindFirstChild(cf => cf.ByControlType(ControlType.TabItem).And(cf.ByName("Configuration View")));
                    break;
                case TreeConfig.ViewType.PhysicalView:
                    ViewTab = ViewTab.FindFirstChild(cf => cf.ByControlType(ControlType.TabItem).And(cf.ByName("Physical View")));
                    break;
            }
            if (view != TreeConfig.ViewType.Workspace && view != TreeConfig.ViewType.PropertyWindow)
                TreeConfig.ClickAutomationElement(ViewTab);
            else
                Mouse.Click(Rect.Center());
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
        }
        public AutomationElement GetActiveConfigurtion() {
            SwitchView(TreeConfig.ViewType.ConfigurationView);
            AutomationElement treeElement = ProjectExplorer.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree).And(cf.ByAutomationId("ConfigurationTree")));
            AutomationElement [] allConfigurations = treeElement.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem));
            return allConfigurations.First(cf => cf.Name.IndexOf("[Active]", StringComparison.OrdinalIgnoreCase) >= 0) ?? throw new Exception("Active configuration not found");
        }
        public AutomationElement GetLogicalViewRoot(AppProject project) {
            SwitchView(TreeConfig.ViewType.LogicalView);
            return ProjectExplorer.FindFirstDescendant(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_" + project.Name.Substring(0, project.Name.IndexOf(".")))));
        }
        public void ActivateSimulation() {
            if (!IsButtonActive(_onlineToolBar.FindFirstChild(cf => cf.ByName("BR_\nActivate Simulation")).AsButton(), "activateSimulation"))
                InvokeMenuItem(GetMenu("Online"), "Activate Simulation");
        }
        public void InsertObjectFromToolBox(TreeConfig.ViewType viewType, string category, string objectName, bool drag = false, Point toDrag = new Point()) {
            InitializeViews(projectExplorer: true, toolbox: true, outputResults: true);
            SwitchView(viewType);
            if (category != string.Empty) {
                SetToolBoxMinSize(categories: true);
                SearchToolBox(category);
                AutomationElement toolBoxCategories = Toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByAutomationId("_categoriesListView")));
                AutomationElement desiredToolBoxItem = toolBoxCategories.FindFirstDescendant(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByName(category))) ?? throw new Exception(category + " toolbox item not found - not installed?");
                AutomationElement [] allDesc = desiredToolBoxItem.FindAllChildren();
                if (allDesc[0].AsCheckBox().IsChecked == false) {
                    desiredToolBoxItem.Click();
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                }
            }
            SetToolBoxMinSize(categories: false);
            SearchToolBox(objectName);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            AutomationElement toolBoxContextContent = Toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("_elementsListView")));
            AutomationElement desiredElementItem = toolBoxContextContent.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataItem).And(cf.ByName(objectName))) ?? throw new Exception(objectName + " element not found");
            if (drag) {
                Mouse.MoveTo(desiredElementItem.BoundingRectangle.Center());
                Mouse.Down();
                Mouse.MoveTo(toDrag);
                Mouse.Up();
            }
            else
                desiredElementItem.DoubleClick();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
        }
        public bool IsButtonActive(Button button, string image = "") {
            //string workingDirectory = System.Environment.CurrentDirectory;
            //Capture.Element(button).ToFile(             workingDirectory + "\\FlaUITests\\Util\\screenshots\\" + image + ".png");
            Util.ConsoleOut(Util.Verbose.FULL, "Checking if button: " + button.Name + " is activated");
            Color borderColor = Capture.Element(button).Bitmap.GetPixel(0,0);
            if (borderColor.Equals(Color.FromArgb(0, 120, 215)))
/*          Color blue = Color.Blue;
            Image actual = ResizeImage(Image.FromFile(  workingDirectory + "\\FlaUITests\\Util\\screenshots\\" + image + ".png"), 33, 33);
            actual.Save(workingDirectory + "\\FlaUITests\\Util\\screenshots\\" + image + "_resized.png");
            Image active = Image.FromFile(              workingDirectory + "\\FlaUITests\\Util\\Buttons\\" + image + "_active.png");
            Image inactive = Image.FromFile(            workingDirectory + "\\FlaUITests\\Util\\Buttons\\" + image + "_inactive.png");
            bool isactive = ImageComparer.Compare(actual, active, new ColorDifference(20), out Image diff);
            diff.Save(workingDirectory + "\\FlaUITests\\Util\\screenshots\\" + image + "_diffactive.png");
            bool isinactive =   ImageComparer.Compare(actual, inactive, new ColorDifference(20), out diff);
            diff.Save(workingDirectory + "\\FlaUITests\\Util\\screenshots\\" + image + "_diffinactive.png");
            if ((isactive && isinactive) || (!isactive && !isinactive))
                throw new Exception ("Could not discern if button " + button.Name + " is active");
            if (isactive)*/
                return true;
            return false;
        }
        /*Bitmap ResizeImage(Image image, int width, int height) {
            Rectangle destRect = new Rectangle(0, 0, width, height);
            Bitmap destImage = new Bitmap(width, height);
            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            using (Graphics graphics = Graphics.FromImage(destImage)) {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (ImageAttributes wrapMode = new ImageAttributes()) {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width,image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        } */       
        public void Transfer () {
            ToolBarBuild.FindAllDescendants(cf => cf.ByControlType(ControlType.Button)).FirstOrDefault(cf => cf.Name.IndexOf("BR_\nTransfer", StringComparison.OrdinalIgnoreCase) >= 0).AsButton().Click();
            Window transferDialog;
            while ((transferDialog = GetModalWindow("Transfer to target")) == null)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Button transferButton = transferDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Transfer"))).AsButton();
            AutomationElement infoPane = transferDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByAutomationId("pStepsOutline")));
            if (infoPane.Name.IndexOf("initial", StringComparison.OrdinalIgnoreCase) >= 0) {
                transferButton.Click();
                Window deletionWarningDialog;
                while ((deletionWarningDialog = GetModalWindow("Target application storage will be deleted")) == null)
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                Button yesButton = deletionWarningDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Yes"))).AsButton();
                yesButton.Click();
                while (transferDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Text).And(cf.ByAutomationId("tBInfo"))).AsTextBox().Text.IndexOf("Install finished", StringComparison.OrdinalIgnoreCase) < 0)
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                Button closeButton = transferDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByAutomationId("bClose"))).AsButton();
                closeButton.Click();
            }
        }
        public void Build() {
            ToolBarBuild.FindAllDescendants(cf => cf.ByControlType(ControlType.Button)).FirstOrDefault(cf => cf.Name.IndexOf("BR_\nBuild", StringComparison.OrdinalIgnoreCase) >= 0).AsButton().Click();
            WaitForMessage("Build: ", 60);
            Window buildProjectWindow = GetModalWindow("Build Project");
            buildProjectWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Close"))).AsButton().Click();
        }
        public void Save() {
            ToolBarStandard.FindAllDescendants(cf => cf.ByControlType(ControlType.Button)).FirstOrDefault(cf => cf.Name.IndexOf("BR_\nSave", StringComparison.OrdinalIgnoreCase) >= 0).AsButton().Click();
        }
        public void SaveAll() {
            ToolBarStandard.FindAllDescendants(cf => cf.ByControlType(ControlType.Button)).FirstOrDefault(cf => cf.Name.IndexOf("BR_\nSave All", StringComparison.OrdinalIgnoreCase) >= 0).AsButton().Click();
        }
        public void SelectComponentVersion (string componentName, string version) {
            InvokeMenuItem(GetMenu("Project"), "Change Runtime Versions...");
            Window manageComponentsWindow;
            while ((manageComponentsWindow = GetModalWindow(TreeConfig.CurrentProject.CPU + " - Properties")) == null)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            AutomationElement tabcontrol = manageComponentsWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Tab).And(cf.ByAutomationId("tabControl")));
            TabItem componentsTab = tabcontrol.FindFirstChild(cf => cf.ByControlType(ControlType.TabItem).And(cf.ByName("Runtime Versions"))).AsTabItem();
            TreeConfig.ClickAutomationElement(componentsTab);
            AutomationElement componentsListView = componentsTab.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid));
            AutomationElement [] componentItems = componentsListView.FindAllDescendants(cf => cf.ByControlType(ControlType.DataItem));
            AutomationElement componentItem = null;
            using (var engine = new TesseractEngine(System.Environment.CurrentDirectory + "\\FlaUITests\\Util\\tessdata", "eng", EngineMode.Default)) {
                if (componentName == "Automation Runtime") {
                    componentItem = componentItems.FirstOrDefault(c => c.Name.IndexOf(".ArCfg", StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else if (componentName == "Visual Components"){
                    componentItem = componentItems.FirstOrDefault(c => c.Name.IndexOf(".VcCfg", StringComparison.OrdinalIgnoreCase) >= 0);
                }
                else {
                    foreach (AutomationElement item in componentItems) {
                        if (item.Name.IndexOf(".DomainCfg", StringComparison.OrdinalIgnoreCase) >= 0) {
                            AutomationElement compText = item.FindAllChildren(cf => cf.ByControlType(ControlType.Custom))[0];
                            CaptureImage compImg = Capture.Element(compText);
                            string file = System.Environment.CurrentDirectory + "\\FlaUITests\\Util\\screenshots\\OCR.png";
                            compImg.ToFile(file);
                            Page page = engine.Process(Pix.LoadFromFile(file));
                            string text = page.GetText();
                            page.Dispose();
                            if (text.IndexOf(componentName) >= 0) {
                                componentItem = item;
                                break;
                            }
                        }
                        else
                            continue;
                    }
                }
            }
            AutomationElement [] allTexts = componentItem.FindAllChildren(cf => cf.ByControlType(ControlType.Custom));
            TextBox versText = allTexts[2].FindFirstChild().AsTextBox();
            if (!(versText.Name.IndexOf(version) >= 0)) {
                TreeConfig.ClickAutomationElement(allTexts[1].FindFirstChild(cf => cf.ByControlType(ControlType.ComboBox)));
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
                AutomationElement wa = manageComponentsWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Window));
                AutomationElement a = wa.FindFirstChild(cf => cf.ByName(version).And(cf.ByControlType(ControlType.ListItem)));
                if (a != null)
                    TreeConfig.ClickAutomationElement(a);
                else {
                    InstallComponentVersion(componentName, version);
                    SelectComponentVersion(componentName, version);
                    return;
                }
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
            Button okButton = manageComponentsWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByAutomationId("btnOk"))).AsButton();
            okButton.Click();
            LooseModalWindow(manageComponentsWindow);
            TreeConfig.ClickAutomationElement(IDE_Main.MainWindow.TitleBar);
        }
        public void InstallComponentVersion (string componentName, string version) {
        }
        public Rectangle FindWordinCapture (AutomationElement ae, string text) {
            Util.ConsoleOut(Util.Verbose.FULL, "No text available, searching for word \"" + text + "\" in element: " + ae.Name);
            Dictionary<Rectangle, string> dict = new Dictionary<Rectangle, string>();
            PageIteratorLevel containingWord = PageIteratorLevel.Word;
            using (var engine = new TesseractEngine(System.Environment.CurrentDirectory + "\\FlaUITests\\Util\\tessdata", "eng", EngineMode.Default)) {
                CaptureImage compImg = Capture.Element(ae);
                string file = System.Environment.CurrentDirectory + "\\FlaUITests\\Util\\screenshots\\OCR_" + TreeConfig.RemoveSpecialChars(text) + ".png";
                compImg.ToFile(file);
                using (Page page = engine.Process(Pix.LoadFromFile(file))) {
                    using (var iter = page.GetIterator()) {
                        iter.Begin();
                        do {
                            if (iter.TryGetBoundingBox(containingWord, out var rect))
                                dict.Add(new Rectangle(rect.X1, rect.Y1, rect.X2-rect.X1, rect.Y2-rect.Y1), iter.GetText(containingWord));
                        } while (iter.Next(containingWord));
                    }
                }           
            }
            Rectangle rec = new Rectangle();
            int i, min = int.MaxValue;
            foreach (var d in dict) {
                i = Util.GetDamerauLevenshteinDistance(text, d.Value);
                if (i < min) {
                    min = i;
                    rec = d.Key;
                }
            }
            return rec;
        }
        public void SetIWorkspaceMinSize(AutomationElement scrollableEditor = null, bool percent = false) {
            InitUIElements();
            Rectangle rect = UIElementsBounds["Workspace"];
            if (percent) {
                int Xscreen = (int) (_screen.WorkingArea.Width * 0.6);
                int Yscreen = (int) (_screen.WorkingArea.Height * 0.6);
                if (rect.Width < Xscreen) {
                    Util.ConsoleOut(Util.Verbose.FULL, "Workspace size too narrow - trying to make it broader.");
                    if (ProjectExplorer == null) {
                        Point point = new Point { X = rect.Right + 1, Y = rect.Bottom - 30};
                        Mouse.DragHorizontally(point, Xscreen+1 - rect.Width);
                    }
                    else {
                        if (Toolbox == null) {
                            Point point = new Point { X = rect.Left - 1, Y = rect.Bottom - 30};
                            Mouse.DragHorizontally(point, rect.Width - Xscreen - 1);
                        }
                        else {
                            float ratio = 1f*UIElementsBounds["ProjectExplorer"].Width/(1f*UIElementsBounds["Toolbox"].Width);
                            Point point = new Point { X = rect.Left - 1, Y = rect.Bottom - 30};
                            Mouse.DragHorizontally(point, (int) ((rect.Width - Xscreen - 1)*ratio/(1f+ratio)));
                            point = new Point { X = rect.Right + 1, Y = rect.Bottom - 30};
                            Mouse.DragHorizontally(point, (int)((Xscreen + 1 - rect.Width)/(1f+ratio)));
                        }
                    }
                }
                if (rect.Height < Yscreen) {
                    Util.ConsoleOut(Util.Verbose.FULL, "Workspace size too small - trying to make it taller.");
                    Point point = new Point { X = rect.Left + 30, Y = rect.Bottom + 1};
                    Mouse.DragVertically(point, Yscreen + 1 - rect.Height);
                }
            }
            if (scrollableEditor is null) return;
            if (scrollableEditor.Patterns.Scroll.Pattern.VerticalScrollPercent != 0d) {
                TreeConfig.ClickAutomationElement(scrollableEditor);
                using (Keyboard.Pressing(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL)) {
                    do {
                        Mouse.Scroll(0.5d);
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    } while (scrollableEditor.Patterns.Scroll.Pattern.VerticalScrollPercent != 0d);
                }
            }
            while (scrollableEditor.Patterns.Scroll.Pattern.HorizontalScrollPercent != 0d) {
                rect = UIElementsBounds["Workspace"];
                Point point = new Point { X = rect.Left - 1, Y = rect.Bottom - 100};
                Mouse.DragHorizontally(point, 100);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                if (scrollableEditor.Patterns.Scroll.Pattern.HorizontalScrollPercent == 0d) break;
                point = new Point { X = rect.Right + 1, Y = rect.Bottom - 100};
                Mouse.DragHorizontally(point, -100);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
        }
        public void RemoveTrailingWhitespaceFromXML(AutomationElement editor) {
            bool emptyline = true;
            while (emptyline) {
                TreeConfig.ClickAutomationElement(editor);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                Keyboard.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                IDE_Main.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nCopy ")).AsButton().Click();
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                string copiedText = Clipboard.GetText();
                if (copiedText.ElementAt(0) != '<') {
                    TreeConfig.ClickAutomationElement(editor);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                    Keyboard.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.HOME);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                    Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.DELETE);
                }
                else {
                    emptyline = false;
                    TreeConfig.ClickAutomationElement(editor);
                }
            }
        }
        public void GenerateProgram(string Name, bool AB = false, bool ANSIC = false, bool ANSICPP = false, bool CFC = false, bool CNC = false, bool FBD = false, bool IL = false, bool LD = false, bool reACTION = false, bool Robot = false, bool SFC = false, bool STOOP = false, bool ST = false, bool AllInOne = false) {
            SwitchView(TreeConfig.ViewType.LogicalView);
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, null, null, out var e);
            if (AB)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "AB Program" + (AllInOne?" All In One":""));
            if (ANSIC)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "ANSI C Program" + (AllInOne?" All In One":""));
            if (ANSICPP)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "ANSI C++ Program" + (AllInOne?" All In One":""));
            if (CFC)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "CFC Program");
            if (CNC)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "CNC Program");
            if (FBD)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "FBD Program");
            if (IL)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "IL Program" + (AllInOne?" All In One":""));
            if (LD)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "LD Program");
            if (reACTION)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "reACTION Diagram Program");
            if (Robot)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Robot Program");
            if (SFC)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "SFC Program");
            if (STOOP)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "ST OOP Program" + (AllInOne?" All In One":""));
            if (ST)
                InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "ST Program" + (AllInOne?" All In One":""));
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_Program"}, new List<string> { "_Object Name" }, out e, program:true);
            Mouse.RightClick();
            TreeConfig.ClickContextMenuItem(MainWindow, "Rename");
            Keyboard.Type(Name);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            string s = e.Name;
            s = Name + s.Substring(s.IndexOf("::"));
            e.Rename(s);
        }
        public void GenerateVariables(Object o, out string [][] strings, string package = "") {
            Editor e;
            string [] sout;
            strings = new string[((Array) o).Length][];
            if (package == string.Empty)
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_Global.var"}, new List<string> { "_Object Name" }, out e);
            else
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_" + package, "BR_Variables.var"}, new List<string> { "_Object Name", "_Object Name" }, out e, Editorname:package + "::" + "Variables.var");
            Mouse.Click(e.ConfigWorkspace.BoundingRectangle.Center());
            AutomationElement configTree = e.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            Button newVariable = e.ConfigWorkspace.FindFirstChild(cf => cf.ByName("Variable Declaration")).FindFirstChild(cf => cf.ByName("Add Variable")).AsButton();
            int i = 0;
            foreach (Object obj in (Array) o) {
                Object ob;
                bool isArray = false;
                string arrayLimits = "";
                if (obj is Array array) {
                    ob = array.GetValue(0);
                    isArray = true;
                    arrayLimits = "[0.." + array.Length + "]";
                }
                else
                    ob = obj;
                sout = new string[2];
                newVariable.Click();
                sout[0] = obj.GetType().ToString().Replace('.', '_') + "_" + (isArray?"a":"") + i;
                if (isArray)
                    sout[0] = sout[0].Substring(0, sout[0].IndexOf('[')) + sout[0].Substring(sout[0].IndexOf(']') + 1);
                Keyboard.Type(sout[0]);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
                AutomationElement varVar = configTree.FindFirstChild(cf => cf.ByName("BR_" + sout[0]));
                AutomationElement varType = varVar.FindFirstChild(cf => cf.ByName("BR_" + sout[0] + "_Type"));
                varType.Click();
                if (ob is byte) {   //USINT
                    sout[1] = "USINT";
                } else if (ob is sbyte) { 
                    Keyboard.Type(sout[1] = "SINT");
                } else if (ob is ushort) {
                    Keyboard.Type(sout[1] = "UINT");
                } else if (ob is short) {
                    Keyboard.Type(sout[1] = "INT");
                } else if (ob is uint) {
                    Keyboard.Type(sout[1] = "UDINT");
                } else if (ob is int) {
                    Keyboard.Type(sout[1] = "DINT");
                } else if (ob is float) {
                    Keyboard.Type(sout[1] = "REAL");
                } else if (ob is double) {
                    Keyboard.Type(sout[1] = "LREAL");
                } else if (ob is bool) {
                    Keyboard.Type(sout[1] = "BOOL");
                } else if (ob is DateTime) {
                    Keyboard.Type(sout[1] = "DT");
                }
                if (isArray)
                    Keyboard.Type(arrayLimits);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                strings[i] = sout;
                i++;
            }
        }
        public AutomationElement GetWorkspaceToolbar(string WindowSubString) {
            AutomationElement ConfigWorkspaceWindow = Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(WindowSubString) >= 0);
            return ConfigWorkspaceWindow.FindAllChildren().First(cf => cf.ClassName.IndexOf("ToolBar") >= 0);
        }
        public AutomationElement GetWorkspaceToolbar(Editor editor) {
            return editor.ConfigWorkspace.FindAllChildren().First(cf => cf.ClassName.IndexOf("ToolBar") >= 0);
        }
        public AutomationElement GetWorkspaceConfigRoot(string WindowSubString, string ElementName) {
            AutomationElement ConfigWorkspaceWindow = Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(WindowSubString) >= 0);
            AutomationElement configTree = ConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            return configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName(ElementName)));
        }
        public AutomationElement GetWorkspaceConfigRoot(Editor editor, string ElementName) {
            AutomationElement configTree = editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            return configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName(ElementName)));
        }

        public class Editor {
            public AutomationElement ConfigWorkspace, Tab;
            public string Name;
            public Editor Open(string name) {
                Name = name;
                ConfigWorkspace = Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(name) >= 0);
                AutomationElement TabList = Workspace.FindFirstChild(cf => cf.ByControlType(ControlType.Tab));
                try {Tab = TabList.FindAllChildren(cf => cf.ByControlType(ControlType.TabItem)).First(cf => cf.Name.IndexOf(name) >= 0);} catch (Exception) {}
                return this;
            }
            public Editor Rename(string name) {
                Editor ret = new Editor().Open(name);
                Editors.Add(ret);
                Editors.Remove(this);
                return ret;
            } 
            public void Restore() {
                AutomationElement TabList = Workspace.FindFirstChild(cf => cf.ByControlType(ControlType.Tab));
                try {Tab = TabList.FindAllChildren(cf => cf.ByControlType(ControlType.TabItem)).First(cf => cf.Name.IndexOf(Name) >= 0);} catch (Exception) {}
                if (Tab == null) {
                    Button tabs = TabList.FindFirstChild(cf => cf.ByControlType(ControlType.Button)).AsButton();
                    tabs.Click();
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(800));
                    Menu m = MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu)).AsMenu();
                    AutomationElement toolBar = m.FindFirstChild(cf => cf.ByControlType(ControlType.ToolBar));
                    MenuItem mi = toolBar.FindAllChildren(cf => cf.ByControlType(ControlType.MenuItem)).First(cf => cf.Name.IndexOf(Name) >= 0).AsMenuItem();
                    mi.Click();
                }
                else
                    Tab.Click();
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(800));
            }
            public void Close() {
                Restore();
                AutomationElement TabList = Workspace.FindFirstChild(cf => cf.ByControlType(ControlType.Tab));
                try {Tab = TabList.FindAllChildren(cf => cf.ByControlType(ControlType.TabItem)).First(cf => cf.Name.IndexOf(Name) >= 0);} catch (Exception) {}
                TreeConfig.IdeMain.Save();
                Rectangle rec;
                Point point;
                if (Tab == null) { //first TabItem doesn't show up in this list
                    TabList = Workspace.FindFirstChild(cf => cf.ByControlType(ControlType.Tab));
                    Tab = TabList.FindFirstChild(cf => cf.ByControlType(ControlType.TabItem));
                    rec = Tab.BoundingRectangle;
                    point = new Point {X = rec.Left - 10, Y = rec.Top + 10};
                }
                else {
                    rec = Tab.BoundingRectangle;
                    point = new Point {X = rec.Right - 10, Y = rec.Top + 10};
                }
                Mouse.MoveTo(point);
                Mouse.Click();
                Editors.Remove(this);
            }
            public static Editor OpenOrAttach(string Name) {
                Editor e = Editors.Find(x => x.Name == Name);
                if (e == null)
                    Editors.Add(e = new Editor().Open(Name));
                return e;
            }
        }    
    }
}