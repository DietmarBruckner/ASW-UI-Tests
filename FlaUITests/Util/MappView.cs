using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

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
            TreeConfig.IdeMain.WaitForMessage("finished.");
        }
        void ActivateOPCUACS() {
            string uaconfig = "BR_UaCsConfig.uacfg";
             //open UACS configuration page
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_Connectivity", "BR_OpcUaCs", uaconfig}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" });
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
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new List<string> { "BR_OPC UA Client/Server" }, new List<string> { "_Value" }, uacsConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 1); //Select "Enabled"
            //set anonymous authentication to Enabled
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new List<string> { "BR_Security", "BR_Authentication", "BR_Authentication Methods", "BR_Anonymous" }, new List<string> { "_Name", "_Name", "_Name", "_Value" }, uacsConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 1); //Select "Enabled"
            //add BR_Engineer as user role
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new List<string> { "BR_Security", "BR_Authorization", "BR_Anonymous Access", "BR_User Role 1" }, new List<string> { "_Name", "_Name", "_Name", "_Value" }, uacsConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 2); //Select "BR_Engineer"           
        }
        List<string> FindXMLPath(string file, string element) {
            List<XElement> res = new List<XElement>();
            List<string> s = new List<string>();
            if (!System.IO.File.Exists(file))
                Console.WriteLine($"Warning: mapp view editor file not found at path: {file}");
            try {
                XDocument doc = XDocument.Load(file);
                XElement root = doc.Root;
                XElement xConfiguration = root;
                if (xConfiguration == null)
                    Console.WriteLine("Warning: Configuration element not found in mapp view editor file");
                FindRecursive(ref res, xConfiguration, ref element);
                res.Reverse();
                foreach (XElement xe in res)
                    s.Add("BR_" + xe.Attribute("name-en").Value);

            } catch (Exception ex) { Console.WriteLine($"Error reading {file}: {ex.Message}"); }
            return s;
        }
        void FindRecursive(ref List<XElement> path, XElement root, ref string element) {
            //ref XElement [] res = ref path;
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
            if (path.Count != count)
                path.Add(root);
        }
        void ConfigureMappViewServer() {
            string mvconfig = "BR_Config.mappviewcfg";
            string editorPath = Util.Environment.InstallationPath + "\\AS\\TechnologyPackages\\mappView\\" + Version + "\\Editors\\";
            List<string> path = FindXMLPath(editorPath + "mappviewcfg.xml", "Protocol");
            //insert mapp View configuration under configuration view and open its workspace
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView"}, new List<string> { "_Configuration", "_Configuration" });
            TreeConfig.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, TreeConfig.IdeMain, "mapp View", "mapp View Configuration");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", mvconfig }, new List<string> { "_Configuration", "_Configuration", "_Configuration" });
            AutomationElement mvaConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(mvconfig.Substring(3, mvconfig.Length-3)) >= 0);
            AutomationElement configTree = mvaConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            AutomationElement mvConfigRoot = configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_MappViewConfiguration")));
            //select HTTP as communication protocol
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, FindXMLPath(editorPath + "mappviewcfg.xml", "Protocol"), new List<string> { "_Name", "_Value" }, mvConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 0); //Select "HTTP"
            //select anonymous token as Startup User
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, new List<string> { "BR_Server configuration", "BR_Startup User"}, new List<string> { "_Name", "_Value" }, mvConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 0); //Select "anonymous token"
        }
    }
}