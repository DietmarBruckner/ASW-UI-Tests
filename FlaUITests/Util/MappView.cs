using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Linq;

namespace FlaUITests.Util {
    public partial class MappView {

        public override void InitComponent() {
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
            TreeConfig.IdeMain.SelectComponentVersion("mapp View", Version);
            if (!TreeConfig.IdeMain.GetLogicalViewRoot(Project).FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Any(cf => cf.Name.IndexOf("mappView") >= 0))
                 InsertComponent();
            ActivateOPCUACS();
            ConfigureMappViewServer();
        }
        public override void InsertComponent() {
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, null, null);
            TreeConfig.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, TreeConfig.IdeMain, "mapp View", "mapp View");
            Window newMappViewDialog = TreeConfig.IdeMain.GetModalWindow("Insert mapp View solution");
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
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            TreeConfig.IdeMain.WaitForMessage("Parsing finished.");
        }
        void ActivateOPCUACS() {
            string uaconfig = "BR_UaCsConfig.uacfg";
             //open UACS configuration page
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new string[] { "BR_" + Project.CPU, "BR_Connectivity", "BR_OpcUaCs", uaconfig}, new string[] { "_Configuration", "_Configuration", "_Configuration", "_Configuration" });
            //activate advanced visibility
            AutomationElement uaConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(uaconfig.Substring(3, uaconfig.Length-3)) >= 0);
            AutomationElement configTree = uaConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            AutomationElement uacsConfigRoot = configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_ClientServerConfiguration")));
            AutomationElement uaToolbar = uaConfigWorkspaceWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Client/Server Configuration")));
            Button advancedVisibilityButton = uaToolbar.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Change Advanced Parameter Visibility"))).AsButton();
            int itemsCount = uacsConfigRoot.FindAllDescendants(cf => cf.ByControlType(ControlType.TreeItem)).Length;
            advancedVisibilityButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            uacsConfigRoot = configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_ClientServerConfiguration")));
            int itemsCount1 = uacsConfigRoot.FindAllDescendants(cf => cf.ByControlType(ControlType.TreeItem)).Length;
            if (itemsCount1 < itemsCount) {
                advancedVisibilityButton.Click();
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                uacsConfigRoot = configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_ClientServerConfiguration")));
            }
             //set OPC UA Client/Server to Enabled
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new string[] { "BR_OPC UA Client/Server" }, new string[] { "_Value" }, uacsConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 1); //Select "Enabled"
            //set anonymous authentication to Enabled
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new string[] { "BR_Security", "BR_Authentication", "BR_Authentication Methods", "BR_Anonymous" }, new string[] { "_Name", "_Name", "_Name", "_Value" }, uacsConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 1); //Select "Enabled"
            //add BR_Engineer as user role
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new string[] { "BR_Security", "BR_Authorization", "BR_Anonymous Access", "BR_User Role 1" }, new string[] { "_Name", "_Name", "_Name", "_Value" }, uacsConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 2); //Select "BR_Engineer"           
        }
        void ConfigureMappViewServer() {
            string mvconfig = "BR_Config.mappviewcfg";
            //insert mapp View configuration under configuration view and open its workspace
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new string[] { "BR_" + Project.CPU, "BR_mappView"}, new string[] { "_Configuration", "_Configuration" });
            TreeConfig.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, TreeConfig.IdeMain, "mapp View", "mapp View Configuration");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new string[] { "BR_" + Project.CPU, "BR_mappView", mvconfig }, new string[] { "_Configuration", "_Configuration", "_Configuration" });
            AutomationElement mvaConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(mvconfig.Substring(3, mvconfig.Length-3)) >= 0);
            AutomationElement configTree = mvaConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            AutomationElement mvConfigRoot = configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_MappViewConfiguration")));
            //select HTTP as communication protocol
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new string[] { "BR_Server configuration", "BR_Protocol"}, new string[] { "_Name", "_Value" }, mvConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 0); //Select "HTTP"
            //select anonymous token as Startup User
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new string[] { "BR_Server configuration", "BR_Startup User"}, new string[] { "_Name", "_Value" }, mvConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 0); //Select "anonymous token"
        }
    }
}