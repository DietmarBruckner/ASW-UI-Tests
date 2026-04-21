using System;
using System.Collections.Generic;
using FlaUITests.Util;

namespace FlaUITests {
    class Program {
        static AutomationStudio6 as6;
        static void Main(string[] args) {
            Util.Environment.InstallationPath = "C:\\Program Files (x86)\\BRAutomation\\AS6";
            as6 = new AutomationStudio6();
            //as6.Project.CloseProject();
            //as6.Project.OpenProject("C:\\Users\\ATDIBRU\\OneDrive - ABB\\projects\\test_Mot");
            //as6.Project = new FlaUITests.Util.AppProject(as6.Ide_Main, "FlaUI_Tests", "C:\\Users\\ATDIBRU\\OneDrive - ABB\\projects", "Config1", "X20CP1684");
            Dictionary<Components, string> dictcom = new Dictionary<Components, string> () {
                {Components.OPCUACS, "6.6.1."},
                {Components.mappView, "6.6.0"},
                {Components.AutomationRuntime, "6.5.1"}
            };
            as6.Project = new FlaUITests.Util.AppProject(as6.Ide_Main, "FlaUI_Test1", "C:\\Users\\ATDIBRU\\OneDrive - ABB\\projects", "Config1", "X20CP1684", dictcom, "6.3");
            //as6.Project = new FlaUITests.Util.MappViewProject(as6.Ide_Main);
            as6.Project.ReadProject();
            //as6.CloseApplication();
            //as6.Project.DeleteProject();
        }
    }
}