using FlaUI.Core.AutomationElements;
using System.Drawing;
using FlaUI.Core.Input;
using FlaUI.Core.Definitions;

namespace FlaUITests.Util {
    public static class TreeConfig {
        public static void ClickConfigTreeItem(AutomationElement element, string sub, bool doubleClick = false) {
            AutomationElement clickElement = element.FindFirstChild(cf => cf.ByName(element.Name + sub));
            Rectangle elementRect = clickElement.BoundingRectangle;
            Point clickPoint = new Point { X = elementRect.Left + elementRect.Width / 2, Y = elementRect.Top + elementRect.Height / 2 };
            if (doubleClick) {
                Mouse.DoubleClick(clickPoint);
            } else {
                Mouse.Click(clickPoint);
            }
        }
        public static void ClickComboBoxTreeItem(Window window, int index) {
            AutomationElement comboBox = window.Parent.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
            AutomationElement item = comboBox.FindAllChildren()[index];
            Rectangle elementRect = item.BoundingRectangle;
            Point clickPoint = new Point { X = elementRect.Left + elementRect.Width / 2, Y = elementRect.Top + elementRect.Height / 2 };
            Mouse.Click(clickPoint);
        }
    }
}