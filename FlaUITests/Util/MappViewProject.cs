using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using System;
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
            TreeConfig.ActivateTreeLeave(TreeConfig.ViewType.ConfigurationView, new string[] { "BR_" + CPU, "BR_Connectivity", "BR_OpcUaCs", "BR_UaCsConfig.uacfg"}, new string[] { "_Configuration", "_Configuration", "_Configuration", "_Configuration" });
             AutomationElement ae = _ideMain.GetActiveConfigurtion().FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_" + CPU)));
/*            TreeConfig.ClickConfigTreeItem(ae, "_Configuration", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_Connectivity")));
            TreeConfig.ClickConfigTreeItem(ae, "_Configuration", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_OpcUaCs")));
            TreeConfig.ClickConfigTreeItem(ae, "_Configuration", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_UaCsConfig.uacfg")));
            TreeConfig.ClickConfigTreeItem(ae, "_Configuration", true); */
 
            AutomationElement uaConfigWorkspaceWindow = _ideMain.Workspace.FindAllDescendants(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf("UaCsConfig.uacfg") >= 0);
            AutomationElement configTree = uaConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            AutomationElement uaToolbar = uaConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Client/Server Configuration")));
            AutomationElement advancedVisibilityButton = uaToolbar.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Change Advanced Parameter Visibility")));
            if (!advancedVisibilityButton.IsEnabled)
                advancedVisibilityButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            AutomationElement uacsenable = configTree.FindFirstDescendant(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_OPC UA Client/Server")));
            TreeConfig.ClickConfigTreeItem(uacsenable, "_Value");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            TreeConfig.ClickComboBoxTreeItem(_ideMain.MainWindow, 1); //Select "Enabled"
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            ae = configTree.FindFirstDescendant(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_Security")));
            TreeConfig.ClickConfigTreeItem(ae, "_Name", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_Authentication")));
            TreeConfig.ClickConfigTreeItem(ae, "_Name", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_Authentication Methods")));
            TreeConfig.ClickConfigTreeItem(ae, "_Name", true);
            ae = ae.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_Anonymous")));
            TreeConfig.ClickConfigTreeItem(ae, "_Value");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            TreeConfig.ClickComboBoxTreeItem(_ideMain.MainWindow, 1); //Select "Enabled"
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            _ideMain.ToolBarStandard.FindAllDescendants(cf => cf.ByControlType(ControlType.Button)).FirstOrDefault(cf => cf.Name.IndexOf("BR_\nSave", StringComparison.OrdinalIgnoreCase) >= 0).AsButton().Click();
        }
    }
}