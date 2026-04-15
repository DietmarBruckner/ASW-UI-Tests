using System;

namespace FlaUITests {
    class Program {
        static AutomationStudio6 as6;
        static void Main(string[] args) {
            as6 = new AutomationStudio6();
            //as6.Project.CloseProject();
            //as6.Project.OpenProject("C:\\Users\\ATDIBRU\\OneDrive - ABB\\projects\\test_Mot");
            //as6.Project = new FlaUITests.Util.AppProject(as6.Ide_Main, "FlaUI_Tests", "C:\\Users\\ATDIBRU\\OneDrive - ABB\\projects", "Config1", "X20CP1684");
            as6.Project = new FlaUITests.Util.MappViewProject(as6.Ide_Main, "FlaUI_Tests", "C:\\Users\\ATDIBRU\\OneDrive - ABB\\projects", "Config1", "X20CP1684");
            //as6.Project = new FlaUITests.Util.MappViewProject(as6.Ide_Main);
            as6.Project.ReadProject();
            //as6.CloseApplication();
            //as6.Project.DeleteProject();
        }
    }
}