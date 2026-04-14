using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using System;
using System.Drawing;
using System.Linq;

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
            if (!_ideMain.GetLogicalViewRoot(this).FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Any(cf => cf.Name.IndexOf("mappView") >= 0)) {
                InsertMappView();
            }
            ActivateOPCUACS();
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
        void ActivateOPCUACS () {
            /* AutomationElement ae = _ideMain.GetActiveConfigurtion().FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_" + CPU)));
            ClickConfigTreeItem(ae, "_Configuration", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_Connectivity")));
            ClickConfigTreeItem(ae, "_Configuration", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_OpcUaCs")));
            ClickConfigTreeItem(ae, "_Configuration", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_UaCsConfig.uacfg")));
            ClickConfigTreeItem(ae, "_Configuration", true);

            AutomationElement uaConfigWorkspaceWindow = _ideMain.Workspace.FindAllDescendants(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf("UaCsConfig.uacfg") >= 0);
            AutomationElement configTree = uaConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            AutomationElement uacsenable = configTree.FindFirstDescendant(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_OPC UA Client/Server")));
            ClickConfigTreeItem(uacsenable, "_Value");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            ClickComboBoxTreeItem(1); //Select "Enabled"
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1)); */
            AutomationElement [] buttons = _ideMain.ToolBarStandard.FindAllDescendants(cf => cf.ByControlType(ControlType.Button));
            AutomationElement saveButton = _ideMain.ToolBarStandard.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("BR_\nSave"))).AsButton();
            _ideMain.ToolBarStandard.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("BR_\nSave"))).AsButton().Invoke();
            while (_ideMain.StatusBar.Name.IndexOf("Saving", StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }
}