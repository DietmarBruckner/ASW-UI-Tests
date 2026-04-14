using System;
using System.Linq;
using System.Collections.Generic;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.UIA2;
using Menu = FlaUI.Core.AutomationElements.Menu;
using System.Drawing;
using Xunit.Sdk;

namespace FlaUITests.Util {
    public class IDE_Main {
        public Application App { get; private set; }
        private UIA2Automation _automation;
        public Window MainWindow { get; private set; }
        private ConditionFactory _cf;
        public enum ViewType { LogicalView, ConfigurationView, PhysicalView }
        private Menu _fileMenu; 
        private Menu _editMenu;
        private Menu _viewMenu;
        private Menu _openMenu;
        private Menu _projectMenu;
        private Menu _debugMenu;
        private Menu _onlineMenu;
        private Menu _toolsMenu;
        private Menu _windowMenu;
        private Menu _helpMenu;
        private Dictionary<string, Menu> _menus;
        public AutomationElement ProjectExplorer { get; private set; }
        public AutomationElement Toolbox { get; private set; }
        public AutomationElement PropertyWindow { get; private set; }
        public AutomationElement OutputWindow { get; private set; }
        public AutomationElement StatusBar { get; private set; }
        private TitleBar _titleBar;
        private AutomationElement _toolBars;
        private AutomationElement _standardToolBar;
        private AutomationElement _buildToolBar;
        private AutomationElement _onlineToolBar;
        private AutomationElement _unittestToolBar;
        private AutomationElement _editToolBar;
        private AutomationElement _formatToolBar;
        private AutomationElement _zoomToolBar;
        private AutomationElement _debugToolBar;
        public Dictionary<string, Rectangle> UIElementsBounds { get {
                AutomationElement a;
                Dictionary<string, Rectangle> bounds = new Dictionary<string, Rectangle> {
                    { "MainWindow", MainWindow.BoundingRectangle }
                };
                if ((a = MainWindow.TitleBar) != null)
                    bounds.Add("TitleBar", a.BoundingRectangle);
                if ((a = MainWindow.FindFirstDescendant(_cf.Menu()).AsMenu()) != null)
                    bounds.Add("Menus", a.BoundingRectangle);
                if ((a = MainWindow.FindFirstDescendant(_cf.ByControlType(ControlType.Pane).And(_cf.ByAutomationId("59419")))) != null)
                    bounds.Add("ToolBar", a.BoundingRectangle);
                if (ProjectExplorer != null)
                    bounds.Add("ProjectExplorer", ProjectExplorer.BoundingRectangle);
                if (Toolbox != null)
                    bounds.Add("Toolbox", Toolbox.BoundingRectangle);
                if (PropertyWindow != null)
                    bounds.Add("PropertyWindow", PropertyWindow.BoundingRectangle);
                if (OutputWindow != null)
                    bounds.Add("OutputWindow", OutputWindow.BoundingRectangle);
                if ((a = MainWindow.FindAllChildren(_cf.ByControlType(ControlType.StatusBar))[0]) != null)
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
            Init();
        }
        void Init() {
            Menu menu;
            if ((menu = MainWindow.FindFirstDescendant(_cf.Menu()).AsMenu()) == null) {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            AutomationElement[] menus = menu.FindAllDescendants();
            // Initialize all menu items
            _fileMenu = menus[0].AsMenu();
            _editMenu = menus[1].AsMenu();
            _viewMenu = menus[2].AsMenu();
            _openMenu = menus[3].AsMenu();
            _projectMenu = menus[4].AsMenu();
            _debugMenu = menus[5].AsMenu();
            _onlineMenu = menus[6].AsMenu();
            _toolsMenu = menus[7].AsMenu();
            _windowMenu = menus[8].AsMenu();
            _helpMenu = menus[9].AsMenu();
            _menus = new Dictionary<string, Menu>
            {
                {"File", _fileMenu},
                {"Edit", _editMenu},
                {"View", _viewMenu},
                {"Open", _openMenu},
                {"Project", _projectMenu},
                {"Debug", _debugMenu},
                {"Online", _onlineMenu},
                {"Tools", _toolsMenu},
                {"Window", _windowMenu},
                {"Help", _helpMenu}
            };
            AutomationElement[] allPanes = MainWindow.FindAllChildren(_cf.ByControlType(ControlType.StatusBar));
            if (allPanes.Length > 0)
                StatusBar = allPanes[0];
            while (StatusBar.Name.IndexOf("Opening", StringComparison.OrdinalIgnoreCase) >= 0);
            allPanes = MainWindow.FindAllChildren(_cf.ByControlType(ControlType.Pane));
            foreach (AutomationElement a in allPanes) {
                string name = a.Name;
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
                    AutomationElement[] children = a.FindAllChildren();
                    if(children.Any(c => c.ControlType == ControlType.ToolBar)) {
                        _toolBars = a;
                        foreach (AutomationElement child in children) {
                            string childName = child.Name;
                            if (childName == null) continue;
                            if (childName.IndexOf("Standard", StringComparison.OrdinalIgnoreCase) >= 0)
                                _standardToolBar = child;
                            else if (childName.IndexOf("Build", StringComparison.OrdinalIgnoreCase) >= 0)
                                _buildToolBar = child;
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
            Console.WriteLine("Application opened successfully. Main elements initialized.");
            
        }
        public void InvokeMenuItem(Menu menu, string menuItemName, string subMenuItemName = null) {
            string nameMenu = menu.Name.Substring(3, menu.Name.Length - 3); // Remove the trailing 'BR&' from the menu name
            int i = 3;
            while (i-- > 0) {
                try {
                    menu.Click(); // Click the menu to open it
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
                    Console.WriteLine("Error while trying to click " + menuItemName + " in menu " + nameMenu + ((subMenuItemName != null)? " in submenu " + subMenuItemName : "") + ".");
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
            if (_menus.ContainsKey(menuName)) {
                return _menus[menuName];
            }
            return null;
        }
        public Window GetModalWindow(String name) {
            return MainWindow.ModalWindows.FirstOrDefault(w => w.Title.Contains(name));
        }
        public void InitializeViews(bool projectExplorer = false, bool toolbox = false, bool propertyWindow = false, bool outputResults = false, bool statusBar = false) {
            if (projectExplorer) {
                ProjectExplorer = MainWindow.FindAllChildren(cf => cf.ByControlType(ControlType.Pane)).FirstOrDefault(c => c.Name.IndexOf("View", StringComparison.OrdinalIgnoreCase) >= 0);
                if (ProjectExplorer == null) {
                    InvokeMenuItem(GetMenu("View"), "Project Explorer", "Logical View");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    ProjectExplorer = MainWindow.FindAllChildren(cf => cf.ByControlType(ControlType.Pane)).FirstOrDefault(c => c.Name.IndexOf("View", StringComparison.OrdinalIgnoreCase) >= 0);
                }
            }
            if (toolbox) {
                Toolbox = MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Toolbox")));
                if (Toolbox == null) {
                    InvokeMenuItem(GetMenu("View"), "Toolbox");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                    Toolbox = MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Toolbox")));
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
        public void MakeToolBoxElementsVisible(bool categories) {
            Rectangle splitviewRect = UIElementsBounds["Toolbox"];
            AutomationElement a = Toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByAutomationId("_splitContainer")));
            AutomationElement [] allChildren = a.FindAllChildren();
            Rectangle categoriesListViewRect = allChildren[0].FindAllDescendants().First(c => c.AutomationId == "_categoriesListView").BoundingRectangle;
            Rectangle elementsListViewRect = allChildren[1].FindAllDescendants().First(c => c.AutomationId == "_elementsListView").BoundingRectangle;
            //min size of 250 px (or height of categories list + 50) height and 400 px width for the Toolbox to ensure all elements are visible and can be clicked
            if (splitviewRect.Height <= 250 || splitviewRect.Width <= 400 || splitviewRect.Height <= categoriesListViewRect.Height + 50) {
                Console.WriteLine("Toolbox size too small to make toolbox elements visible - trying to make it bigger.");
                Point point = new Point { X = splitviewRect.Left - 1, Y = categoriesListViewRect.Top + 30 };
                Mouse.MoveTo(point);
                Mouse.DragHorizontally(point, splitviewRect.Width - 400);
                point = new Point { X = splitviewRect.Left + 30, Y = splitviewRect.Bottom + 1};
                Mouse.MoveTo(point);
                Mouse.DragVertically(point, Math.Max(250, categoriesListViewRect.Height + 50) - splitviewRect.Height);
            }
            //min size of 200 px height and 400 px width for the Categories list to ensure all elements are visible and can be clicked
            if (categories) {
                if (categoriesListViewRect.Height <= 100) {
                    Console.WriteLine("Categories list size too small to make elements visible - trying to make it bigger.");
                    Point point = new Point { X = categoriesListViewRect.Left + 30, Y = categoriesListViewRect.Bottom + 1};
                    Mouse.MoveTo(point);
                    Mouse.DragVertically(point, 100 - categoriesListViewRect.Height);
                }
            }
            else {
                if (elementsListViewRect.Height <= 100) {
                    Console.WriteLine("Elements list size too small to make elements visible - trying to make it bigger.");
                    Point point = new Point { X = categoriesListViewRect.Left + 30, Y = categoriesListViewRect.Bottom + 1};
                    Mouse.MoveTo(point);
                    Mouse.DragVertically(point, elementsListViewRect.Height - 100);
                }
            }
        }
        public void SearchToolBox(string searchTerm) {
            AutomationElement searchTextBox = Toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("searchTermTextBox")));
            if (searchTextBox.Name != searchTerm) {
                Point point = new Point { X = searchTextBox.BoundingRectangle.Left - 10, Y = searchTextBox.BoundingRectangle.Top + searchTextBox.BoundingRectangle.Height / 2 };
                Mouse.LeftClick(point);
                point = point = new Point { X = searchTextBox.BoundingRectangle.Left+ searchTextBox.BoundingRectangle.Width / 2, Y = searchTextBox.BoundingRectangle.Top + searchTextBox.BoundingRectangle.Height / 2 };
                Mouse.LeftClick(point);
                foreach (char ch in searchTerm) {
                    Keyboard.Type(ch);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                }
            }
        }
        public void WaitParsing() {
            InvokeMenuItem(GetMenu("View"), "Go To", "Output Results");
            bool done = false;
            while (!done) {
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                AutomationElement outputListView = OutputWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("outputListView")));
                AutomationElement [] allMessages = outputListView.FindAllDescendants(cf => cf.ByControlType(ControlType.DataItem));
                AutomationElement [] allMessagesTexts = allMessages[allMessages.Length - 1].FindAllDescendants(cf => cf.ByControlType(ControlType.Text));
                if (allMessagesTexts.Any(m => m.Name.IndexOf("Parsing finished", StringComparison.OrdinalIgnoreCase) >= 0))
                    done = true;
            }
        }
        public void SwitchView(ViewType view) {
            Point point;
            Rectangle projectExplorerRect = ProjectExplorer.BoundingRectangle;
            if (projectExplorerRect.Width <= 400) {
                Console.WriteLine("Project Explorer size too small - trying to make it bigger.");
                point = new Point { X = projectExplorerRect.Right + 1, Y = projectExplorerRect.Top + 30};
                Mouse.MoveTo(point);
                Mouse.DragHorizontally(point, 400 - projectExplorerRect.Width);
            }
            if (projectExplorerRect.Height <= 400) {
                Console.WriteLine("Project Explorer size too small - trying to make it bigger.");
                point = new Point { X = projectExplorerRect.Left + 30, Y = projectExplorerRect.Bottom + 1};
                Mouse.MoveTo(point);
                Mouse.DragVertically(point, 400 - projectExplorerRect.Height);
            }
            AutomationElement ViewTab = null;
            switch (view) {
                case ViewType.LogicalView:
                    ViewTab = ProjectExplorer.FindAllDescendants(cf => cf.ByControlType(ControlType.TabItem).And(cf.ByName("Logical View"))).FirstOrDefault();
                    break;
                case ViewType.ConfigurationView:
                    ViewTab = ProjectExplorer.FindAllDescendants(cf => cf.ByControlType(ControlType.TabItem).And(cf.ByName("Configuration View"))).FirstOrDefault();
                    break;
                case ViewType.PhysicalView:
                    ViewTab = ProjectExplorer.FindAllDescendants(cf => cf.ByControlType(ControlType.TabItem).And(cf.ByName("Physical View"))).FirstOrDefault();
                    break;
            }
            point = new Point { X = ViewTab.BoundingRectangle.Left + ViewTab.BoundingRectangle.Width / 2, Y = ViewTab.BoundingRectangle.Top + ViewTab.BoundingRectangle.Height / 2 };
            Mouse.LeftClick(point);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
        }
        public AutomationElement GetActiveConfigurtion() {
            SwitchView(ViewType.ConfigurationView);
            AutomationElement treeElement = ProjectExplorer.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree).And(cf.ByAutomationId("ConfigurationTree")));
            AutomationElement [] allConfigurations = treeElement.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem));
            return allConfigurations.First(cf => cf.Name.IndexOf("[Active]", StringComparison.OrdinalIgnoreCase) >= 0) ?? throw new Exception("Active configuration not found");
        }
        public AutomationElement GetLogicalViewRoot(AppProject project) {
            SwitchView(ViewType.LogicalView);
            AutomationElement [] allTreeItems = ProjectExplorer.FindAllDescendants(cf => cf.ByControlType(ControlType.TreeItem));
            AutomationElement a = ProjectExplorer.FindFirstDescendant(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_" + project.Name)));
            return a;
        }
    }
}