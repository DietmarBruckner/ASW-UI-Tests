using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA2;
using Menu = FlaUI.Core.AutomationElements.Menu;
using System.Drawing;
using Tesseract;
using FlaUI.Core.Capturing;
using Microsoft.VisualStudio.TestTools.UITesting;
using Mouse = FlaUI.Core.Input.Mouse;
using Keyboard = FlaUI.Core.Input.Keyboard;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Windows.Forms;
using FlaUI.Core.Tools;
using Application = FlaUI.Core.Application;
using Button = FlaUI.Core.AutomationElements.Button;
using MenuItem = FlaUI.Core.AutomationElements.MenuItem;
using TextBox = FlaUI.Core.AutomationElements.TextBox;
using System.Runtime.CompilerServices;

namespace FlaUITests.Util {
    public class IDE_Main {
        public Application App { get; private set; }
        private readonly UIA2Automation _automation;
        public Window MainWindow { get; private set; }
        private readonly ConditionFactory _cf;
        private Menu _fileMenu, _editMenu, _viewMenu, _insertMenu, _openMenu, _projectMenu, _debugMenu, _onlineMenu, _toolsMenu, _windowMenu, _helpMenu;
        private Dictionary<string, Menu> MenuNames { get {
            Dictionary<string, Menu> dm = new Dictionary<string, Menu> {
                {"File", _fileMenu}, {"Edit", _editMenu}, {"View", _viewMenu}, {"Insert", _insertMenu}, {"Open", _openMenu}, {"Project", _projectMenu}, {"Debug", _debugMenu}, {"Online", _onlineMenu}, {"Tools", _toolsMenu}, {"Window", _windowMenu}, {"Help", _helpMenu}};
            return dm;
        } }
        public AutomationElement ProjectExplorer { get; private set; }
        public AutomationElement Toolbox { get; private set; }
        public AutomationElement PropertyWindow { get; private set; }
        public AutomationElement OutputWindow { get; private set; }
        public AutomationElement StatusBar { get; private set; }
        public AutomationElement Workspace { get; private set; }
        private TitleBar _titleBar;
        private AutomationElement _toolBars;
        public AutomationElement ToolBarStandard { get; private set; }
        public AutomationElement ToolBarBuild { get; private set; }
        private AutomationElement _onlineToolBar;
        private AutomationElement _unittestToolBar;
        private AutomationElement _editToolBar;
        private AutomationElement _formatToolBar;
        private AutomationElement _zoomToolBar;
        private AutomationElement _debugToolBar;
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
                if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                    Console.WriteLine("Maximizing main window");
                MainWindow.TitleBar.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Maximize"))).AsButton().Invoke();
            }

        }
        void InitMenues() {
            Menu menu = MainWindow.FindFirstDescendant(_cf.Menu()).AsMenu();
            AutomationElement[] menus = menu.FindAllDescendants();
            foreach (AutomationElement m in menus) {
                string name = m.Name;
                if (name == null) continue;
                if (name.IndexOf("File", StringComparison.OrdinalIgnoreCase) >= 0)
                    _fileMenu = m.AsMenu();
                else if (name.IndexOf("Edit", StringComparison.OrdinalIgnoreCase) >= 0)
                    _editMenu = m.AsMenu();
                else if (name.IndexOf("View", StringComparison.OrdinalIgnoreCase) >= 0)
                    _viewMenu = m.AsMenu();
                else if (name.IndexOf("Insert", StringComparison.OrdinalIgnoreCase) >= 0)
                    _insertMenu = m.AsMenu();
                else if (name.IndexOf("Open", StringComparison.OrdinalIgnoreCase) >= 0)
                    _openMenu = m.AsMenu();
                else if (name.IndexOf("Project", StringComparison.OrdinalIgnoreCase) >= 0)
                    _projectMenu = m.AsMenu();
                else if (name.IndexOf("Debug", StringComparison.OrdinalIgnoreCase) >= 0)
                    _debugMenu = m.AsMenu();
                else if (name.IndexOf("nline", StringComparison.OrdinalIgnoreCase) >= 0)
                    _onlineMenu = m.AsMenu();
                else if (name.IndexOf("Tools", StringComparison.OrdinalIgnoreCase) >= 0)
                    _toolsMenu = m.AsMenu();
                else if (name.IndexOf("Window", StringComparison.OrdinalIgnoreCase) >= 0)
                    _windowMenu = m.AsMenu();
                else if (name.IndexOf("Help", StringComparison.OrdinalIgnoreCase) >= 0)
                    _helpMenu = m.AsMenu();
            }
        }
        void Init() {
            InitMenues();
            AutomationElement[] allPanes = MainWindow.FindAllChildren(_cf.ByControlType(ControlType.StatusBar));
            if (allPanes.Length > 0)
                StatusBar = allPanes[0];
            while (StatusBar.Name.IndexOf("Opening", StringComparison.OrdinalIgnoreCase) >= 0);
            allPanes = MainWindow.FindAllChildren(_cf.ByControlType(ControlType.Pane));
            foreach (AutomationElement a in allPanes) {
                string name = a.Name;
                string autoId;
                try {autoId = a.AutomationId;} catch (Exception) { autoId = ""; }
                if (name == null) continue;
                if (name != "") {
                    if (name.IndexOf("View", StringComparison.OrdinalIgnoreCase) >= 0)
                        ProjectExplorer = a;
                    else if (name.IndexOf("Toolbox", StringComparison.OrdinalIgnoreCase) >= 0)
                        Toolbox = a;
                    else if (name.IndexOf("Property", StringComparison.OrdinalIgnoreCase) >= 0)
                        PropertyWindow = a;
                    else if (name.IndexOf("Output", StringComparison.OrdinalIgnoreCase) >= 0)
                        OutputWindow = a;
                }
                else {
                    if (autoId == "59648") {
                        Workspace = a;
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
            _titleBar = MainWindow.TitleBar;
            //if (TreeConfig.CurrentProject.verbose >= Environment.Verbose.LIGHT) {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Application opened successfully. Main elements initialized.");
                Console.WriteLine("------------------------------------------");
            //}
        }
        public void InvokeMenuItem(Menu menu, string menuItemName, string subMenuItemName = null) {
            string nameMenu = menu.Name.Substring(3, menu.Name.Length - 3); // Remove the trailing 'BR&' from the menu name
            if (nameMenu == "&nline")
                nameMenu = "Online";
            int i = 3;
            while (i-- > 0) {
                try {
                    menu.Click(); // Click the menu to open it
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(800));
                    Menu m = MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByName(nameMenu))).AsMenu();
                    AutomationElement toolBar = m.FindFirstChild(cf => cf.ByControlType(ControlType.ToolBar));
                    MenuItem mi = null;
                    bool notFound = true;
                    AutomationElement[] children = toolBar.FindAllChildren();
                    foreach (AutomationElement child in children) {
                        string name = child.Name;
                        if (name == null) continue;
                        if (name.IndexOf(menuItemName, StringComparison.OrdinalIgnoreCase) >= 0) {
                            mi = child.AsMenuItem();
                            notFound = false;
                            break;
                        }
                    }
                    if (notFound) 
                        continue; 
                    mi.Invoke();
                    if (subMenuItemName != null) {
                        Menu subMenu = MainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByName(menuItemName))).AsMenu();
                        toolBar = m.FindFirstChild(cf => cf.ByControlType(ControlType.ToolBar));
                        mi = null;
                        notFound = true;
                        AutomationElement[] subChildren = subMenu.FindAllChildren();
                        foreach (AutomationElement child in subChildren) {
                            string name = child.Name;
                            if (name == null) continue;
                            if (name.IndexOf(subMenuItemName, StringComparison.OrdinalIgnoreCase) >= 0) {
                                mi = child.AsMenuItem();
                                notFound = false;
                                break;
                            }
                        if (notFound) 
                            continue; 
                        mi.Invoke();}
                    }
                    break;
                }
                catch (Exception) {
                    if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.LIGHT) {
                        Console.WriteLine("Error while trying to click " + menuItemName + " in menu " + nameMenu + ((subMenuItemName != null)? " in submenu " + subMenuItemName : "") + ".");
                        Console.WriteLine("trys left: " + i);
                    }
                }
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
                if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                    Console.WriteLine("Waiting for window: " + name);
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            CheckResizeWindowWithinScreen(w);
            return w;
        }
        public void CheckResizeWindowWithinScreen (Window w) {
            Rectangle wbr = w.BoundingRectangle;
            Screen screen = Screen.FromHandle(w.Properties.NativeWindowHandle);
            Rectangle sr = screen.Bounds;
            bool isFullyVisible = wbr.Left >= sr.Left && wbr.Top >= sr.Top && wbr.Right <= sr.Right && wbr.Bottom <= sr.Bottom;
            if (!isFullyVisible) {
                if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                    Console.WriteLine("Window not fully visible - trying to make it fit screen.");
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
            if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine("Checking if necessary view(s) are there: " + (projectExplorer?"Project Explorer ":"") + (toolbox?"Object catalog ":"") + (propertyWindow?"Property window ":"") + (outputResults?"Output results ":"") + (statusBar?"Statusbar":""));
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
                PropertyWindow = MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Property Window")));
                if (PropertyWindow == null) {
                    InvokeMenuItem(GetMenu("View"), "Property Window");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    PropertyWindow = MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Property Window")));
                }
            }
            if (outputResults) {
                OutputWindow = MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Output Results")));
                if (OutputWindow == null) {
                    InvokeMenuItem(GetMenu("View"), "Output", "Output Results");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    OutputWindow = MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Output Results")));
                }
            }
            if (statusBar) {
                StatusBar = MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.StatusBar));
                if (StatusBar == null) {
                    InvokeMenuItem(GetMenu("View"), "Status Bar");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    StatusBar = MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.StatusBar));
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
                if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                    Console.WriteLine("Toolbox size too small to make toolbox elements visible - trying to make it bigger.");
                Point point = new Point { X = splitviewRect.Left + 30, Y = splitviewRect.Bottom + 1};
                //Mouse.MoveTo(point);
                Mouse.DragVertically(point, Math.Max(251, categoriesListViewRect.Height + 50) - splitviewRect.Height);
            }
            if (splitviewRect.Width < 400) {
                if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                    Console.WriteLine("Toolbox size too small to make toolbox elements visible - trying to make it bigger.");
                Point point = new Point { X = splitviewRect.Left - 1, Y = categoriesListViewRect.Top + 30 };
                //Mouse.MoveTo(point);
                Mouse.DragHorizontally(point, splitviewRect.Width - 401);
            }
            //min size of 200 px height and 400 px width for the Categories list to ensure all elements are visible and can be clicked
            if (categories) {
                if (categoriesListViewRect.Height < 100) {
                    if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                        Console.WriteLine("Categories list size too small to make elements visible - trying to make it bigger.");
                    Point point = new Point { X = categoriesListViewRect.Left + 30, Y = categoriesListViewRect.Bottom + 1};
                    //Mouse.MoveTo(point);
                    Mouse.DragVertically(point, 101 - categoriesListViewRect.Height);
                }
            }
            else {
                if (elementsListViewRect.Height < 100) {
                    if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                        Console.WriteLine("Elements list size too small to make elements visible - trying to make it bigger.");
                    Point point = new Point { X = categoriesListViewRect.Left + 30, Y = categoriesListViewRect.Bottom + 1};
                    //Mouse.MoveTo(point);
                    Mouse.DragVertically(point, elementsListViewRect.Height - 101);
                }
            }
        }
        public void SearchToolBox(string searchTerm) {
            if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine("Typing into Object catalog search field: " + searchTerm);
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
            if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine("Waiting for message: " + message + "; Timeout: " + timeout);
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
            if (!done && TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine("Waiting for message ran into timeout");
        }
        public void SwitchView(TreeConfig.ViewType view, int x = 400, int y = 400) {
            if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.LIGHT)
                Console.WriteLine("Switching to view: " + view.ToString());
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
                Console.WriteLine(view.ToString() + " size too thin - trying to make it broader.");
                point = new Point { X = dragX?Rect.Right + 1:Rect.Left-1, Y = Rect.Top + 30};
                Mouse.DragHorizontally(point, (x+1 - Rect.Width)*(dragX?1:-1));
            }
            if (Rect.Height < y) {
                Console.WriteLine(view.ToString() + " size too small - trying to make it taller.");
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
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
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
            AutomationElement toolBoxContextContent = Toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("_elementsListView")));
            AutomationElement desiredElementItem = toolBoxContextContent.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataItem).And(cf.ByName(objectName))) ?? throw new Exception(objectName + " element not found");
            if (drag)
                Mouse.Drag(desiredElementItem.BoundingRectangle.Center(), toDrag);
            else
                desiredElementItem.DoubleClick();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
        }
        public bool IsButtonActive(Button button, string image = "") {
            //string workingDirectory = System.Environment.CurrentDirectory;
            //Capture.Element(button).ToFile(             workingDirectory + "\\FlaUITests\\Util\\screenshots\\" + image + ".png");
            if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine("Checking if button: " + button.Name + " is activated");
            Color borderColor = Capture.Element(button).Bitmap.GetPixel(0,0);
            if (borderColor.Equals(Color.FromArgb(0, 120, 215)))
/*            Color blue = Color.Blue;
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
            while (StatusBar.Name.IndexOf("Builds", StringComparison.OrdinalIgnoreCase) >= 0);
            WaitForMessage("Build:");
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
                AutomationElement a = manageComponentsWindow.FindFirstChild(cf => cf.ByName(version));
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
            while ((manageComponentsWindow = MainWindow.ModalWindows.FirstOrDefault(x => x.Title.Contains(TreeConfig.CurrentProject.CPU + " - Properties"))) != null)
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            TreeConfig.ClickAutomationElement(TreeConfig.IdeMain.MainWindow.TitleBar);
        }
        public void InstallComponentVersion (string componentName, string version) {
        }
        public void SetIWorkspaceMinSize(AutomationElement docIATeditor = null) {
            Rectangle rect = UIElementsBounds["Workspace"];
            int Xscreen = (int) (_screen.WorkingArea.Width * 0.6);
            int Yscreen = (int) (_screen.WorkingArea.Height * 0.6);
            if (rect.Width < Xscreen) {
                if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                    Console.WriteLine("Workspace size too narrow - trying to make it broader.");
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
                if (TreeConfig.CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                    Console.WriteLine("Workspace size too small - trying to make it taller.");
                    Point point = new Point { X = rect.Left + 30, Y = rect.Bottom + 1};
                    Mouse.DragVertically(point, Yscreen + 1 - rect.Height);
            }
            if (docIATeditor is null) return;
            if (docIATeditor.Patterns.Scroll.Pattern.VerticalScrollPercent != 0d) {
                TreeConfig.ClickAutomationElement(docIATeditor);
                using (Keyboard.Pressing(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL)) {
                    do {
                        Mouse.Scroll(0.5d);
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    } while (docIATeditor.Patterns.Scroll.Pattern.VerticalScrollPercent != 0d);
                }
            }
            while (docIATeditor.Patterns.Scroll.Pattern.HorizontalScrollPercent != 0d) {
                rect = UIElementsBounds["Workspace"];
                Point point = new Point { X = rect.Left - 1, Y = rect.Bottom - 100};
                Mouse.DragHorizontally(point, 100);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                point = new Point { X = rect.Right + 1, Y = rect.Bottom - 100};
                Mouse.DragHorizontally(point, -100);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
        }
    }
}