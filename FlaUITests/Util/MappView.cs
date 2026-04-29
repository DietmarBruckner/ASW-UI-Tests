using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;


namespace FlaUITests.Util {
    public partial class MappView {
        string editorPathMV;
        public override void InitComponent() {
            editorPathMV = Environment.InstallationPath + "\\AS\\TechnologyPackages\\mappView\\" + Version + "\\Editors\\";
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Checking/setting mapp View version to " + Version);
            }
            TreeConfig.IdeMain.SelectComponentVersion("mapp View", Version);
            if (!TreeConfig.IdeMain.GetLogicalViewRoot(Project).FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Any(cf => cf.Name.IndexOf("mappView") >= 0))
                 TM611_4_InsertComponent();
            //TM611_3_2_ConfigureMappViewServer();
            TM611_4_1_RenameVIS();
            AddComponents();
        }
        public override void TM611_4_InsertComponent() {
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, null, null);
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Adding mapp View object");
            }
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "mapp View", "mapp View");
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
        void TM611_3_2_ConfigureMappViewServer() {
            string mvconfig = "Config.mappviewcfg";
            if (TreeConfig.IdeMain.GetActiveConfigurtion().FindAllDescendants(cf => cf.ByControlType(ControlType.TreeItem)).First(cf => cf.Name.IndexOf("mappView") >= 0).FindAllChildren(cf => cf.ByName(mvconfig)) == null) {
                if (Verbose >= Environment.Verbose.STEPS) {
                    Console.WriteLine("==========================================");
                    Console.WriteLine("Inserting new mapp View configuration");
                }
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView"}, new List<string> { "_Configuration", "_Configuration" });
                TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "mapp View", "mapp View Configuration");
            }
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Opening new mapp View configuration in workspace");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", "BR_" + mvconfig }, new List<string> { "_Configuration", "_Configuration", "_Configuration" });
            AutomationElement mvConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(mvconfig) >= 0);
            AutomationElement configTree = mvConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            AutomationElement mvConfigRoot = configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_MappViewConfiguration")));
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Selecting HTTP as communication protocol");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathMV + "mappviewcfg.xml", "Protocol"), new List<string> { "_Name", "_Value" }, mvConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 0); //Select "HTTP"
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Selecting anonymous token as Startup User");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathMV + "mappviewcfg.xml", "Startup User"), new List<string> { "_Name", "_Value" }, mvConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 0); //Select "anonymous token"
        }
        void TM611_4_1_RenameVIS() {
            string visname = "vis_0.vis";
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", "BR_" + visname}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" });
            AutomationElement visConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(visname) >= 0);
            AutomationElement adocText = visConfigWorkspaceWindow.FindAllDescendants().First(cf => cf.Name.IndexOf("<?xml") >= 0);
            string sdocText = adocText.Name;

            var range = adocText.Patterns.Value;
        }
        void AddComponents() {
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting new Localizable Texts container");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Resources", "BR_Texts"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Localizable Texts");
            if (Verbose >= Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting new Project Language container");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView"}, new List<string> { "_Object Name" });
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Project Language");
            
        }
    }
}