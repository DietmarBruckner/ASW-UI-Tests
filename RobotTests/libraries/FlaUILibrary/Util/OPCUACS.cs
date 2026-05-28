using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Linq;
using System.Collections.Generic;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;

namespace FlaUILibrary.Util {
    public partial class OPCUACS {
        void EnsureEditorContext() {
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
        }
        public override void InitComponent() {
            EnsureEditorContext();
            Util.ConsoleOut(Util.Verbose.STEPS, "Checking/setting OPC UA/CS version to " + Version);
            TreeConfig.IdeMain.SelectComponentVersion(null, "OPC", Version);
            InsertComponent();
            TM611_3_1_ActivateOPCUACS();
            TM611_10_RBAC();
        }
        public override void InsertComponent() {
            //activated by default, nothing to do
        }
        public void ConfigureClientServerActivation() {
            EnsureEditorContext();
            TM611_3_1_ActivateOPCUACS();
        }
        public void ConfigureRoleBasedAccessControl() {
            EnsureEditorContext();
            TM611_10_RBAC();
        }
        void TM611_3_1_ActivateOPCUACS() {
            string uaconfig = "UaCsConfig.uacfg";
             //open UACS configuration page
            Util.ConsoleOut(Util.Verbose.STEPS, "Opening OPC UA/CS configuration in workspace");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_Connectivity", "BR_OpcUaCs", "BR_" + uaconfig}, out IDE_Main.ActiveEditor);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            //activate advanced visibility
            AutomationElement ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(IDE_Main.ActiveEditor, "BR_ClientServerConfiguration");
            AutomationElement uaToolbar = TreeConfig.IdeMain.GetWorkspaceToolbar(IDE_Main.ActiveEditor);
            Button advancedVisibilityButton = uaToolbar.FindFirstChild(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Change Advanced Parameter Visibility"))).AsButton();
            if (!IDE_Main.IsButtonActive(advancedVisibilityButton)) {
                advancedVisibilityButton.Click();
                ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(IDE_Main.ActiveEditor, "BR_ClientServerConfiguration");
            }
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Util.ConsoleOut(Util.Verbose.STEPS, "Setting OPC UA Client/Server to Enabled");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new List<string> { "BR_OPC UA Client/Server" }, out var e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Enabled"
            Util.ConsoleOut(Util.Verbose.STEPS, "Setting anonymous authentication to Enabled");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\"  + "uacfg.xml", "Anonymous"), out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Enabled"
            Util.ConsoleOut(Util.Verbose.STEPS, "Adding BR_Engineer as a user role");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uacfg.xml", "Anonymous Access", new string [] { "BR_User Role 1" }), out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 2); //Select "BR_Engineer"
            IDE_Main.ActiveEditor.Close();
        }
        void TM611_10_RBAC() {
            Util.ConsoleOut(Util.Verbose.STEPS, "Creating roles: Operator, Service and Observer");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_AccessAndSecurity", "BR_UserRoleSystem"}, out var e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "Role");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_AccessAndSecurity", "BR_UserRoleSystem", "BR_Role.role"}, out IDE_Main.ActiveEditor);
            IDE_Main.AddRole("Operator", false);
            IDE_Main.AddRole("Service");
            IDE_Main.AddRole("Observer");
            TreeConfig.IdeMain.SaveAll();
            IDE_Main.ActiveEditor.Close();

            Util.ConsoleOut(Util.Verbose.STEPS, "Creating users: Operator, Service and Observer");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_AccessAndSecurity", "BR_UserRoleSystem"}, out e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "User");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_AccessAndSecurity", "BR_UserRoleSystem", "BR_User.user"}, out IDE_Main.ActiveEditor);
            IDE_Main.AddUser("UserOperator", "5555", "Operator", false);
            IDE_Main.AddUser("UserService", "9999", "Service");
            IDE_Main.AddUser("UserObserver", "0000", "Observer");
            IDE_Main.ActiveEditor.Close();
            
            string uadvconfig = "UaDvConfig.uadcfg";
            Util.ConsoleOut(Util.Verbose.STEPS, "Opening OPC UA Default View configuration in workspace");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_Connectivity", "BR_OpcUaCs", "BR_" + uadvconfig}, out IDE_Main.ActiveEditor);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            AutomationElement ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(IDE_Main.ActiveEditor, "BR_DefaultViewConfiguration");
            Util.ConsoleOut(Util.Verbose.STEPS, "Editing role permissions for OPC UA Default View");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Name" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "Operator");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Name" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "Service");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 3", "BR_Name" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "Observer");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_Browse" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_Read" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_Write" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_Call" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_ReadRolePermissions" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_ReadHistory" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_Browse" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_Read" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_Write" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_Call" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_ReadRolePermissions" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_ReadHistory" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 3", "BR_Permissions", "BR_Browse" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 3", "BR_Permissions", "BR_Read" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 3", "BR_Permissions", "BR_Call" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 3", "BR_Permissions", "BR_ReadRolePermissions" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(Util.Environment.InstallationPath + Util.Environment.EditorPathOPCUACS + Version + "\\Editors\\" + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 3", "BR_Permissions", "BR_ReadHistory" }), out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
        }
    }
}