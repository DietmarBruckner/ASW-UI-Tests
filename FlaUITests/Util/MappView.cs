using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;


namespace FlaUITests.Util {
    public partial class MappView {
        string editorPathMV;
        public override void InitComponent() {
            editorPathMV = Util.Environment.InstallationPath + "\\AS\\TechnologyPackages\\mappView\\" + Version + "\\Editors\\";
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
            TreeConfig.IdeMain.SelectComponentVersion("mapp View", Version);
             if (!TreeConfig.IdeMain.GetLogicalViewRoot(Project).FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Any(cf => cf.Name.IndexOf("mappView") >= 0))
                 InsertComponent();
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
        void ConfigureMappViewServer() {
            string mvconfig = "BR_Config.mappviewcfg";
            //insert mapp View configuration under configuration view and open its workspace
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView"}, new List<string> { "_Configuration", "_Configuration" });
            TreeConfig.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, TreeConfig.IdeMain, "mapp View", "mapp View Configuration");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", mvconfig }, new List<string> { "_Configuration", "_Configuration", "_Configuration" });
            AutomationElement mvaConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(mvconfig.Substring(3, mvconfig.Length-3)) >= 0);
            AutomationElement configTree = mvaConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            AutomationElement mvConfigRoot = configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_MappViewConfiguration")));
            //select HTTP as communication protocol
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathMV + "mappviewcfg.xml", "Protocol"), new List<string> { "_Name", "_Value" }, mvConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 0); //Select "HTTP"
            //select anonymous token as Startup User
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathMV + "mappviewcfg.xml", "Startup User"), new List<string> { "_Name", "_Value" }, mvConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 0); //Select "anonymous token"
        }
    }
}