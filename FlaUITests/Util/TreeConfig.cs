using FlaUI.Core.AutomationElements;
using System.Drawing;
using FlaUI.Core.Input;
using FlaUI.Core.Definitions;
using System;
using FlaUI.Core.AutomationElements.Scrolling;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Linq;

namespace FlaUITests.Util {
    public static class TreeConfig {
        public enum ViewType { LogicalView, ConfigurationView, PhysicalView, Workspace, PropertyWindow }
        public static IDE_Main IdeMain { get; set; }
        public static AppProject CurrentProject { get; set; }

        public static void ClickConfigTreeItem(ViewType viewType, AutomationElement element, string sub, bool doubleClick = false) {
            if (CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine("Trying to " + (doubleClick?"double click ":"click ") + "element: " + element.Name + "." + sub);
            MakeTreeItemVisible(viewType, element, sub);
            ClickAutomationElement(element.FindFirstChild(cf => cf.ByName(element.Name + sub)), doubleClick);
        }
        public static void ClickComboBoxTreeItem(Window window, int index) {
            if (CurrentProject != null && CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine("Trying to click " + (index+1) + "-th element of list");
            AutomationElement comboBox = window.Parent.FindFirstChild(cf => cf.ByControlType(ControlType.List));
            ClickAutomationElement(comboBox.FindAllChildren()[index]);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
        }
        public static void ClickComboBoxTreeItem(Window window, string element) {
            if (CurrentProject != null && CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine("Trying to click element: " + element + " in list");
            AutomationElement comboBox = window.Parent.FindFirstChild(cf => cf.ByControlType(ControlType.List));
            ClickAutomationElement(comboBox.FindFirstChild(cf => cf.ByName(element)));
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
        }
        public static void ClickAutomationElement(AutomationElement element, bool doubleClick = false) {
            if (CurrentProject != null && CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine((doubleClick?"Double clicking ":"Clicking ") + "in the middle of element: " + element);            
            Point point = new Point { X = element.BoundingRectangle.Left + element.BoundingRectangle.Width / 2, Y = element.BoundingRectangle.Top + element.BoundingRectangle.Height / 2 };
            if (doubleClick)
                Mouse.DoubleClick(point);
            else
                Mouse.Click(point);
        }
        public static void MakeTreeItemVisible(ViewType viewType, AutomationElement element, string sub) {
            if (CurrentProject.verbose >= Util.Environment.Verbose.STEPS)
                Console.WriteLine("Checking if element: " + element.Name + "." + sub + " is within view");
            AutomationElement clickElement = element.FindFirstChild(cf => cf.ByName(element.Name + sub));
            Rectangle elementRect = clickElement.BoundingRectangle;
            if (elementRect.Width == 0 || elementRect.Height == 0) {
            if (CurrentProject.verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine("Element: " + element.Name + "." + sub + " not within view, scrolling ...");
                AutomationElement view = null;
                switch (viewType) {
                    case ViewType.LogicalView:
                    case ViewType.ConfigurationView:
                    case ViewType.PhysicalView:
                        view = IdeMain.ProjectExplorer;
                        break;
                    case ViewType.Workspace:
                        view = IdeMain.Workspace;
                        break;
                }
                VerticalScrollBar verticalScrollBar = view.FindFirstDescendant(cf => cf.ByControlType(ControlType.ScrollBar).And(cf.ByAutomationId("Vertical ScrollBar"))).AsVerticalScrollBar();
                HorizontalScrollBar horizontalScrollBar = view.FindFirstDescendant(cf => cf.ByControlType(ControlType.ScrollBar).And(cf.ByAutomationId("Horizontal ScrollBar"))).AsHorizontalScrollBar();
                if (verticalScrollBar != null)
                    while (verticalScrollBar.Value > verticalScrollBar.MinimumValue)
                        verticalScrollBar.ScrollUpLarge();
                if (horizontalScrollBar != null)
                    while (horizontalScrollBar.Value > horizontalScrollBar.MinimumValue)
                        horizontalScrollBar.ScrollLeftLarge();
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                clickElement = element.FindFirstChild(cf => cf.ByName(element.Name + sub));
                elementRect = clickElement.BoundingRectangle;
                if (horizontalScrollBar != null && verticalScrollBar != null)
                    while((elementRect.Width == 0 || elementRect.Height == 0) && horizontalScrollBar.Value < horizontalScrollBar.MaximumValue) {
                        //try scrolling down and right until found
                        while ((elementRect.Width == 0 || elementRect.Height == 0) && verticalScrollBar.Value < verticalScrollBar.MaximumValue) {
                            verticalScrollBar.ScrollDown();
                            verticalScrollBar.ScrollDown();
                            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                            clickElement = element.FindFirstChild(cf => cf.ByName(element.Name + sub));
                            elementRect = clickElement.BoundingRectangle;
                        }
                        if (elementRect.Width == 0 || elementRect.Height == 0) {
                            while (verticalScrollBar.Value > verticalScrollBar.MinimumValue)
                                verticalScrollBar.ScrollUpLarge();
                            horizontalScrollBar.ScrollRightLarge();
                        }
                    }
                else {
                    if (verticalScrollBar != null)
                        while ((elementRect.Width == 0 || elementRect.Height == 0) && verticalScrollBar.Value < verticalScrollBar.MaximumValue) {
                                verticalScrollBar.ScrollDown();
                                verticalScrollBar.ScrollDown();
                                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                                clickElement = element.FindFirstChild(cf => cf.ByName(element.Name + sub));
                                elementRect = clickElement.BoundingRectangle;
                            }
                    if (horizontalScrollBar != null)
                        while ((elementRect.Width == 0 || elementRect.Height == 0) && horizontalScrollBar.Value < horizontalScrollBar.MaximumValue) {
                                horizontalScrollBar.ScrollRight();
                                horizontalScrollBar.ScrollRight();
                                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                                clickElement = element.FindFirstChild(cf => cf.ByName(element.Name + sub));
                                elementRect = clickElement.BoundingRectangle;
                            }
                }
                if (elementRect.Width != 0 && elementRect.Height != 0) {
                    Rectangle vr = view.BoundingRectangle;
                    while ((elementRect.Top > vr.Top + vr.Height/2) && (verticalScrollBar.Value < verticalScrollBar.MaximumValue)) {
                        verticalScrollBar.ScrollDown();
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                        clickElement = element.FindFirstChild(cf => cf.ByName(element.Name + sub));
                        elementRect = clickElement.BoundingRectangle;
                    }
                    while ((elementRect.Left > vr.Left + vr.Width/2) && (horizontalScrollBar.Value < horizontalScrollBar.MaximumValue)) {
                        horizontalScrollBar.ScrollRight();
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                        clickElement = element.FindFirstChild(cf => cf.ByName(element.Name + sub));
                        elementRect = clickElement.BoundingRectangle;
                    }
                }
                else
                    if (CurrentProject.verbose >= Util.Environment.Verbose.STEPS)
                        Console.WriteLine("Could not locate " + element.Name);
            }
        }
        public static void ActivateTreeLeaf(ViewType viewType, List<string> leaves, List<string> toClickSubstrings, AutomationElement root = null) {
            AutomationElement ae = null;
            if (CurrentProject.verbose >= Util.Environment.Verbose.STEPS)
                Console.WriteLine("Opening treeview element: " + leaves.Last() + "." + toClickSubstrings.Last());
            if (CurrentProject.verbose >= Util.Environment.Verbose.FULL) {
                Console.Write("Along the path: ");
                foreach (string s in leaves)
                    Console.Write(s + ", ");
                Console.WriteLine();
            }
            switch (viewType) {
                case ViewType.LogicalView:
                    ae = IdeMain.GetLogicalViewRoot(CurrentProject);
                    ClickConfigTreeItem(viewType, ae, "_Object Name", true);
                    if (leaves == null)
                        return;
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
                    IdeMain.SwitchView(viewType);
                    ClickConfigTreeItem(viewType, ae, "_Name");
                    Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.RIGHT);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    break;
            }
            foreach (var sub in leaves) {
                AutomationElement oldAe = ae;
                ae = oldAe.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName(sub)));
                if (viewType == ViewType.Workspace) { //no double clicking, but expanding via right arrow
                    ClickConfigTreeItem(viewType, ae, toClickSubstrings[leaves.IndexOf(sub)]); //combobox in final leaf node needs some steps to activate
                    if (leaves.IndexOf(sub) == leaves.Count - 1) {
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                        AutomationElement combobox = root.Parent.FindFirstChild(cf => cf.ByAutomationId("100")).FindFirstChild(cf => cf.ByControlType(ControlType.ComboBox));
                        Button expandButton = combobox.FindFirstChild(cf => cf.ByControlType(ControlType.Button)).AsButton();
                        Mouse.MoveTo(expandButton.GetClickablePoint());
                        if (IdeMain.MainWindow.Parent.FindFirstChild(cf => cf.ByControlType(ControlType.List)) == null) //if list is not yet open, click to open it
                             Mouse.Click();
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                        return;
                    }
                    else
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.RIGHT);
                }
                else //Double click all tree items to expand them, as tree items in Configuration view expand on double click
                    ClickConfigTreeItem(viewType, ae, toClickSubstrings[leaves.IndexOf(sub)], true); 
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                //After clicking the tree item, the tree is refreshed and we need to find the tree item again to be able to continue expanding the tree
                ae = oldAe.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName(sub)));    
            }
        }
        public static List<string> FindXMLPath(string file, string element, string addon = null) {
            List<XElement> res = new List<XElement>();
            List<string> s = new List<string>();
            if (!System.IO.File.Exists(file))
                Console.WriteLine($"Warning: file not found at path: {file}");
            try {
                XDocument doc = XDocument.Load(file);
                XElement root = doc.Root;
                if (root == null)
                    Console.WriteLine($"Warning: Root element not found in file: {file}");
                FindRecursive(ref res, root, ref element);
                res.Reverse();
                foreach (XElement xe in res)
                    if (xe != root)
                        s.Add("BR_" + xe.Attribute("Name-en").Value);
                if (addon != null)
                    s.Add(addon);
            } catch (Exception ex) { Console.WriteLine($"Error reading {file}: {ex.Message}"); }
            return s;
        }
        static void FindRecursive(ref List<XElement> path, XElement root, ref string element) {
            int count = path.Count;
            foreach (XElement groupElement in root.Elements("Group")) {
                XAttribute nameAttr = groupElement.Attribute("Name-en");
                if (nameAttr != null && nameAttr.Value == element) {
                    path.Add(groupElement);
                    path.Add(root);
                    return;
                }
                FindRecursive(ref path, groupElement, ref element);
            }
            foreach (XElement selElement in root.Elements("Selector")) {
                XAttribute nameAttr = selElement.Attribute("Name-en");
                if (nameAttr != null && nameAttr.Value == element) {
                    path.Add(selElement);
                    path.Add(root);
                    return;
                }
                FindRecursive(ref path, selElement, ref element);
            }
            foreach (XElement propElement in root.Elements("Property")) {
                XAttribute nameAttr = propElement.Attribute("Name-en");
                if (nameAttr != null && nameAttr.Value == element) {
                    path.Add(propElement);
                    path.Add(root);
                    return;
                }
                FindRecursive(ref path, propElement, ref element);
            }
            if (path.Count != count)
                path.Add(root);
        }
    }
}