using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Linq;
using System.Collections.Generic;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;

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
            InsertComponent();
            //TM611_3_1_ActivateOPCUACS();
            TM611_10_RBAC();
        }
        public override void InsertComponent() {
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
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uacfg.xml", "Anonymous Access", new string [] { "BR_User Role 1" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 2); //Select "BR_Engineer"
            editor.Close();
        }
        void TM611_10_RBAC() {
/*             if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Creating roles: Operator, Service and Observer");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_AccessAndSecurity", "BR_UserRoleSystem"}, new List<string> { "_Configuration", "_Configuration", "_Configuration" }, out var e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "Role");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_AccessAndSecurity", "BR_UserRoleSystem", "BR_Role.role"}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" }, out var role_editor);
            Mouse.Click(role_editor.ConfigWorkspace.BoundingRectangle.Center());
            AutomationElement configTree = role_editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            Button newRole = role_editor.ConfigWorkspace.FindFirstChild(cf => cf.ByName("Role Configuration")).FindFirstChild(cf => cf.ByName("Add \"Role\" Element")).AsButton();
            AutomationElement newRoleTreeItem = configTree.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Last();
            AutomationElement newRoleTreeItemName = newRoleTreeItem.FindFirstChild(cf => cf.ByName(newRoleTreeItem.Name + "_Name"));
            Mouse.MoveTo(newRoleTreeItemName.BoundingRectangle.Center());
            Mouse.DoubleClick();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            Keyboard.Type("Operator");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            newRole.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            newRoleTreeItem = configTree.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Last();
            newRoleTreeItemName = newRoleTreeItem.FindFirstChild(cf => cf.ByName(newRoleTreeItem.Name + "_Name"));
            Mouse.MoveTo(newRoleTreeItemName.BoundingRectangle.Center());
            Mouse.DoubleClick();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            Keyboard.Type("Service");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            newRole.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            newRoleTreeItem = configTree.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Last();
            newRoleTreeItemName = newRoleTreeItem.FindFirstChild(cf => cf.ByName(newRoleTreeItem.Name + "_Name"));
            Mouse.MoveTo(newRoleTreeItemName.BoundingRectangle.Center());
            Mouse.DoubleClick();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            Keyboard.Type("Observer");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            TreeConfig.IdeMain.SaveAll();
            role_editor.Close();

            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Creating users: Operator, Service and Observer");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_AccessAndSecurity", "BR_UserRoleSystem"}, new List<string> { "_Configuration", "_Configuration", "_Configuration" }, out e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "User");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_AccessAndSecurity", "BR_UserRoleSystem", "BR_User.user"}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" }, out var user_editor);
            AddUser(user_editor, "UserOperator", "5555", "Operator", false);
            AddUser(user_editor, "UserService", "9999", "Service");
            AddUser(user_editor, "UserObserver", "0000", "Observer");
            user_editor.Close();
 */            string uadvconfig = "UaDvConfig.uadcfg";
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Opening OPC UA Default View configuration in workspace");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_Connectivity", "BR_OpcUaCs", "BR_" + uadvconfig}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" }, out var uadv_editor);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            AutomationElement ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(uadv_editor, "BR_DefaultViewConfiguration");
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Editing role permissions for OPC UA Default View");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Name" }), new List<string> { "_Name", "_Name", "_Value" }, out var e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "Operator");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Name" }), new List<string> { "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "Service");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 3", "BR_Name" }), new List<string> { "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "Observer");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_Browse" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_Read" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_Write" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_Call" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_ReadRolePermissions" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 1", "BR_Permissions", "BR_ReadHistory" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_Browse" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_Read" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_Write" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_Call" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_ReadRolePermissions" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathOP + "uadcfg.xml", "DefaultRolePermissions", new string [] { "BR_Role 2", "BR_Permissions", "BR_ReadHistory" }), new List<string> { "_Name", "_Name", "_Name", "_Value" }, out e, ConfigRoot, shortcut:0);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 1); //Select "Eanabled"
        }
        void AddUser(IDE_Main.Editor editor, string Name, string Password, string Role, Boolean addUser = true) {
            Mouse.Click(editor.ConfigWorkspace.BoundingRectangle.Center());
            AutomationElement configTree = editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            Button newUser = editor.ConfigWorkspace.FindFirstChild(cf => cf.ByName("User Configuration")).FindFirstChild(cf => cf.ByName("Add \"User\" Element")).AsButton();
            if (addUser) {
                newUser.Click();
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            }
            AutomationElement newUserTreeItem = configTree.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Last();
            AutomationElement newUserTreeItemName = newUserTreeItem.FindFirstChild(cf => cf.ByName(newUserTreeItem.Name + "_Name"));
            AutomationElement newUserTreeItemPwd = newUserTreeItem.FindFirstChild(cf => cf.ByName("BR_Password"));
            AutomationElement newUserTreeItemPwdValue = newUserTreeItemPwd.FindFirstChild(cf => cf.ByName(newUserTreeItemPwd.Name + "_Value"));
            AutomationElement newUserTreeItemRole = newUserTreeItem.FindFirstChild(cf => cf.ByName("BR_Roles"));
            AutomationElement newUserTreeItemRoleAssigned = newUserTreeItemRole.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem));
            TreeConfig.ClickConfigTreeItem(TreeConfig.ViewType.Workspace, newUserTreeItemRoleAssigned, "_Value");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
            AutomationElement combobox = configTree.FindFirstChild(cf => cf.ByAutomationId("100")).FindFirstChild(cf => cf.ByControlType(ControlType.ComboBox));
            Button expandButton = combobox.FindFirstChild(cf => cf.ByControlType(ControlType.Button)).AsButton();
            Mouse.MoveTo(expandButton.GetClickablePoint());
            if (IDE_Main.MainWindow.Parent.FindFirstChild(cf => cf.ByControlType(ControlType.List)) == null) //if list is not yet open, click to open it
                Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, Role);
            Mouse.MoveTo(newUserTreeItemPwdValue.BoundingRectangle.Center());
            Mouse.DoubleClick();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            Keyboard.Type(Password);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            Mouse.MoveTo(newUserTreeItemName.BoundingRectangle.Center());
            Mouse.DoubleClick();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            Keyboard.Type(Name);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            TreeConfig.IdeMain.SaveAll();
        }
    }
}