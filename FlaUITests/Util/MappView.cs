using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Tesseract;
using FlaUI.Core.Capturing;
using FlaUI.Core.Input;
using System.Drawing;
using System.Windows.Forms.VisualStyles;
//using System.Windows;
using FlaUI.Core.Tools;
using Point = System.Drawing.Point;

namespace FlaUITests.Util {
    public partial class MappView {
        string editorPathMV;
        public override void InitComponent() {
            editorPathMV = Util.Environment.InstallationPath + "\\AS\\TechnologyPackages\\mappView\\" + Version + "\\Editors\\";
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Checking/setting mapp View version to " + Version);
            }
            //TreeConfig.IdeMain.SelectComponentVersion("mapp View", Version);
            //if (!TreeConfig.IdeMain.GetLogicalViewRoot(Project).FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Any(cf => cf.Name.IndexOf("mappView") >= 0))
            //     TM611_4_InsertComponent();
            //TM611_3_2_ConfigureMappViewServer();
            //TM611_4_1_RenameVIS();
            TM611_11_Localization();
        }
        public override void TM611_4_InsertComponent() {
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, null, null);
            if (Verbose >= Util.Environment.Verbose.STEPS) {
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
                if (Verbose >= Util.Environment.Verbose.STEPS) {
                    Console.WriteLine("==========================================");
                    Console.WriteLine("Inserting new mapp View configuration");
                }
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView"}, new List<string> { "_Configuration", "_Configuration" });
                TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "mapp View", "mapp View Configuration");
            }
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Opening new mapp View configuration in workspace");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", "BR_" + mvconfig }, new List<string> { "_Configuration", "_Configuration", "_Configuration" });
            AutomationElement mvConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(mvconfig) >= 0);
            AutomationElement configTree = mvConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            AutomationElement mvConfigRoot = configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_MappViewConfiguration")));
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Selecting HTTP as communication protocol");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathMV + "mappviewcfg.xml", "Protocol"), new List<string> { "_Name", "_Value" }, mvConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 0); //Select "HTTP"
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Selecting anonymous token as Startup User");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathMV + "mappviewcfg.xml", "Startup User"), new List<string> { "_Name", "_Value" }, mvConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 0); //Select "anonymous token"
        }
        void TM611_4_1_RenameVIS() {
            Dictionary<Rectangle, string> dict = new Dictionary<Rectangle, string>();
            string visname = "vis_0.vis";
            PageIteratorLevel containingWord = PageIteratorLevel.Word;
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", "BR_" + visname}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" });
            AutomationElement visConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(visname) >= 0);
            AutomationElement adocText = visConfigWorkspaceWindow.FindAllDescendants().First(cf => cf.Name.IndexOf("<?xml") >= 0);
            using (var engine = new TesseractEngine(System.Environment.CurrentDirectory + "\\FlaUITests\\Util\\tessdata", "eng", EngineMode.Default)) {
                CaptureImage compImg = Capture.Element(adocText);
                string file = System.Environment.CurrentDirectory + "\\FlaUITests\\Util\\screenshots\\OCR_vis.png";
                compImg.ToFile(file);
                using (Page page = engine.Process(Pix.LoadFromFile(file))) {
                    using (var iter = page.GetIterator()) {
                        iter.Begin();
                        do {
                            if (iter.TryGetBoundingBox(containingWord, out var rect))
                                dict.Add(new Rectangle(rect.X1, rect.Y1, rect.X2-rect.X1, rect.Y2-rect.Y1), iter.GetText(containingWord));
                        } while (iter.Next(containingWord));
                    }
                }           
            }
            string vis = "\"vis_0\"";
            Rectangle rec = new Rectangle();
            int i, min = int.MaxValue;
            foreach (var d in dict) {
                i = Util.GetDamerauLevenshteinDistance(vis, d.Value);
                if (i < min) {
                    min = i;
                    rec = d.Key;
                }
            }
            Mouse.MoveTo(new Point {X = adocText.BoundingRectangle.X + rec.X, Y = adocText.BoundingRectangle.Y + rec.Y});
            Mouse.DoubleClick();
            Keyboard.Type("Test_Visu");
        }
        void TM611_11_Localization() {
            string tmxconfig = "LocalizableTexts.tmx";
    /*        if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting new Project Language container");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView"}, new List<string> { "_Object Name" });
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Project Languages");
            TreeConfig.IdeMain.SaveAll();
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting new Localizable Texts container and changing namespace to IAT");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Resources", "BR_Texts"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Localizable Texts");
            TreeConfig.IdeMain.SaveAll();
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Resources", "BR_Texts", "BR_" + tmxconfig}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            AutomationElement tmxConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(tmxconfig) >= 0);
            AutomationElement editNamespace = tmxConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByAutomationId("textNamespace")).AsTextBox();
            editNamespace.Patterns.Value.Pattern.SetValue("IAT");
            TreeConfig.IdeMain.SaveAll();
            AutomationElement textTree = tmxConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByAutomationId("B&R TreeView Control")).AsTree();
            AutomationElement newItem;
            foreach (string[] item in testLocalizeableStrings) {
                newItem = textTree.FindAllChildren().Last();
                AutomationElement [] fields = newItem.FindAllChildren();
                fields[0].AsTextBox().Patterns.Value.Pattern.SetValue(item[0]);
                fields[1].AsTextBox().Patterns.Value.Pattern.SetValue(item[1]);
                fields[2].AsTextBox().Patterns.Value.Pattern.SetValue(item[2]);
                TreeConfig.ClickAutomationElement(fields[3]);
                Keyboard.Type(item[3]);
                //fields[3].AsTextBox().Patterns.Value.Pattern.SetValue(item[3]);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                TreeConfig.ClickAutomationElement(tmxConfigWorkspaceWindow);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
            TreeConfig.IdeMain.SaveAll();
    */        if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting widgets");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_content_0.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            AutomationElement content_0ConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf("content_0.content") >= 0);
            Point editorCenter = content_0ConfigWorkspaceWindow.BoundingRectangle.Center();
            TreeConfig.IdeMain.InitializeViews(propertyWindow:true);
            TreeConfig.IdeMain.SwitchView(TreeConfig.ViewType.PropertyWindow);
            TreeConfig.ClickAutomationElement(content_0ConfigWorkspaceWindow);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            AutomationElement content_0Properties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            Size content_0Size = new Size();
            AutomationElement ae = content_0Properties.FindFirstDescendant(cf => cf.ByName("height"));
            content_0Size.Height = int.Parse(content_0Properties.FindFirstDescendant(cf => cf.ByName("height")).Patterns.Value.ToString());
            content_0Size.Width = int.Parse(content_0Properties.FindFirstDescendant(cf => cf.ByName("width")).Patterns.Value.ToString());
            AutomationElement docIATeditor = content_0ConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document).And(cf.ByName("IAT-Editor")));
            AutomationElement defaultLabel = docIATeditor.FindFirstDescendant(cf => cf.ByAutomationId("content_0_Label1"));
            TreeConfig.ClickAutomationElement(defaultLabel);
            //Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.DELETE);
            TreeConfig.IdeMain.SetIWorkspaceMinSize(docIATeditor);
            int tabSize = 1;
            while (tabSize*tabSize < testLocalizeableStrings.Count) tabSize++;
            tabSize++;
            int stepX = content_0Size.Width/tabSize;
            int stepY = content_0Size.Height/tabSize;
            int stepXvis = content_0ConfigWorkspaceWindow.BoundingRectangle.Width/tabSize;
            int stepYvis = content_0ConfigWorkspaceWindow.BoundingRectangle.Height/tabSize;
            int i = 0, j = 0;
            foreach(string[] text in testLocalizeableStrings) {
                Point p = new Point { X = content_0ConfigWorkspaceWindow.BoundingRectangle.Left + (int) (stepXvis * (i + 0.25f)), Y = content_0ConfigWorkspaceWindow.BoundingRectangle.Top + (int) (stepYvis * (j + 0.25f)) };
                TreeConfig.ClickAutomationElement(content_0ConfigWorkspaceWindow);
                TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", text[0], drag: true, p);
                Mouse.Click(p);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
                AutomationElement properties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            }
        }
        readonly List<string[]> testLocalizeableStrings = new List<string[]> {
            {new string [] {"BarChart", "fr_BarChart", "de_BarChart", "en_BarChart"} }, 
            {new string [] {"Button", "fr_Button", "de_Button", "en_Button"} }, 
            {new string [] {"Label", "fr_Label", "de_Label", "en_Label"} }, 
            {new string [] {"Navigation", "fr_Navigation", "de_Navigation", "en_Navigation"} }, 
            {new string [] {"1", "fr_", "de_", "en_"} }, 
            {new string [] {"2", "fr_", "de_", "en_"} }
        };
    }
}