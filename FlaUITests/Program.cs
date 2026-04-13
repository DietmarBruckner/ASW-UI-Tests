using System;

namespace FlaUITests {
    class Program {
        static AutomationStudio6 as6;
        static void Main(string[] args) {
            as6 = new AutomationStudio6();
            as6.Project.CloseProject();
            as6.Project.OpenProject("C:\\Users\\ATDIBRU\\OneDrive - ABB\\projects\\test_Mot");
            as6.Project.ReadProject();
            as6.CloseApplication();
        }
    }
}