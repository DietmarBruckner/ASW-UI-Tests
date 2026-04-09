using System;

namespace FlaUITests
{
    class Program
    {
        static AutomationStudio6 as6;
        static void Main(string[] args)
        {
            as6 = new AutomationStudio6();
            as6.OpenApplication();
            while (!as6.IsProjectLoaded()) {
                Console.WriteLine("Waiting for project to load...");
                as6.GetApplication().WaitWhileBusy(TimeSpan.FromSeconds(20));
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            as6.closeProject();
            as6.readProject();
            as6.FileMenu.Click();
            Console.WriteLine("File menu clicked successfully.");
            as6.CloseApplication();
        }
    } 
}
