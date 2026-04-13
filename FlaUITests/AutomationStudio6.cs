using System;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.UIA2;
using FlaUITests.Util;

namespace FlaUITests {
    public class AutomationStudio6 {
        private Application _app;
        public IDE_Main Ide_Main { get; private set; }
        public AppProject Project { get; set; }

        public AutomationStudio6() {
            OpenApplication();
        }

        /// <summary>
        /// Opens the Automation Studio 6 application and initializes main items
        /// </summary>
        void OpenApplication() {
            _app = Application.Attach(@"C:\Program Files (x86)\BRAutomation\AS6\bin-en\pg.exe");
            //_app = Application.Launch(@"C:\Program Files (x86)\BRAutomation\AS6\bin-en\pg.exe");
            Ide_Main = new IDE_Main(_app);
            if (Ide_Main.IsProjectLoaded())
                Project = new AppProject(Ide_Main);
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
        public Application GetApplication()
        {
            return _app;
        }
    }
}