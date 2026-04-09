using System;
using System.Drawing;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.UIA2;
using Menu = FlaUI.Core.AutomationElements.Menu;

namespace FlaUITests {
    public class AutomationStudio6 {
        private Application _app;
        private UIA2Automation _automation;
        private Window _mainWindow;
        private ConditionFactory _cf;
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

        // Common AS6 main window elements
        private AutomationElement _views;
        private AutomationElement _toolbox;
        private AutomationElement _propertyWindow;
        private AutomationElement _outputWindow;
        private AutomationElement _statusBar;
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
        public bool IsProjectLoaded() {
            return _titleBar != null && !string.IsNullOrEmpty(_titleBar.Name) && _titleBar.Name.IndexOf("Automation Studio", StringComparison.OrdinalIgnoreCase) >= 10;
         }
        public void InvokeMenuItem(Menu menu, string menuItemName) {
            string nameMenu = menu.Name.Substring(3, menu.Name.Length - 3); // Remove the trailing 'M' from the menu name
            int i = 3;
            while (i-- > 0) {
                try {
                    menu.Click(); // Click the menu to open it
                    Menu m = _mainWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Menu).And(cf.ByName(nameMenu))).AsMenu();
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
                    break;
                }
                catch (Exception) {
                    Console.WriteLine("Error while trying to click " + menuItemName + " in menu " + nameMenu + ".");
                }
            }
        }
        public void CloseProject() {
            if (IsProjectLoaded()) {
                string[] paths = Projectpath();
                InvokeMenuItem(_fileMenu, "Close Project");
                Console.WriteLine("Project " + paths[2] + " closed.");
            }
        }
        public void OpenProject(string projectPath) {
            InvokeMenuItem(_fileMenu, "Open Project...");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1)); // Wait for the Open Project dialog to appear
            Window openProjectDialog = _mainWindow.ModalWindows.FirstOrDefault(w => w.Title.Contains("Open"));
            if (openProjectDialog != null) {
                AutomationElement pane3 = openProjectDialog.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByAutomationId("40965")));
                AutomationElement comboBox = pane3.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByAutomationId("41477")));
                ProgressBar progressBar = comboBox.FindFirstChild(cf => cf.ByControlType(ControlType.ProgressBar)).AsProgressBar();
                AutomationElement pane = progressBar.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
                Point point = new Point { X = pane.BoundingRectangle.Left+ pane.BoundingRectangle.Width / 2, Y = pane.BoundingRectangle.Top + pane.BoundingRectangle.Height / 2 };
                Mouse.LeftClick(point);
                Keyboard.Type(projectPath + "\n");
                AutomationElement pane1 = openProjectDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Shell Folder View")));
                AutomationElement fileList = pane1.FindFirstChild(cf => cf.ByControlType(ControlType.List));
                AutomationElement [] children = fileList.FindAllChildren();
                AutomationElement targetItem = children.FirstOrDefault(c => c.Name.Contains(".apj"));
                targetItem.DoubleClick();
                Console.WriteLine("Project " + projectPath + " opened.");
            }
        }
        private string[] Projectpath()
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
            return new string[] { folder, configString, projectString };
        }
        public void ReadProject() {
            if (IsProjectLoaded()) {
                string[] paths = Projectpath();
                string folder = paths[0];
                string configString = paths[1];
                HardwareTopology hardwareTopology = new HardwareConfigReader(folder, configString).ReadHardwareTopology();
             }
        }
        /// <summary>
        /// Opens the Automation Studio 6 application and initializes all IDE items
        /// </summary>
        public void OpenApplication() {
            _app = Application.Launch(@"C:\Program Files (x86)\BRAutomation\AS6\bin-en\pg.exe");
            _app.WaitWhileMainHandleIsMissing(TimeSpan.FromSeconds(20));
            _app.WaitWhileBusy(TimeSpan.FromSeconds(20));

            _automation = new UIA2Automation();
            _mainWindow = _app.GetMainWindow(_automation);
            _cf = new ConditionFactory(new UIA2PropertyLibrary());

            Menu menu;
            if ((menu = _mainWindow.FindFirstDescendant(_cf.Menu()).AsMenu()) == null) {
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

            AutomationElement[] allPanes = _mainWindow.FindAllChildren(_cf.ByControlType(ControlType.Pane));
            foreach (AutomationElement a in allPanes) {
                string name = a.Name;
                if (name == null) continue;
                if (name != "") {
                    if (name.IndexOf("View", StringComparison.OrdinalIgnoreCase) >= 0)
                        _views = a;
                    else if (name.IndexOf("Toolbox", StringComparison.OrdinalIgnoreCase) >= 0)
                        _toolbox = a;
                    else if (name.IndexOf("Property", StringComparison.OrdinalIgnoreCase) >= 0)
                        _propertyWindow = a;
                    else if (name.IndexOf("Output", StringComparison.OrdinalIgnoreCase) >= 0)
                        _outputWindow = a;
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
            allPanes = _mainWindow.FindAllChildren(_cf.ByControlType(ControlType.StatusBar));
            if (allPanes.Length > 0)
                _statusBar = allPanes[0];
            _titleBar = _mainWindow.TitleBar;
            Console.WriteLine("Application opened successfully. Main elements initialized.");
        }
        /// <summary>
        /// Closes the Automation Studio 6 application
        /// </summary>
        public void CloseApplication()
        {
            if (_app != null)
            {
                _app.Close();
                Console.WriteLine("Application closed successfully.");
            }
            else
            {
                Console.WriteLine("Warning: Application was not open.");
            }
        }
        /// <summary>
        /// Gets the main window of the application
        /// </summary>
        public Window GetMainWindow()
        {
            return _mainWindow;
        }
        /// <summary>
        /// Gets the automation element
        /// </summary>
        public UIA2Automation GetAutomation()
        {
            return _automation;
        }
        public Application GetApplication()
        {
            return _app;
        }
    }
}
