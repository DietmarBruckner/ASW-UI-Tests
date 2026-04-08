using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.UIA2;
using Xunit;

namespace TestProject1
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_ReturnsFour_WhenAddingTwoAndTwo()
        {
            int result = Calculator.Add(2, 2);

            Assert.Equal(4, result);
        }
        [Fact]
        public void Test1()
        {
               var app = Application.Launch(@"C:\Program Files (x86)\BRAutomation\AS6\bin-en\pg.exe");
               app.WaitWhileMainHandleIsMissing();
                var automation = new UIA2Automation();
                var mainWindow = app.GetMainWindow(automation);
                var cf = new ConditionFactory(new UIA2PropertyLibrary());
        }
    }
}
