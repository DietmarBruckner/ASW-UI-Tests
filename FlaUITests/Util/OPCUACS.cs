using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace FlaUITests.Util {
    public partial class OPCUACS {
        string editorPathOP;
        public override void InitComponent() {
            editorPathOP = Util.Environment.InstallationPath + "\\AS\\TechnologyPackages\\OpcUaCs\\" + Version + "\\Editors\\";
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Checking/setting OPC UA/CS version to " + Version);
            }
            TreeConfig.IdeMain.SelectComponentVersion("OPC", Version);
            ActivateOPCUACS();
        }
        public override void InsertComponent() {
            
        }
        void ActivateOPCUACS() {
            string uaconfig = "BR_UaCsConfig.uacfg";
             //open UACS configuration page
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Opening OPC UA/CS configuration in workspace");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_Connectivity", "BR_OpcUaCs", uaconfig}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" });
            //activate advanced visibility
            AutomationElement uaConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(uaconfig.Substring(3, uaconfig.Length-3)) >= 0);
            AutomationElement configTree = uaConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            AutomationElement uacsConfigRoot = configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_ClientServerConfiguration")));
            AutomationElement uaToolbar = uaConfigWorkspaceWindow.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Client/Server Configuration")));
            Button advancedVisibilityButton = uaToolbar.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Change Advanced Parameter Visibility"))).AsButton();
            if (!TreeConfig.IdeMain.IsButtonActive(advancedVisibilityButton))
                advancedVisibilityButton.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Setting OPC UA Client/Server to Enabled");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new List<string> { "BR_OPC UA Client/Server" }, new List<string> { "_Value" }, uacsConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 1); //Select "Enabled"
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Setting anonymous authentication to Enabled");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uacfg.xml", "Anonymous"), new List<string> { "_Name", "_Name", "_Name", "_Value" }, uacsConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 1); //Select "Enabled"
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Adding BR_Engineer as a user role");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uacfg.xml", "Anonymous Access", "BR_User Role 1"), new List<string> { "_Name", "_Name", "_Name", "_Value" }, uacsConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 2); //Select "BR_Engineer"
        }
    }
}