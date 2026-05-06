using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using FlaUI.Core.Input;
using System.Drawing;
using FlaUI.Core.Tools;
using Point = System.Drawing.Point;
using System.Windows;
using System.Threading;

namespace FlaUITests.Util {
    public partial class MappView {
        string editorPathMV;
        string editorPathTS;
        readonly List<string[]> chartStrings = new List<string[]> {
            {new string [] {"AlarmHistory", "", "", ""} }, 
            {new string [] {"AlarmLine", "", "", ""} }, 
            {new string [] {"AlarmList", "", "", ""} }, 
            {new string [] {"AuditList", "", "", ""} }, 
            {new string [] {"BarChart", "", "", ""} }, 
            {new string [] {"BasicSlider", "", "", ""} }, 
            {new string [] {"Button", "", "", ""} }, 
            {new string [] {"ButtonBar", "", "", ""} }, 
            {new string [] {"CheckBox", "", "", ""} }, 
            {new string [] {"ContentCarousel", "", "", ""} }, 
            {new string [] {"Database", "", "", ""} }, 
            {new string [] {"DateTimeInput", "", "", ""} }, 
            {new string [] {"DateTimeOutput", "", "", ""} }, 
            {new string [] {"DonutChart", "", "", ""} }, 
            {new string [] {"DropDownBox", "", "", ""} }, 
            {new string [] {"Ellipse", "", "", ""} }, 
            {new string [] {"FavoriteWatch", "", "", ""} }, 
            {new string [] {"FlexBox", "", "", ""} }, 
            {new string [] {"FlexLayoutPanel", "", "", ""} }, 
            {new string [] {"FlyOut", "", "", ""} }, 
            {new string [] {"GridLine", "", "", ""} }, 
            {new string [] {"GroupBox", "", "", ""} }, 
            {new string [] {"HoverButton", "", "", ""} }, 
            {new string [] {"Image", "", "", ""} }, 
            {new string [] {"ImageList", "", "", ""} }, 
            {new string [] {"InfoBanner", "", "", ""} }, 
            {new string [] {"Joystick", "", "", ""} }, 
            {new string [] {"Label", "", "", ""} }, 
            {new string [] {"LadderEditor", "", "", ""} }, 
            {new string [] {"Line", "", "", ""} }, 
            {new string [] {"LinearGauge", "", "", ""} }, 
            {new string [] {"Linechart", "", "", ""} }, 
            {new string [] {"ListBox", "", "", ""} }, 
            {new string [] {"Login", "", "", ""} }, 
            {new string [] {"LoginButton", "", "", ""} }, 
            {new string [] {"LoginInfo", "", "", ""} }, 
            {new string [] {"LogoutButton", "", "", ""} }, 
            {new string [] {"MeasurementSystemSelector", "", "", ""} }, 
            {new string [] {"MomentaryPushButton", "", "", ""} }, 
            {new string [] {"MotionPad", "", "", ""} }, 
            {new string [] {"NavigationBar", "", "", ""} }, 
            {new string [] {"NavigationButton", "", "", ""} }, 
            {new string [] {"NumericInput", "", "", ""} }, 
            {new string [] {"NumericOutput", "", "", ""} }, 
            {new string [] {"OnlineChart", "", "", ""} }, 
            {new string [] {"OnlineChartHDA", "", "", ""} }, 
            {new string [] {"Paper", "", "", ""} }, 
            {new string [] {"Password", "", "", ""} }, 
            {new string [] {"PDFViewer", "", "", ""} }, 
            {new string [] {"PieChart", "", "", ""} }, 
            {new string [] {"ProfileGenerator", "", "", ""} }, 
            {new string [] {"ProgressBar", "", "", ""} }, 
            {new string [] {"PushButton", "", "", ""} }, 
            {new string [] {"QRViewer", "", "", ""} }, 
            {new string [] {"RadialButtonBar", "", "", ""} }, 
            {new string [] {"RadialGauge", "", "", ""} }, 
            {new string [] {"RadialSlider", "", "", ""} }, 
            {new string [] {"RadioButton", "", "", ""} }, 
            {new string [] {"RadioButtonGroup", "", "", ""} }, 
            {new string [] {"RangeSlider", "", "", ""} }, 
            {new string [] {"Rectangle", "", "", ""} }, 
            {new string [] {"Sequencer", "", "", ""} }, 
            {new string [] {"SequencerStepItemParameterForm", "", "", ""} }, 
            {new string [] {"SequencerTable", "", "", ""} }, 
            {new string [] {"Skyline", "", "", ""} }, 
            {new string [] {"StackedBarChart", "", "", ""} }, 
            {new string [] {"TabControl", "", "", ""} }, 
            {new string [] {"Table", "", "", ""} }, 
            {new string [] {"TextInput", "", "", ""} }, 
            {new string [] {"TextOutput", "", "", ""} }, 
            {new string [] {"TextPad", "", "", ""} }, 
            {new string [] {"TextPicker", "", "", ""} }, 
            {new string [] {"TimeLine", "", "", ""} }, 
            {new string [] {"ToggleButton", "", "", ""} }, 
            {new string [] {"ToggleSwitch", "", "", ""} }, 
            {new string [] {"UserList", "", "", ""} }, 
            {new string [] {"VideoPlayer", "", "", ""} }, 
            {new string [] {"VncViewer", "", "", ""} }, 
            {new string [] {"WebViewer", "", "", ""} }, 
            {new string [] {"XYChart", "", "", ""} },
            {new string [] {"XYJoystick", "", "", ""} }
        };
        string[] _navStrings = new string[] {"    <NavigationPath refId=\"", "\">\r\n", "      <Destination refId=\"", "\" index=\"0\" />\r\n", "      <Destination refId=\"", "\" index=\"1\" />\r\n", "      <Destination refId=\"", "\" index=\"2\" />\r\n", "    </NavigationPath>\r\n"};
        public override void InitComponent() {
            editorPathMV = Util.Environment.InstallationPath + "\\AS\\TechnologyPackages\\mappView\\" + Version + "\\Editors\\";
            editorPathTS = Util.Environment.InstallationPath + "\\AS\\TechnologyPackages\\TextSystem\\n.d\\Editors\\";
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Checking/setting mapp View version to " + Version);
            }
        //    TreeConfig.IdeMain.SelectComponentVersion("mapp View", Version);
            if (!TreeConfig.IdeMain.GetLogicalViewRoot(Project).FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Any(cf => cf.Name.IndexOf("mappView") >= 0))
                 TM611_4_InsertComponent();
        //    TreeConfig.IdeMain.Build();
        //    TM611_3_2_ConfigureMappViewServer();
        //    TreeConfig.IdeMain.Build();
        //    TM611_4_1_RenameVIS();
        //    TreeConfig.IdeMain.Build();
            TM611_11_Localization();
            TreeConfig.IdeMain.Build();
            TM611_5_Layout();
            TreeConfig.IdeMain.Build();
            InsertWidgets();
            TreeConfig.IdeMain.Build();
            TM611_6_Navigation();
        }
        public override void TM611_4_InsertComponent() {
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, null, null);
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Adding mapp View object");
            }
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "mapp View", "mapp View");
            FlaUI.Core.AutomationElements.Window newMappViewDialog = TreeConfig.IdeMain.GetModalWindow("Insert mapp View solution");
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
            if (TreeConfig.IdeMain.GetActiveConfigurtion().FindAllDescendants(cf => cf.ByControlType(ControlType.TreeItem)).First(cf => cf.Name.IndexOf("mappView") >= 0).FindAllChildren(cf => cf.ByName("BR_" + mvconfig)).Count() == 0) {
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
            TreeConfig.ClickAutomationElement(TreeConfig.IdeMain.MainWindow.TitleBar);
        }
        void TM611_4_1_RenameVIS() {
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Renaming Visu");
            }
            string visname = "vis_0.vis";
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", "BR_" + visname}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" });
            AutomationElement visConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(visname) >= 0);
            AutomationElement adocText = visConfigWorkspaceWindow.FindAllDescendants().First(cf => cf.Name.IndexOf("<?xml") >= 0);
            TreeConfig.IdeMain.RemoveTrailingWhitespaceFromXML(adocText);
            visname = "Test_Visu";
            Rectangle rec = TreeConfig.IdeMain.FindWordinCapture(adocText, "\"vis_0\"");
            Mouse.MoveTo(new Point {X = adocText.BoundingRectangle.X + rec.X + rec.Width/2, Y = adocText.BoundingRectangle.Y + rec.Y});
            Mouse.DoubleClick();
            Keyboard.Type(visname);
            TreeConfig.IdeMain.SaveAll();
            rec = TreeConfig.IdeMain.Workspace.FindFirstChild(cf => cf.ByControlType(ControlType.Tab)).FindFirstChild(cf => cf.ByControlType(ControlType.TabItem).And(cf.ByName(visname + ".vis [XML File]"))).BoundingRectangle;
            Mouse.MoveTo(new Point {X = rec.Right - 10, Y = rec.Top + 10});
            Mouse.Click();
        }
        void TM611_11_Localization() {
            string tmxconfig = "LocalizableTexts.tmx";
/*              if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting new Project Language container");
            } 
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView"}, new List<string> { "_Object Name" });
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Project Languages");
 */            TreeConfig.IdeMain.SaveAll();
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting new Localizable Texts container and changing namespace to IAT");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Resources", "BR_Texts"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Localizable Texts");
            TreeConfig.IdeMain.SaveAll();
            foreach(string[] text in chartStrings) {
                text[1] = "fr_" + text[0];
                text[2] = "de_" + text[0];
                text[3] = "en_" + text[0];
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Resources", "BR_Texts", "BR_" + tmxconfig}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            AutomationElement tmxConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf(tmxconfig) >= 0);
            AutomationElement editNamespace = tmxConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByAutomationId("textNamespace")).AsTextBox();
            editNamespace.Patterns.Value.Pattern.SetValue("IAT");
            TreeConfig.IdeMain.SaveAll();
            AutomationElement textTree = tmxConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByAutomationId("B&R TreeView Control")).AsTree();
            AutomationElement newItem;
             foreach (string[] item in chartStrings) {
                newItem = textTree.FindAllChildren().Last();
                AutomationElement [] fields = newItem.FindAllChildren();
                TreeConfig.ClickAutomationElement(fields[0]);
                Keyboard.Type(item[0]);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(800));
                TreeConfig.ClickAutomationElement(fields[1]);
                Keyboard.Type(item[1]);
                TreeConfig.ClickAutomationElement(fields[2]);
                Keyboard.Type(item[2]);
                TreeConfig.ClickAutomationElement(fields[3]);
                Keyboard.Type(item[3]);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                TreeConfig.ClickAutomationElement(tmxConfigWorkspaceWindow);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
            }
             TreeConfig.IdeMain.SaveAll();
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting and editing Textsystem Config File");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_TextSystem"}, new List<string> { "_Configuration", "_Configuration" });
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "Textsystem Configuration");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_TextSystem", "BR_TC.textconfig"}, new List<string> { "_Configuration", "_Configuration", "_Configuration" });
            AutomationElement TCConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf("TC.textconfig") >= 0);
            AutomationElement configTree = TCConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Tree));
            AutomationElement TCConfigRoot = configTree.FindFirstChild(cf => cf.ByControlType(ControlType.TreeItem).And(cf.ByName("BR_TextConfig")));
            Mouse.Click(TCConfigWorkspaceWindow.BoundingRectangle.Center());

            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "System language"), new List<string> { "_Name", "_Value" }, TCConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, "en");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Fallback language"), new List<string> { "_Name", "_Value" }, TCConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, "de");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Target languages", "BR_Target language 1"), new List<string> { "_Name", "_Name", "_Value" }, TCConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, "en");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Target languages", "BR_Target language 2"), new List<string> { "_Name", "_Name", "_Value" }, TCConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, "de");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Target languages", "BR_Target language 3"), new List<string> { "_Name", "_Name", "_Value" }, TCConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, "fr");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Tmx files for target", "BR_Tmx file 1"), new List<string> { "_Name", "_Value" }, TCConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(TreeConfig.IdeMain.MainWindow, 0);
            TreeConfig.IdeMain.SaveAll();
        }
        void TM611_5_Layout() {
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting navigation and info content");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_content_0.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            AutomationElement content_0ConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).First(cf => cf.Name.IndexOf("content_0.content") >= 0);
            Point editorCenter = content_0ConfigWorkspaceWindow.BoundingRectangle.Center();
            
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Page content");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents", "BR_content_1.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            AutomationElement properties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement name = properties.FindFirstDescendant(cf => cf.ByName("Name"));
            Mouse.Click(new Point {X = name.BoundingRectangle.Right - 20, Y = name.BoundingRectangle.Top + name.BoundingRectangle.Height/2});
            Mouse.DoubleClick(new Point {X = name.BoundingRectangle.Right - 20, Y = name.BoundingRectangle.Top + name.BoundingRectangle.Height/2});
            Keyboard.Type("Navigation");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            EditSize(width:100, height:500, content:true);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", "Navigation", true, content_0ConfigWorkspaceWindow.BoundingRectangle.Center());
            //properties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            EditPosition(top:0, left:0);
            EditSize(width:100, height:500);
            TreeConfig.IdeMain.SaveAll(); 

            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Page content");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents", "BR_content_1.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            properties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            name = properties.FindFirstDescendant(cf => cf.ByName("Name"));
            Mouse.Click(new Point {X = name.BoundingRectangle.Right - 20, Y = name.BoundingRectangle.Top + name.BoundingRectangle.Height/2});
            Mouse.DoubleClick(new Point {X = name.BoundingRectangle.Right - 20, Y = name.BoundingRectangle.Top + name.BoundingRectangle.Height/2});
            Keyboard.Type("Info_Pane");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            EditSize(height:100, content:true);
            TreeConfig.IdeMain.SaveAll();
            Mouse.Click(editorCenter);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", "Label", drag:true, toDrag:editorCenter);
            EditSize(width:200, height:30);
            EditPosition(left:50, top:5);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", "LanguageSelector", drag:true, toDrag:editorCenter);
            EditPosition(left:680, top:35);
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Preparing Layout for all Pages");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_content_0.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            EditSize(width:700, height:500, content:true);
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Layouts", "BR_layout_0.layout"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            AutomationElement layout_0ConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).First(cf => cf.Name.IndexOf("layout_0.layout") >= 0);
            Mouse.Click(editorCenter);
            EditSize(width:700, height:500, area:true);
            EditPosition(left:100, top:100, area:true);
            FlaUI.Core.AutomationElements.Button createArea = layout_0ConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Create Area"))).AsButton();
            createArea.Click();
            EditSize(width:100, height:500, area:true);
            EditPosition(left:0, top:100, area:true);
            createArea.Click();
            EditSize(width:800, height:100, area:true);
            EditPosition(left:0, top:0, area:true);
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_page_0.page"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            AutomationElement page_0ConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).First(cf => cf.Name.IndexOf("page_0.page") >= 0);
            Mouse.Click(editorCenter);
            AutomationElement editor = page_0ConfigWorkspaceWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document).And(cf.ByName("Page-Editor")));
            TreeConfig.IdeMain.SetIWorkspaceMinSize(editor, percent:true);
            Mouse.MoveTo(new Point {X = editor.BoundingRectangle.Left + (int)(editor.BoundingRectangle.Width * 50/800), Y = editor.BoundingRectangle.Top + (int)(editor.BoundingRectangle.Height * 300/600)});
            Mouse.Click();
            SelectFromMappViewDropDown(new string [] {"Common", "refId"}, "Navigation");
            Mouse.MoveTo(new Point {X = editor.BoundingRectangle.Left + (int)(editor.BoundingRectangle.Width * 400/800), Y = editor.BoundingRectangle.Top + (int)(editor.BoundingRectangle.Height * 50/600)});
            Mouse.Click();
            SelectFromMappViewDropDown(new string [] {"Common", "refId"}, "Info_Pane");
            TreeConfig.IdeMain.SaveAll();
        }
        void InsertWidgets() {
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting widgets");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_content_0.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            AutomationElement content_0ConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).First(cf => cf.Name.IndexOf("content_0.content") >= 0);
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
            int pageID = 0;
            string pageName, contentName;
            foreach(string[] text in chartStrings) {
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
                AutomationElement [] allChildren = TreeConfig.IdeMain.ToolBarStandard.FindAllChildren();
                TreeConfig.IdeMain.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nCopy ")).AsButton().Click();
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages"}, new List<string> { "_Object Name", "_Object Name", "_Object Name" });
                TreeConfig.IdeMain.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nPaste ")).AsButton().Click();
                pageID++;
                pageName = "page_" + pageID;
                contentName = "content_" + pageID;
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_" + pageName, "BR_" + contentName + ".content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
                TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", text[0], drag:true, toDrag:editorCenter);
                EditSize(width:500, height:400);
                EditPosition(left:100, top:50);
            }
        }   
        void TM611_6_Navigation() {
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Creating navigation file");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView"}, new List<string> { "_Configuration", "_Configuration" });
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "Navigation");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", "BR_navigation_0.nav"}, new List<string> { "_Configuration", "_Configuration", "_Configuration" });
            AutomationElement navConfigWorkspaceWindow = TreeConfig.IdeMain.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf("navigation_0.nav [XML File]") >= 0);
            AutomationElement editor = navConfigWorkspaceWindow.FindAllDescendants().FirstOrDefault(cf => cf.Name.Contains("<?xml version")).AsTextBox();
            TreeConfig.IdeMain.RemoveTrailingWhitespaceFromXML(editor);
            Keyboard.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            TreeConfig.IdeMain.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nCopy ")).AsButton().Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            string copiedText = Clipboard.GetText();
            while (copiedText.ElementAt(0) != '<') copiedText = copiedText.Substring(1);
            int firstIndex = copiedText.IndexOf("    <NavigationPath ");
            int secondIndex = copiedText.IndexOf("  </NavigationPaths>");
            string outText = copiedText.Substring(0, firstIndex);
            int pageID = 0;
            string pageName, page0Name = "page_0";
            for(int i=0; i<chartStrings.Count+1; i++) {
                outText += _navStrings[0];
                pageName = "page_" + pageID;
                outText += pageName;
                outText += _navStrings[1] + _navStrings[2];
                outText += page0Name;
                outText += _navStrings[3] + _navStrings[4];
                pageName = "page_" + (pageID==0?0:(pageID-1));
                outText += pageName;
                outText += _navStrings[5];
                if (i != chartStrings.Count) {
                    outText += _navStrings[6];
                    pageName = "page_" + (pageID+1);
                    outText += pageName;
                    outText += _navStrings[7];
                }
                outText += _navStrings[8];
                pageID++;
            }
            outText += copiedText.Substring(secondIndex);
            Copy_to_clipboard(outText);
            Keyboard.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A);
            TreeConfig.IdeMain.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nPaste ")).AsButton().Click();
            TreeConfig.IdeMain.SaveAll();
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Connecting it to Navigation Widget");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents", "BR_Navigation.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            Mouse.Click(navConfigWorkspaceWindow.BoundingRectangle.Center());
            SelectFromMappViewDropDown(new string [] {"Data", "navRefId"}, "navigation_0");
        }
        void SelectFromMappViewDropDown(string [] stree, string select) {
            if (Verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine("Selecting " + select + " from Dropdown: " + stree[0] + ", " + stree[1]);
            AutomationElement properties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            Mouse.Position = properties.BoundingRectangle.Center();
            AutomationElement first = properties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
            List<AutomationElement>  atree = new List<AutomationElement> { properties.FindFirstChild(cf => cf.ByName(stree[0])) };
            atree.Add(atree.ElementAt(0).FindFirstChild(cf => cf.ByName(stree[1])));
            while (!properties.BoundingRectangle.IntersectsWith(first.BoundingRectangle)) {
                Mouse.Scroll(1d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
            while (!properties.BoundingRectangle.IntersectsWith(atree.ElementAt(1).BoundingRectangle)) {
                Mouse.Scroll(-1d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
            Mouse.Scroll(-4d);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            Mouse.DoubleClick(new Point {X = atree.ElementAt(1).BoundingRectangle.Right - 15, Y = atree.ElementAt(1).BoundingRectangle.Top + atree.ElementAt(1).BoundingRectangle.Height/2});
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Rectangle rec = TreeConfig.IdeMain.FindWordinCapture(properties, select);
            Point point = new Point {X = properties.BoundingRectangle.Left + rec.Left + rec.Width/2, Y = properties.BoundingRectangle.Top + rec.Top + rec.Height/2};
            Mouse.Click(point);
        }
        void EditSize(int width = -1, int height = -1, bool content = false, bool area = false) {
            AutomationElement aproperties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            Mouse.Position = aproperties.BoundingRectangle.Center();
            AutomationElement afirst = aproperties.FindFirstChild();
            if (content) {
                AutomationElement aproperty = aproperties.FindFirstChild(cf => cf.ByName("Property"));
                AutomationElement aheight = aproperty.FindFirstChild(cf => cf.ByName("height"));
                AutomationElement awidth = aproperty.FindFirstChild(cf => cf.ByName("width"));
                if (width != -1) {
                    Mouse.DoubleClick(new Point {X = awidth.BoundingRectangle.Right - 20, Y = awidth.BoundingRectangle.Top + awidth.BoundingRectangle.Height/2});
                    Keyboard.Type("" + width);
                    Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                }
                if (height != -1) {
                    Mouse.DoubleClick(new Point {X = aheight.BoundingRectangle.Right - 20, Y = aheight.BoundingRectangle.Top + aheight.BoundingRectangle.Height/2});
                    Keyboard.Type("" + height);
                    Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                }
            }
            else { if (area) {
                    AutomationElement layout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
                    AutomationElement aheight = layout.FindFirstChild(cf => cf.ByName("height"));
                    AutomationElement awidth = layout.FindFirstChild(cf => cf.ByName("width"));
                    if (width != -1) {
                        Mouse.DoubleClick(new Point {X = awidth.BoundingRectangle.Right - 20, Y = awidth.BoundingRectangle.Top + awidth.BoundingRectangle.Height/2});
                        Keyboard.Type("" + width);
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                    }
                    if (height != -1) {
                        Mouse.DoubleClick(new Point {X = aheight.BoundingRectangle.Right - 20, Y = aheight.BoundingRectangle.Top + aheight.BoundingRectangle.Height/2});
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
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    AutomationElement s_width = size.FindFirstChild(cf => cf.ByName("width"));
                    if (width != -1 && int.Parse(s_width.Patterns.Value.Pattern.Value) != width) {
                        Mouse.DoubleClick(new Point {X = s_width.BoundingRectangle.Right - 20, Y = s_width.BoundingRectangle.Top + s_width.BoundingRectangle.Height/2});
                        Keyboard.Type("" + width);
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                    }
                    AutomationElement s_height = size.FindFirstChild(cf => cf.ByName("height"));
                    if (height != -1 && int.Parse(s_height.Patterns.Value.Pattern.Value) != height) {
                        Mouse.DoubleClick(new Point {X = s_height.BoundingRectangle.Right - 20, Y = s_height.BoundingRectangle.Top + s_height.BoundingRectangle.Height/2});
                        Keyboard.Type("" + height);
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                    }
                }
            }
            TreeConfig.IdeMain.SaveAll();
        }
        void EditPosition(int top = -1, int left = -1, bool area = false) {
            AutomationElement aproperties = TreeConfig.IdeMain.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            Mouse.Position = aproperties.BoundingRectangle.Center();
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
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
            AutomationElement p_top = (area?layout:position).FindFirstChild(cf => cf.ByName("top"));
            AutomationElement p_left = (area?layout:position).FindFirstChild(cf => cf.ByName("left"));
            if (top != -1 && int.Parse(p_top.Patterns.Value.Pattern.Value) != top) {
                Mouse.DoubleClick(new Point {X = p_top.BoundingRectangle.Right - 20, Y = p_top.BoundingRectangle.Top + p_top.BoundingRectangle.Height/2});
                Keyboard.Type("" + top);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            }
            if (left != -1 && int.Parse(p_left.Patterns.Value.Pattern.Value) != left) {
                Mouse.DoubleClick(new Point {X = p_left.BoundingRectangle.Right - 20, Y = p_left.BoundingRectangle.Top + p_left.BoundingRectangle.Height/2});
                Keyboard.Type("" + left);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            }
            TreeConfig.IdeMain.SaveAll();
        }
        public static void SomethingToRunInThread(Object o) {
            System.Windows.Clipboard.SetText((string) o);
        }
        protected void Copy_to_clipboard(string text) {
            //ParameterizedThreadStart  p = new ParameterizedThreadStart (SomethingToRunInThread);
            Thread th = new Thread(SomethingToRunInThread);
            th.SetApartmentState(ApartmentState.STA);
            th.IsBackground = false;
            th.Start(text);
            th.Join();
        }
    }
}