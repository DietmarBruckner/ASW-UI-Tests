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
/*                 var menu = mainWindow.FindFirstDescendant(cf.Menu()).AsMenu();
                var fileMenuItem = menu.Items[0];
                var editMenuItem = menu.Items[1];
                var viewMenuItem = menu.Items[2];
                var insertMenuItem = menu.Items[3];
                var openMenuItem = menu.Items[4];
                var projectMenuItem = menu.Items[5];
                var debugMenuItem = menu.Items[6];
                var onlineMenuItem = menu.Items[7];
                var toolsMenuItem = menu.Items[8];
                var windowMenuItem = menu.Items[9];
                var helpMenuItem = menu.Items[10];

                fileMenuItem.DrawHighlight();
                fileMenuItem.Invoke();

                mainWindow.FindFirstDescendant(cf.ByName("mnuFile")).AsMenuItem().RightClick();
                var contextMenu = mainWindow.ContextMenu;
 */        }
    }
}
