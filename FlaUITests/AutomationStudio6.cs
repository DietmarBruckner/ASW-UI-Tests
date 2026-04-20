using System;
using FlaUI.Core;
using FlaUITests.Util;

namespace FlaUITests {
    public class AutomationStudio6 {
        private readonly Application _app;
        public IDE_Main Ide_Main { get; private set; }
        public AppProject Project { get; set; }

        public AutomationStudio6() {
            try {
                _app = Application.Attach(@"C:\Program Files (x86)\BRAutomation\AS6\bin-en\pg.exe");
            }
            catch (Exception) {
                _app = Application.Launch(@"C:\Program Files (x86)\BRAutomation\AS6\bin-en\pg.exe");
            }
            if (_app == null) {
                Console.WriteLine("Error: Could not find or start Automation Studio 6 process.");
                return;
            }
            _app.WaitWhileMainHandleIsMissing();
            Ide_Main = new IDE_Main(_app);
            TreeConfig.IdeMain = Ide_Main;
            if (Ide_Main.IsProjectLoaded()) {
                Project = new AppProject(Ide_Main);
                TreeConfig.CurrentProject = Project;
            }
        }
        public void CloseApplication() {
            if (_app != null) {
                _app.Close();
                Console.WriteLine("Application closed successfully.");
            }
            else
                Console.WriteLine("Warning: Application was not open.");
        }
        public Application GetApplication() {
            return _app;
        }
    }
}