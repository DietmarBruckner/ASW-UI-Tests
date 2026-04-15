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
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
        }
        public static void ActivateTreeLeaf(ViewType viewType, string [] leaves, string [] toClickSubstrings, AutomationElement root = null) {
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
                    ClickConfigTreeItem(ae, "_Name");
                    Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.RIGHT);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    break;
            }
            foreach (var sub in leaves) {
                AutomationElement oldAe = ae;
                ae = oldAe.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName(sub)));
                if (viewType == ViewType.Workspace) { //no double clicking, but expanding via right arrow
                    ClickConfigTreeItem(ae, toClickSubstrings[Array.IndexOf(leaves, sub)]);
                    if (Array.IndexOf(leaves, sub) == leaves.Length - 1) {
                        AutomationElement [] allChildren = root.Parent.FindFirstChild(cf => cf.ByAutomationId("100")).FindAllChildren();
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                    }
                    else
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.RIGHT);
                }
                else //Double click all tree items to expand them, as tree items in Configuration view expand on double click
                    ClickConfigTreeItem(ae, toClickSubstrings[Array.IndexOf(leaves, sub)], true); 
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                //After clicking the tree item, the tree is refreshed and we need to find the tree item again to be able to continue expanding the tree
                ae = oldAe.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName(sub)));    
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