using System;

namespace TestProject1
{
    class Program
    {
        static AutomationStudio6 as6;
        static void Main(string[] args)
        {
            as6 = new AutomationStudio6();
            as6.OpenApplication();
            as6.readProject();
            as6.FileMenu.Click();
            Console.WriteLine("File menu clicked successfully.");
            as6.CloseApplication();
        }
    } 
}
