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
            _ideMain.ToolBarStandard.FindAllDescendants(cf => cf.ByControlType(ControlType.Button)).FirstOrDefault(cf => cf.Name.IndexOf("BR_\nSave", StringComparison.OrdinalIgnoreCase) >= 0).AsButton().Click();
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
            //open UACS configuration page
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new string[] { "BR_" + CPU, "BR_Connectivity", "BR_OpcUaCs", "BR_UaCsConfig.uacfg"}, new string[] { "_Configuration", "_Configuration", "_Configuration", "_Configuration" });
            //activate advanced visibility
            AutomationElement uaConfigWorkspaceWindow = _ideMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf("UaCsConfig.uacfg") >= 0);
            AutomationElement uaToolbar = uaConfigWorkspaceWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Client/Server Configuration")));
            AutomationElement advancedVisibilityButton = uaToolbar.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Change Advanced Parameter Visibility")));
            if (!advancedVisibilityButton.IsEnabled)
                advancedVisibilityButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            //set OPC UA Client/Server to Enabled
            AutomationElement configTree = uaConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            AutomationElement uacsConfigRoot = configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_ClientServerConfiguration")));
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new string[] { "BR_OPC UA Client/Server" }, new string[] { "_Value" }, uacsConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(_ideMain.MainWindow, 1); //Select "Enabled"
            //set anonymous authentication to Enabled
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new string[] { "BR_Security", "BR_Authentication", "BR_Authentication Methods", "BR_Anonymous" }, new string[] { "_Name", "_Name", "_Name", "_Value" }, uacsConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(_ideMain.MainWindow, 1); //Select "Enabled"

            
        }
    }
}