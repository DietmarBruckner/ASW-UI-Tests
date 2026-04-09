using System;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA2;
using Menu = FlaUI.Core.AutomationElements.Menu;

namespace FlaUITests {
    public class AutomationStudio6 {
        private Application _app;
        private UIA2Automation _automation;
        private Window _mainWindow;
        private ConditionFactory _cf;
        public Menu FileMenu { get; private set; }
        public Menu EditMenu { get; private set; }
        public Menu ViewMenu { get; private set; }
        public Menu OpenMenu { get; private set; }
        public Menu ProjectMenu { get; private set; }
        public Menu DebugMenu { get; private set; }
        public Menu OnlineMenu { get; private set; }
        public Menu ToolsMenu { get; private set; }
        public Menu WindowMenu { get; private set; }
        public Menu HelpMenu { get; private set; }

        // Common AS6 main window elements
        public AutomationElement Views { get; private set; }
        public AutomationElement Toolbox { get; private set; }
        public AutomationElement PropertyWindow { get; private set; }
        public AutomationElement OutputWindow { get; private set; }
        public AutomationElement StatusBar { get; private set; }
        public TitleBar TitleBar { get; private set; }
        public AutomationElement ToolBars { get; private set; }
        public AutomationElement StandardToolBar { get; private set; }
        public AutomationElement BuildToolBar { get; private set; }
        public AutomationElement OnlineToolBar { get; private set; }
        public AutomationElement UnittestToolBar { get; private set; }
        public AutomationElement EditToolBar { get; private set; }
        public AutomationElement FormatToolBar { get; private set; }
        public AutomationElement ZoomToolBar { get; private set; }
        public AutomationElement DebugToolBar { get; private set; }

        public bool IsProjectLoaded() {
            return TitleBar != null && !string.IsNullOrEmpty(TitleBar.Name) && TitleBar.Name.IndexOf("Automation Studio", StringComparison.OrdinalIgnoreCase) >= 10;
         }
        public void closeProject() {
            if (IsProjectLoaded()) {
                FileMenu.Click(); // Click File menu
                Menu newFileMenu = _mainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Menu).And(cf.ByName("File"))).AsMenu();
                MenuItem closeProjectMenuItem = null;
                while((closeProjectMenuItem = newFileMenu.FindFirstDescendant(cf => cf.ByControlType(ControlType.MenuItem).And(cf.ByName("Close Project"))).AsMenuItem()) == null) {
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3)); // Wait for the Close Project menu item to appear
                    FileMenu.Click(); // Click File menu again to refresh the menu items
                }   
                closeProjectMenuItem.Click(); // Click Close Project
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1)); // Wait for the project to close
            }
        }
        public void readProject() {
            String titleString = TitleBar.Name;
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
            HardwareTopology hardwareTopology = new HardwareConfigReader(folder, configString).ReadHardwareTopology();
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
            FileMenu = menus[0].AsMenu();
            EditMenu = menus[1].AsMenu();
            ViewMenu = menus[2].AsMenu();
            OpenMenu = menus[3].AsMenu();
            ProjectMenu = menus[4].AsMenu();
            DebugMenu = menus[5].AsMenu();
            OnlineMenu = menus[6].AsMenu();
            ToolsMenu = menus[7].AsMenu();
            WindowMenu = menus[8].AsMenu();
            HelpMenu = menus[9].AsMenu();

            AutomationElement[] allPanes = _mainWindow.FindAllChildren(_cf.ByControlType(ControlType.Pane));
            foreach (AutomationElement a in allPanes) {
                string name = a.Name;
                if (name == null) continue;
                if (name != "") {
                    if (name.IndexOf("View", StringComparison.OrdinalIgnoreCase) >= 0)
                        Views = a;
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
                        ToolBars = a;
                        foreach (AutomationElement child in children) {
                            string childName = child.Name;
                            if (childName == null) continue;
                            if (childName.IndexOf("Standard", StringComparison.OrdinalIgnoreCase) >= 0)
                                StandardToolBar = child;
                            else if (childName.IndexOf("Build", StringComparison.OrdinalIgnoreCase) >= 0)
                                BuildToolBar = child;
                            else if (childName.IndexOf("Online", StringComparison.OrdinalIgnoreCase) >= 0)
                                OnlineToolBar = child;
                            else if (childName.IndexOf("Unit", StringComparison.OrdinalIgnoreCase) >= 0)
                                UnittestToolBar = child;
                            else if (childName.IndexOf("Edit", StringComparison.OrdinalIgnoreCase) >= 0)
                                EditToolBar = child;
                            else if (childName.IndexOf("Format", StringComparison.OrdinalIgnoreCase) >= 0)
                                FormatToolBar = child;
                            else if (childName.IndexOf("Zoom", StringComparison.OrdinalIgnoreCase) >= 0)
                                ZoomToolBar = child;
                            else if (childName.IndexOf("Debug", StringComparison.OrdinalIgnoreCase) >= 0)
                                DebugToolBar = child;
                        }
                    }
                }
            }
            allPanes = _mainWindow.FindAllChildren(_cf.ByControlType(ControlType.StatusBar));
            if (allPanes.Length > 0)
                StatusBar = allPanes[0];
            TitleBar = _mainWindow.TitleBar;
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
