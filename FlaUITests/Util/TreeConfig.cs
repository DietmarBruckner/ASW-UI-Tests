using FlaUI.Core.AutomationElements;
using System.Drawing;
using FlaUI.Core.Input;
using FlaUI.Core.Definitions;
using System;

namespace FlaUITests.Util {
    public static class TreeConfig {
        public enum ViewType { LogicalView, ConfigurationView, PhysicalView, Workspace }
        public static IDE_Main IdeMain { get; set; }
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
        public static void ActivateTreeLeave(ViewType viewType, string [] leaves, string [] toClickSubstrings, AutomationElement root = null) {
            AutomationElement ae = null;
            switch (viewType) {
                case ViewType.LogicalView:
                    break;
                case ViewType.ConfigurationView:
                    ae = IdeMain.GetActiveConfigurtion();
                    break;
                case ViewType.PhysicalView:
                    break;
                case ViewType.Workspace:
                    if (root == null)
                        throw new Exception("Root element must be provided for Workspace view type");
                    ae = root;
                    break;
            }
            foreach (var sub in leaves) {
                AutomationElement oldAe = ae;
                ae = oldAe.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName(sub)));
                ClickConfigTreeItem(ae, toClickSubstrings[Array.IndexOf(leaves, sub)], !((Array.IndexOf(leaves, sub) == leaves.Length - 1) && (viewType == ViewType.Workspace))); //Double click all tree items except the last one in Workspace view, as the last click should activate the tree leave, not just expand it
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                ae = oldAe.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName(sub)));    //After clicking the tree item, the tree is refreshed and we need to find the tree item again to be able to continue expanding the tree
            }
/*             ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_" + CPU)));
            ClickConfigTreeItem(ae, "_Configuration", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_Connectivity")));
            ClickConfigTreeItem(ae, "_Configuration", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_OpcUaCs")));
            ClickConfigTreeItem(ae, "_Configuration", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_UaCsConfig.uacfg")));
            ClickConfigTreeItem(ae, "_Configuration", true);
 */
        }
    }
}