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
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Checking/setting OPC UA/CS version to " + Version);
            }
            //TreeConfig.IdeMain.SelectComponentVersion("OPC", Version);
            TM611_3_1_ActivateOPCUACS();
        }
        public override void TM611_4_InsertComponent() {
            //activated by default, nothing to do
        }
        void TM611_3_1_ActivateOPCUACS() {
            string uaconfig = "UaCsConfig.uacfg";
             //open UACS configuration page
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Opening OPC UA/CS configuration in workspace");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_Connectivity", "BR_OpcUaCs", "BR_" + uaconfig}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" }, out var editor);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            //activate advanced visibility
            AutomationElement ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(editor, "BR_ClientServerConfiguration");
            AutomationElement uaToolbar = TreeConfig.IdeMain.GetWorkspaceToolbar(editor);
            Button advancedVisibilityButton = uaToolbar.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Change Advanced Parameter Visibility"))).AsButton();
            if (!TreeConfig.IdeMain.IsButtonActive(advancedVisibilityButton)) {
                advancedVisibilityButton.Click();
                ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(uaconfig, "BR_ClientServerConfiguration");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Setting OPC UA Client/Server to Enabled");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new List<string> { "BR_OPC UA Client/Server" }, new List<string> { "_Value" }, out var e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Enabled"
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Setting anonymous authentication to Enabled");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uacfg.xml", "Anonymous"), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Enabled"
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Adding BR_Engineer as a user role");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uacfg.xml", "Anonymous Access", "BR_User Role 1"), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 2); //Select "BR_Engineer"
            editor.Close();
        }
    }
}