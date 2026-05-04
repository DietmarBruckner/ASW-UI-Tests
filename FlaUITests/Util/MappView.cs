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
using FlaUI.Core;

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
            TreeConfig.IdeMain.SelectComponentVersion("mapp View", Version);
            if (!TreeConfig.IdeMain.GetLogicalViewRoot(Project).FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Any(cf => cf.Name.IndexOf("mappView") >= 0))
                 TM611_4_InsertComponent();
            TM611_3_2_ConfigureMappViewServer();
            TM611_4_1_RenameVIS();
            TM611_11_Localization();
            TM611_5_Layout();
            //TM611_6_Navigation();
            InsertWidgets();
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
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView"}, new List<string> { "_Configuration", "_Configuration" });
            AutomationElement asd = TreeConfig.IdeMain.GetActiveConfigurtion().FindAllDescendants(cf => cf.ByControlType(ControlType.TreeItem)).First(cf => cf.Name.IndexOf("mappView") >= 0);
            AutomationElement [] asdsdf = asd.FindAllChildren(cf => cf.ByName(mvconfig));
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
            if (Verbose >= Util.Environment.Verbose.STEPS) {
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
        }
        void TM611_5_Layout() {
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting navigation content");
            }
            AutomationElement content_0ConfigWorkspaceWindow;
            if ((content_0ConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).First(cf => cf.Name.IndexOf("content_0.content") >= 0)) == null) {
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_content_0.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
                content_0ConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).First(cf => cf.Name.IndexOf("content_0.content") >= 0);
            }
            Point editorCenter = content_0ConfigWorkspaceWindow.BoundingRectangle.Center();
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", "Page content");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents", "BR_content_1.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            AutomationElement properties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement name = properties.FindFirstChild(cf => cf.ByName("Name"));
            TreeConfig.ClickAutomationElement(name.FindFirstChild(cf => cf.ByName("Name").And(cf.ByControlType(ControlType.Edit))), true);
            Keyboard.Type("Navigation");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            EditSize(width:100, content:true);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", "NavigationBar", true, content_0ConfigWorkspaceWindow.BoundingRectangle.Center());
            properties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement accessibility = properties.FindFirstChild();
            AutomationElement behavior = properties.FindFirstChild(cf => cf.ByName("Behavior"));
            AutomationElement childPos = behavior.FindFirstChild(cf => cf.ByName("childPositioning"));
            while (!properties.BoundingRectangle.IntersectsWith(accessibility.BoundingRectangle)) {
                Mouse.Scroll(1d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
            while (!properties.BoundingRectangle.IntersectsWith(behavior.BoundingRectangle)) {
                Mouse.Scroll(-1d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
            Mouse.Scroll(-2d);
            TreeConfig.ClickAutomationElement(childPos.FindFirstChild(cf => cf.ByName("childPositioning").And(cf.ByControlType(ControlType.Edit))), true);
            EditPosition(top:0, left:0);
            EditSize(width:100, height:600);
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_content_0.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            EditSize(width:700, content:true);
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Layouts", "BR_layout_0.layout"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            AutomationElement layout_0ConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).First(cf => cf.Name.IndexOf("layout_0.layout") >= 0);
            Mouse.Click(editorCenter);
            EditSize(width:700, height:600, area:true);
            EditPosition(left:100, area:true);
            Button createArea = layout_0ConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Create Area"))).AsButton();
            createArea.Click();
            EditSize(width:100, height:600, area:true);
            EditPosition(left:0, top:100, area:true);
            AutomationElement page_0ConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).First(cf => cf.Name.IndexOf("page_0.page") >= 0);
            TreeConfig.ClickAutomationElement(TreeConfig.IdeMain.Workspace.FindFirstChild(cf => cf.ByControlType(ControlType.Tab)).FindFirstChild(cf => cf.ByControlType(ControlType.TabItem).And(cf.ByName("page_0.page"))));
            Mouse.Click(editorCenter);
            AutomationElement editor = page_0ConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document).And(cf.ByName("Page-Editor")));
            TreeConfig.IdeMain.SetIWorkspaceMinSize(editor, percent:true);
            Mouse.Click(new Point {X = editor.BoundingRectangle.Left + (int)(editor.BoundingRectangle.Width * 50/800), Y = editor.BoundingRectangle.Top + (int)(editor.BoundingRectangle.Height * 300/600)});
            properties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement common = properties.FindFirstChild(cf => cf.ByName("Common"));
            AutomationElement refID = common.FindFirstChild(cf => cf.ByName("refID"));
            TreeConfig.ClickAutomationElement(refID.FindFirstChild(cf => cf.ByName("Open").And(cf.ByControlType(ControlType.Button))));
            PageIteratorLevel containingWord = PageIteratorLevel.Word;
            Rectangle toClick = new Rectangle();
            using (var engine = new TesseractEngine(System.Environment.CurrentDirectory + "\\FlaUITests\\Util\\tessdata", "eng", EngineMode.Default)) {
                CaptureImage compImg = Capture.Element(properties);
                string file = System.Environment.CurrentDirectory + "\\FlaUITests\\Util\\screenshots\\OCR_refID.png";
                compImg.ToFile(file);
                using (Page page = engine.Process(Pix.LoadFromFile(file))) {
                    using (var iter = page.GetIterator()) {
                        iter.Begin();
                        do {
                            if (iter.TryGetBoundingBox(containingWord, out var rect))
                                if (iter.GetText(containingWord).IndexOf("Navigation") >= 0)
                                toClick = new Rectangle(rect.X1, rect.Y1, rect.X2-rect.X1, rect.Y2-rect.Y1);
                        } while (iter.Next(containingWord));
                    }
                }           
            }
            Mouse.Click(toClick.Center());
        }
        void InsertWidgets() {
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting widgets");
            }
            AutomationElement content_0ConfigWorkspaceWindow;
            if ((content_0ConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).First(cf => cf.Name.IndexOf("content_0.content") >= 0)) == null) {
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_content_0.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
                content_0ConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).First(cf => cf.Name.IndexOf("content_0.content") >= 0);
            }
            Point editorCenter = content_0ConfigWorkspaceWindow.BoundingRectangle.Center();
            Mouse.Click(editorCenter);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            AutomationElement content_0Properties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement docIATeditor = content_0ConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document).And(cf.ByName("IAT-Editor")));
            AutomationElement defaultLabel = docIATeditor.FindFirstDescendant(cf => cf.ByAutomationId("content_0_Label1"));
            TreeConfig.ClickAutomationElement(defaultLabel);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.DELETE);
            TreeConfig.IdeMain.SetIWorkspaceMinSize(docIATeditor);
            
            int pageID = 1;
            string pageName, contentName;
            foreach(string[] text in testLocalizeableStrings) {
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
                TreeConfig.IdeMain.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nCopy")).AsButton().Click();
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages"}, new List<string> { "_Object Name", "_Object Name", "_Object Name" });
                pageID++;
                pageName = "page_" + pageID;
                contentName = "content_" + pageID;
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_" + pageName, "BR_" + contentName}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
                TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", text[0], drag:true, toDrag:editorCenter);
                EditSize(width:500, height:500);
                EditPosition(left:100, top:150);
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
        void EditSize(int width = -1, int height = -1, bool content = false, bool area = false) {
            AutomationElement aproperties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement afirst = aproperties.FindFirstChild();
            if (content) {
                AutomationElement aproperty = aproperties.FindFirstChild(cf => cf.ByName("Property"));
                AutomationElement aheight = aproperty.FindFirstChild(cf => cf.ByName("height"));
                AutomationElement awidth = aproperty.FindFirstChild(cf => cf.ByName("width"));
                if (width != -1) {
                    TreeConfig.ClickAutomationElement(awidth.FindFirstChild(cf => cf.ByName("width").And(cf.ByControlType(ControlType.Edit))), true);
                    Keyboard.Type("" + width);
                    Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                }
                if (height != -1) {
                    TreeConfig.ClickAutomationElement(aheight.FindFirstChild(cf => cf.ByName("height").And(cf.ByControlType(ControlType.Edit))), true);
                    Keyboard.Type("" + height);
                    Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                }
            }
            else { if (area) {
                    AutomationElement layout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
                    AutomationElement aheight = layout.FindFirstChild(cf => cf.ByName("height"));
                    AutomationElement awidth = layout.FindFirstChild(cf => cf.ByName("width"));
                    if (width != -1) {
                        TreeConfig.ClickAutomationElement(awidth.FindFirstChild(cf => cf.ByName("width").And(cf.ByControlType(ControlType.Edit))), true);
                        Keyboard.Type("" + width);
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                    }
                    if (height != -1) {
                        TreeConfig.ClickAutomationElement(aheight.FindFirstChild(cf => cf.ByName("height").And(cf.ByControlType(ControlType.Edit))), true);
                        Keyboard.Type("" + height);
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                    }
                }
                else {
                    AutomationElement layout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
                    AutomationElement size = layout.FindFirstChild(cf => cf.ByName("Size"));
                    while (!aproperties.BoundingRectangle.IntersectsWith(afirst.BoundingRectangle)) {
                        Mouse.Scroll(1d);
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    }
                    while (!aproperties.BoundingRectangle.IntersectsWith(size.BoundingRectangle)) {
                        Mouse.Scroll(-1d);
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    }
                    Mouse.Scroll(-2d);
                    Mouse.Click(new Point {X = size.BoundingRectangle.Left + 5, Y = size.BoundingRectangle.Top + 5});
                    Mouse.Scroll(-2d);
                    AutomationElement s_width = size.FindFirstChild(cf => cf.ByName("width"));
                    if (width != -1 && int.Parse(s_width.Patterns.Value.Pattern.Value) != width) {
                        TreeConfig.ClickAutomationElement(s_width.FindFirstChild(cf => cf.ByName("width").And(cf.ByControlType(ControlType.Edit))), true);
                        Keyboard.Type("" + width);
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                    }
                    AutomationElement s_height = size.FindFirstChild(cf => cf.ByName("height"));
                    if (height != -1 && int.Parse(s_height.Patterns.Value.Pattern.Value) != height) {
                        TreeConfig.ClickAutomationElement(s_height.FindFirstChild(cf => cf.ByName("height").And(cf.ByControlType(ControlType.Edit))), true);
                        Keyboard.Type("" + height);
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                    }
                }
            }
            TreeConfig.IdeMain.SaveAll();
        }
        void EditPosition(int top = -1, int left = -1, bool area = false) {
            AutomationElement aproperties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement afirst = aproperties.FindFirstChild();
            AutomationElement layout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
            AutomationElement position = null;
            while (!aproperties.BoundingRectangle.IntersectsWith(afirst.BoundingRectangle)) {
                Mouse.Scroll(1d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
            if (area) {
                while (!aproperties.BoundingRectangle.IntersectsWith(layout.BoundingRectangle)) {
                    Mouse.Scroll(-1d);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                }
                Mouse.Scroll(-2d);
            }
            else {
                position = layout.FindFirstChild(cf => cf.ByName("Position"));
                while (!aproperties.BoundingRectangle.IntersectsWith(position.BoundingRectangle)) {
                    Mouse.Scroll(-1d);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                }
                Mouse.Scroll(-2d);
                Mouse.Click(new Point {X = position.BoundingRectangle.Left + 5, Y = position.BoundingRectangle.Top + 5});
                Mouse.Scroll(-2d);
            }
            AutomationElement p_top = (area?layout:position).FindFirstChild(cf => cf.ByName("top"));
            AutomationElement p_left = (area?layout:position).FindFirstChild(cf => cf.ByName("left"));
            if (top != -1 && int.Parse(p_top.Patterns.Value.Pattern.Value) != top) {
                TreeConfig.ClickAutomationElement(p_top.FindFirstChild(cf => cf.ByName("top").And(cf.ByControlType(ControlType.Edit))), true);
                Keyboard.Type("" + top);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            }
            if (left != -1 && int.Parse(p_left.Patterns.Value.Pattern.Value) != left) {
                TreeConfig.ClickAutomationElement(p_left.FindFirstChild(cf => cf.ByName("left").And(cf.ByControlType(ControlType.Edit))), true);
                Keyboard.Type("" + left);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            }
            TreeConfig.IdeMain.SaveAll();
        }
    }
}