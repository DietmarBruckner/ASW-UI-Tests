using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using System;
using System.Drawing;

namespace FlaUITests.Util {
    public class MappViewProject : AppProject {
        AutomationElement toolbox;
        //AutomationElement outputWindow;
        AutomationElement toolBoxCategories;
        AutomationElement toolBoxContextContent;
        public MappViewProject(IDE_Main ideMain) : base(ideMain) {
            InitMappView();
        }
        public MappViewProject(IDE_Main ideMain, string name, string path, string config, string cpu) : base(ideMain, name, path, config, cpu) {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            InitMappView();
        }
        public void InitMappView() {
            //InsertMappView();
            AutomationElement activeConfig = _ideMain.GetActiveConfigurtion();
            AutomationElement cpuTreeItem = activeConfig.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_" + CPU)));
            Rectangle cpuTreeItemRect = cpuTreeItem.BoundingRectangle;
            Point point = new Point { X = cpuTreeItemRect.Left + 10, Y = cpuTreeItemRect.Top + cpuTreeItemRect.Height / 2 };
            //Mouse.MoveTo(point);
            Mouse.Click(point);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.RIGHT);
            AutomationElement connectivityItem = cpuTreeItem.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_Connectivity")));
            Rectangle connectivityItemRect = connectivityItem.BoundingRectangle;
            point = new Point { X = connectivityItemRect.Left + 10, Y = connectivityItemRect.Top + connectivityItemRect.Height / 2 };
            //Mouse.MoveTo(point);
            Mouse.Click(point);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.RIGHT);
            AutomationElement opcUACSitem = connectivityItem.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_OpcUaCs")));
            Rectangle opcUaCsItemRect = opcUACSitem.BoundingRectangle;
            point = new Point { X = opcUaCsItemRect.Left + 10, Y = opcUaCsItemRect.Top + opcUaCsItemRect.Height / 2 };
            //Mouse.MoveTo(point); 
            Mouse.Click(point);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.RIGHT);
            AutomationElement uaCsConfigItem = opcUACSitem.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_UaCsConfig.uacfg")));
            Rectangle uaCsConfigItemRect = uaCsConfigItem.BoundingRectangle;
            point = new Point { X = uaCsConfigItemRect.Left + 10, Y = uaCsConfigItemRect.Top + uaCsConfigItemRect.Height / 2 };
            //Mouse.MoveTo(point);
            Mouse.DoubleClick(point);
        }
        void InsertMappView() {
            _ideMain.InitializeViews(projectExplorer: true, toolbox: true, outputResults: true);
            _ideMain.MakeToolBoxElementsVisible(categories: true);
            _ideMain.SearchToolBox("mapp view");
            toolbox = _ideMain.Toolbox;
            toolBoxCategories = toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByAutomationId("_categoriesListView")));
            AutomationElement mappViewToolBoxItem = toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByName("mapp View"))) ?? throw new Exception("mapp View toolbox item not found - not installed?");
            AutomationElement [] allDesc = mappViewToolBoxItem.FindAllDescendants();
            if (allDesc[0].AsCheckBox().IsChecked == false) {
                mappViewToolBoxItem.Click();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            _ideMain.MakeToolBoxElementsVisible(categories: false);
            toolBoxContextContent = toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("_elementsListView")));
            AutomationElement mappViewElementItem = toolBoxContextContent.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataItem).And(cf.ByName("mapp View"))) ?? throw new Exception("mapp View element not found");
            mappViewElementItem.DoubleClick();  //mapp View wizard opens
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            Window newMappViewDialog = _ideMain.GetModalWindow("Insert mapp View solution");
            AutomationElement defaultTemplate = null;
            AutomationElement [] allElements = newMappViewDialog.FindAllDescendants();
            foreach (var element in allElements) {
                string childName = element.Name;
                if (childName.IndexOf("Default", StringComparison.OrdinalIgnoreCase) >= 0)
                    defaultTemplate = element;
            }
            if (defaultTemplate == null) {
                Console.WriteLine("Default template not found in mapp View wizard");
                return;
            }
            AutomationElement [] allTemplates = defaultTemplate.Parent.FindAllChildren();
            Random rand = new Random();
            int index = rand.Next(allTemplates.Length);
            allTemplates[index].DoubleClick(); //Select a random template to create some variation in the created projects
            _ideMain.WaitParsing();
        }
    }
}