using System;
using System.Drawing;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.UIA2;
using FlaUITests.Util;
using Menu = FlaUI.Core.AutomationElements.Menu;

namespace FlaUITests {
    public class AutomationStudio6 {
        private Application _app;
        private UIA2Automation _automation;
        private Window _mainWindow;
        private ConditionFactory _cf;
        private IDE_Main _ideMain;


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
            _ideMain = new IDE_Main(_app, _mainWindow, _automation, _cf);

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
