using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.UIA2;
using Menu = FlaUI.Core.AutomationElements.Menu;

namespace TestProject1
{
    public class AutomationStudio6
    {
        private Application _app;
        private UIA2Automation _automation;
        private Window _mainWindow;
        private ConditionFactory _cf;

        // Menu items as global/class variables
        public Menu FileMenu { get; private set; }
        public Menu EditMenu { get; private set; }
        public Menu ViewMenu { get; private set; }
        public Menu InsertMenu { get; private set; }
        public Menu OpenMenu { get; private set; }
        public Menu ProjectMenu { get; private set; }
        public Menu DebugMenu { get; private set; }
        public Menu OnlineMenu { get; private set; }
        public Menu ToolsMenu { get; private set; }
        public Menu WindowMenu { get; private set; }
        public Menu HelpMenu { get; private set; }

        // Common AS6 main window elements
        public Pane Views { get; private set; }
        public Pane Toolbox { get; private set; }
        public Pane PropertyWindow { get; private set; }
        public Pane OutputWindow { get; private set; }
        public StatusBar StatusBar { get; private set; }
        public TitleBar TitleBar { get; private set; }
        public Pane ToolsBars { get; private set; }

        /// <summary>
        /// Opens the Automation Studio 6 application and initializes all IDE items
        /// </summary>
        public void OpenApplication()
        {
            _app = Application.Launch(@"C:\Program Files (x86)\BRAutomation\AS6\bin-en\pg.exe");
            _app.WaitWhileMainHandleIsMissing(TimeSpan.FromSeconds(20));
            _app.WaitWhileBusy(TimeSpan.FromSeconds(20));
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));

            _automation = new UIA2Automation();
            _mainWindow = _app.GetMainWindow(_automation);
            _cf = new ConditionFactory(new UIA2PropertyLibrary());

            var menu = _mainWindow.FindFirstDescendant(_cf.Menu()).AsMenu();
            var menus = menu.FindAllDescendants();

            // Initialize all menu items
            FileMenu = menus.ElementAtOrDefault(0)?.AsMenu();
            EditMenu = menus.ElementAtOrDefault(1)?.AsMenu();
            ViewMenu = menus.ElementAtOrDefault(2)?.AsMenu();
            InsertMenu = menus.ElementAtOrDefault(3)?.AsMenu();
            OpenMenu = menus.ElementAtOrDefault(4)?.AsMenu();
            ProjectMenu = menus.ElementAtOrDefault(5)?.AsMenu();
            DebugMenu = menus.ElementAtOrDefault(6)?.AsMenu();
            OnlineMenu = menus.ElementAtOrDefault(7)?.AsMenu();
            ToolsMenu = menus.ElementAtOrDefault(8)?.AsMenu();
            WindowMenu = menus.ElementAtOrDefault(9)?.AsMenu();
            HelpMenu = menus.ElementAtOrDefault(10)?.AsMenu();

            var descendants = _mainWindow.FindAllChildren();


            ToolBar = descendants.FirstOrDefault(e => e.ControlType == ControlType.ToolBar);
            StatusBar = descendants.FirstOrDefault(e => e.ControlType == ControlType.StatusBar);
            ProjectTree = descendants.FirstOrDefault(e => e.ControlType == ControlType.Tree);
            DocumentTab = descendants.FirstOrDefault(e => e.ControlType == ControlType.Tab);
            SearchBox = descendants.FirstOrDefault(e => e.ControlType == ControlType.Edit &&
                ((e.Name != null && e.Name.IndexOf("Search", StringComparison.OrdinalIgnoreCase) >= 0) ||
                 (e.AutomationId != null && e.AutomationId.IndexOf("Search", StringComparison.OrdinalIgnoreCase) >= 0)));
            OutputPane = descendants.FirstOrDefault(e =>
                (e.Name != null && e.Name.IndexOf("Output", StringComparison.OrdinalIgnoreCase) >= 0) ||
                (e.AutomationId != null && e.AutomationId.IndexOf("Output", StringComparison.OrdinalIgnoreCase) >= 0));
            PropertiesPane = descendants.FirstOrDefault(e =>
                (e.Name != null && e.Name.IndexOf("Property", StringComparison.OrdinalIgnoreCase) >= 0) ||
                (e.AutomationId != null && e.AutomationId.IndexOf("Property", StringComparison.OrdinalIgnoreCase) >= 0));

            Console.WriteLine("Application opened successfully. Menus and main elements initialized.");
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
    }
}
