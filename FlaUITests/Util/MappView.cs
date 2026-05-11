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
using FlaUITests.Util.AS_Objects;
using System.Runtime.InteropServices;
using System.IO;
using System.ComponentModel;
using System.Xml.Linq;

namespace FlaUITests.Util {
    public partial class MappView {
        string editorPathMV;
        string editorPathTS;
        IDE_Main.Editor content0_editor, navcontent_editor;
        readonly List<string[]> inputWidgetStrings = new List<string[]>();
        string[] _navStrings = new string[] {"    <NavigationPath refId=\"", "\">\r\n", "      <Destination refId=\"", "\" index=\"0\" />\r\n", "      <Destination refId=\"", "\" index=\"1\" />\r\n", "      <Destination refId=\"", "\" index=\"2\" />\r\n", "    </NavigationPath>\r\n"};
        MappViewObjects Objects = new MappViewObjects();
        static readonly List<string> buttonDenominators = new List<string> {"ToggleSwitch", "ToggleButton", "RadioButton", "PushButton", "NavigationButton", "MomentaryPushButton", "HoverButton", "Checkbox", "Button"};
        static readonly List<string> chartDenominators = new List<string> {"BarChart", "DonutChart", "LinearGauge", "LineChart", "OnlineChart", "OnlineChartHDA", "PieChart", "ProfileGenerator", "RadialGauge", "StackedBarChart", "Timeline", "XYChart"};
        static readonly List<string> containerDenominators = new List<string> {"ButtonBar", "FlexBox", "FlexLayoutPanel", "FlyOut", "GridLine", "GroupBox", "InfoBanner", /*"Navigation",*/ "NavigationBar", "RadialButtonBar", "RadioButtonGroup", "TabControl", };
        static readonly List<string> dataDenominators = new List<string> {"AlarmHistory", "AlarmLine", "AlarmList", "AuditList", "FavoriteWatch", "Table", "UserList", "Database"};
        static readonly List<string> dateTimeDenominators = new List<string> {"DateTimeInput", "DateTimeOutput"};
        static readonly List<string> drawingDenominators = new List<string> {"Ellipse", "Line", "Rectangle", "Paper"};
        static readonly List<string> imageDenominators = new List<string> {"Image", "ImageList"};
        static readonly List<string> loginDenominators = new List<string> {"Login", "LoginButton", "LoginInfo", "LogoutButton", "Password"};
        static readonly List<string> mediaDenominators = new List<string> {"PDFViewer", "QRViewer", "VideoPlayer", "VNCViewer", "WebViewer"};
        static readonly List<string> motionDenominators = new List<string> {"MotionPad"};
        static readonly List<string> numericDenominators = new List<string> {"BasicSlider", "Joystick", "NumericInput", "NumericOutput", "ProgressBar", "RadialSlider", "RangeSlider", "XYJoystick"};
        static readonly List<string> selectorDenominators = new List<string> {"DropDownBox", "ListBox", "TextPicker"};
        static readonly List<string> systemDenominators = new List<string> {"KeyBoard", "LanguageSelector", "MeasurementSystemSelector", "MotionKeyPad", "NumPad", "SystemNavButton", "SystemLogin", "TextKeyPad", "DateTimePicker", "ContentControl", "ContentCarousel"};
        static readonly List<string> textDenominators = new List<string> {"Label", "TextInput", "TextOutput", "TextPad"};
        static readonly List<string> processDenominators = new List<string> {"Sequencer", "LadderEditor", "Skyline"};
        static readonly bool [] toTestWidgetGroups = new bool[] {true, false, false, false, true, false, false, false, false, false, true, false, false, false, false};
        static readonly List<List<string>> AllWidgets = new List<List<string>> {buttonDenominators, chartDenominators, containerDenominators, dataDenominators, dateTimeDenominators, drawingDenominators, imageDenominators, loginDenominators, mediaDenominators, motionDenominators, numericDenominators, selectorDenominators, systemDenominators, textDenominators, processDenominators};
        readonly List<string> TestWidgets = new List<string>();
        static int width, height;
        public override void InitComponent() {
            editorPathMV = Util.Environment.InstallationPath + "\\AS\\TechnologyPackages\\mappView\\" + Version + "\\Editors\\";
            editorPathTS = Util.Environment.InstallationPath + "\\AS\\TechnologyPackages\\TextSystem\\n.d\\Editors\\";
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
            ReadConfiguration();
            Objects.Pages = new List<MappViewPage>();
            foreach (var WidgetGroup in AllWidgets) {
                if (!toTestWidgetGroups[AllWidgets.IndexOf(WidgetGroup)])
                    continue;
                foreach(var item in inputWidgetStrings)
                    if (WidgetGroup.Contains(item[0]))
                        TestWidgets.Add(item[0]);
            }

            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Checking/setting mapp View version to " + Version);
            }
/*              TreeConfig.IdeMain.SelectComponentVersion("mapp View", Version);
            if (!TreeConfig.IdeMain.GetLogicalViewRoot(Project).FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Any(cf => cf.Name.IndexOf("mappView") >= 0))
                 TM611_4_InsertComponent();
            TreeConfig.IdeMain.Build();
            TM611_3_2_ConfigureMappViewServer();
            TreeConfig.IdeMain.Build();
            TM611_4_1_RenameVIS();
            TreeConfig.IdeMain.Build();
            TM611_11_Localization();
            TreeConfig.IdeMain.Build();
*/            TM611_5_Layout();
            TreeConfig.IdeMain.Build();
            InsertWidgets();
            TreeConfig.IdeMain.Build();
            TM611_6_Navigation();
            TM611_8_Binding();
        }
        public override void TM611_4_InsertComponent() {
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, null, null, out var e);
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
            width = 800;
            height = 600;
        }
        void TM611_3_2_ConfigureMappViewServer() {
            string mvconfig = "Config.mappviewcfg";
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView"}, new List<string> { "_Configuration", "_Configuration" }, out var e);
            if (TreeConfig.IdeMain.GetActiveConfigurtion().FindAllDescendants(cf => cf.ByControlType(ControlType.TreeItem)).First(cf => cf.Name.IndexOf("mappView") >= 0).FindAllChildren(cf => cf.ByName("BR_" + mvconfig)).Count() == 0) {
                if (Verbose >= Util.Environment.Verbose.STEPS) {
                    Console.WriteLine("==========================================");
                    Console.WriteLine("Inserting new mapp View configuration");
                }
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView"}, new List<string> { "_Configuration", "_Configuration" }, out e);
                TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "mapp View", "mapp View Configuration");
            }
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Opening new mapp View configuration in workspace");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", "BR_" + mvconfig }, new List<string> { "_Configuration", "_Configuration", "_Configuration" }, out var editor);
            AutomationElement ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(editor, "BR_MappViewConfiguration");
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Selecting HTTP as communication protocol");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathMV + "mappviewcfg.xml", "Protocol"), new List<string> { "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 0); //Select "HTTP"
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Selecting anonymous token as Startup User");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathMV + "mappviewcfg.xml", "Startup User"), new List<string> { "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 0); //Select "anonymous token"
            TreeConfig.ClickAutomationElement(IDE_Main.MainWindow.TitleBar);
            editor.Close();
        }
        void TM611_4_1_RenameVIS() {
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Renaming Visu");
            }
            string visname = "vis_0.vis";
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", "BR_" + visname}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" }, out var editor);
            AutomationElement adocText = editor.ConfigWorkspace.FindAllDescendants().First(cf => cf.Name.IndexOf("<?xml") >= 0);
            TreeConfig.IdeMain.RemoveTrailingWhitespaceFromXML(adocText);
            visname = "Test_Visu";
            Rectangle rec = TreeConfig.IdeMain.FindWordinCapture(adocText, "\"vis_0\"");
            Mouse.MoveTo(new Point {X = adocText.BoundingRectangle.X + rec.X + rec.Width/2, Y = adocText.BoundingRectangle.Y + rec.Y});
            Mouse.DoubleClick();
            Keyboard.Type(visname);
            TreeConfig.IdeMain.SaveAll();
            editor = editor.Rename(visname + ".vis");
            editor.Close();
        }
        void TM611_11_Localization() {
            string tmxconfig = "LocalizableTexts.tmx";
              if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting new Project Language container");
            } 
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView"}, new List<string> { "_Object Name" }, out var e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Project Languages");
             TreeConfig.IdeMain.SaveAll();
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting new Localizable Texts container and changing namespace to IAT");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Resources", "BR_Texts"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Localizable Texts");
            TreeConfig.IdeMain.SaveAll();
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Resources", "BR_Texts", "BR_" + tmxconfig}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out var editor);
            AutomationElement editNamespace = editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByAutomationId("textNamespace")).AsTextBox();
            editNamespace.Patterns.Value.Pattern.SetValue("IAT");
            TreeConfig.IdeMain.SaveAll();
            AutomationElement textTree = editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByAutomationId("B&R TreeView Control")).AsTree();
            AutomationElement newItem;
             foreach (string s in TestWidgets) {
                string[] item = inputWidgetStrings.Find(x => x.Contains(s));
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
                Mouse.Click(IDE_Main.Workspace.BoundingRectangle.Center());
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
            }
            TreeConfig.IdeMain.SaveAll();
            editor.Close();
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting and editing Textsystem Config File");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_TextSystem"}, new List<string> { "_Configuration", "_Configuration" }, out e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "Textsystem Configuration");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_TextSystem", "BR_TC.textconfig"}, new List<string> { "_Configuration", "_Configuration", "_Configuration" }, out editor);
            AutomationElement ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(editor, "BR_TextConfig");
            Mouse.Click(IDE_Main.Workspace.BoundingRectangle.Center());

            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "System language"), new List<string> { "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "en");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Fallback language"), new List<string> { "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "de");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Target languages", "BR_Target language 1"), new List<string> { "_Name", "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "en");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Target languages", "BR_Target language 2"), new List<string> { "_Name", "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "de");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Target languages", "BR_Target language 3"), new List<string> { "_Name", "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "fr");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Tmx files for target", "BR_Tmx file 1"), new List<string> { "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 0);
            TreeConfig.IdeMain.SaveAll();
            editor.Close();
        }
        void TM611_5_Layout() {
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting navigation and info content");
            }
            //TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_content_0.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out content0_editor);
            //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            Point workspaceCenter = IDE_Main.Workspace.BoundingRectangle.Center();
            
/*            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out var e);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Page content");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents", "BR_content_1.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out navcontent_editor);
            AutomationElement properties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement name = properties.FindFirstDescendant(cf => cf.ByName("Name"));
            Mouse.Click(new Point {X = name.BoundingRectangle.Right - 20, Y = name.BoundingRectangle.Top + name.BoundingRectangle.Height/2});
            Mouse.DoubleClick(new Point {X = name.BoundingRectangle.Right - 20, Y = name.BoundingRectangle.Top + name.BoundingRectangle.Height/2});
            Keyboard.Type("Navigation");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            navcontent_editor = navcontent_editor.Rename("Navigation.content");
            EditSize(width:100, height:500, content:true);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", "Navigation", true, workspaceCenter);
            EditPosition(top:0, left:0);
            EditSize(width:100, height:500);
            TreeConfig.IdeMain.SaveAll(); 

            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out e);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Page content");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents", "BR_content_1.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out var ip_editor);
            properties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            name = properties.FindFirstDescendant(cf => cf.ByName("Name"));
            Mouse.Click(new Point {X = name.BoundingRectangle.Right - 20, Y = name.BoundingRectangle.Top + name.BoundingRectangle.Height/2});
            Mouse.DoubleClick(new Point {X = name.BoundingRectangle.Right - 20, Y = name.BoundingRectangle.Top + name.BoundingRectangle.Height/2});
            Keyboard.Type("Info_Pane");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            ip_editor = ip_editor.Rename("Info_Pane.content");
            EditSize(height:100, content:true);
            TreeConfig.IdeMain.SaveAll();
            Mouse.Click(workspaceCenter);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", "Label", drag:true, toDrag:workspaceCenter);
            EditSize(width:200, height:30);
            EditPosition(left:50, top:5); */
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", "LanguageSelector", drag:true, toDrag:workspaceCenter);
            EditPosition(left:680, top:35); //geht net!
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Preparing Layout for all Pages");
            }
            content0_editor.Restore();
            //TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_content_0.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" });
            EditSize(width:700, height:500, content:true);
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Layouts", "BR_layout_0.layout"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out var layout0_editor);
            Mouse.Click(workspaceCenter);
            EditSize(width:700, height:500, area:true);
            EditPosition(left:100, top:100, area:true);
            FlaUI.Core.AutomationElements.Button createArea = layout0_editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Create Area"))).AsButton();
            createArea.Click();
            EditSize(width:100, height:500, area:true);
            EditPosition(left:0, top:100, area:true);
            createArea.Click();
            EditSize(width:800, height:100, area:true);
            EditPosition(left:0, top:0, area:true);
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_page_0.page"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out var page0_editor);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(1500));
            Mouse.Click(workspaceCenter);
            AutomationElement editor = page0_editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document).And(cf.ByName("Page-Editor")));
            TreeConfig.IdeMain.SetIWorkspaceMinSize(editor, percent:true);
            Mouse.MoveTo(new Point {X = editor.BoundingRectangle.Left + (int)(editor.BoundingRectangle.Width * 50/800), Y = editor.BoundingRectangle.Top + (int)(editor.BoundingRectangle.Height * 300/600)});
            Mouse.Click();
            SelectFromMappViewDropDown(new string [] {"Common", "refId"}, "Navigation");
            Mouse.MoveTo(new Point {X = editor.BoundingRectangle.Left + (int)(editor.BoundingRectangle.Width * 400/800), Y = editor.BoundingRectangle.Top + (int)(editor.BoundingRectangle.Height * 50/600)});
            Mouse.Click();
            SelectFromMappViewDropDown(new string [] {"Common", "refId"}, "Info_Pane");
            TreeConfig.IdeMain.SaveAll();
            page0_editor.Close();
            layout0_editor.Close();
            //ip_editor.Close();
        }
        void InsertWidgets() {
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting widgets");
            }
            content0_editor = IDE_Main.Editors.Find(x => x.Name.Contains("content_0.content"));
            content0_editor.Restore();
            Point editorCenter = IDE_Main.Workspace.BoundingRectangle.Center();
            Mouse.Click(editorCenter);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            AutomationElement content_0Properties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement docIATeditor = content0_editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document).And(cf.ByName("IAT-Editor")));
            AutomationElement defaultLabel = docIATeditor.FindFirstDescendant(cf => cf.ByAutomationId("content_0_Label1"));
            TreeConfig.ClickAutomationElement(defaultLabel);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.DELETE); 
            TreeConfig.IdeMain.SaveAll(); 
            TreeConfig.IdeMain.SetIWorkspaceMinSize(docIATeditor);
            int pageID = 0;
            string pageName, contentName;
            foreach(string text in TestWidgets) {
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out var e);
                IDE_Main.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nCopy ")).AsButton().Click();
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages"}, new List<string> { "_Object Name", "_Object Name", "_Object Name" }, out e);
                IDE_Main.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nPaste ")).AsButton().Click();
                pageID++;
                pageName = "page_" + pageID;
                contentName = "content_" + pageID;
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_" + pageName, "BR_" + contentName + ".content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out e);
                TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", text, drag:true, toDrag:editorCenter);
                EditSize(width:500, height:400);
                EditPosition(left:100, top:50);
                EditText(text);
                List<string[]> ls = new List<string[]> { new string[] { contentName, text } };
                MappViewPage p = new MappViewPage(pageName, ls);
                //IntlTextBinding(p);
                Objects.Pages.Add(p);
                e.Close();
            }
        }   
        void TM611_6_Navigation() {
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Creating navigation file");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView"}, new List<string> { "_Configuration", "_Configuration" }, out var e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "Navigation");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", "BR_navigation_0.nav"}, new List<string> { "_Configuration", "_Configuration", "_Configuration" }, out var nav_editor);
            //AutomationElement ConfigWorkspaceWindow = IDE_Main.Workspace.FindAllChildren(cf => cf.ByControlType(ControlType.Window)).FirstOrDefault(cf => cf.Name.IndexOf("navigation_0.nav [XML File]") >= 0);
            AutomationElement editor = nav_editor.ConfigWorkspace.FindAllDescendants().FirstOrDefault(cf => cf.Name.Contains("<?xml version")).AsTextBox();
            TreeConfig.IdeMain.RemoveTrailingWhitespaceFromXML(editor);
            Keyboard.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            IDE_Main.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nCopy ")).AsButton().Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            string copiedText = Clipboard.GetText();
            while (copiedText.ElementAt(0) != '<') copiedText = copiedText.Substring(1);
            int firstIndex = copiedText.IndexOf("    <NavigationPath ");
            int secondIndex = copiedText.IndexOf("  </NavigationPaths>");
            string outText = copiedText.Substring(0, firstIndex);
            int pageID = 0;
            string pageName, page0Name = "page_0";
            for(int i=0; i<inputWidgetStrings.Count+1; i++) {
                outText += _navStrings[0];
                pageName = "page_" + pageID;
                outText += pageName;
                outText += _navStrings[1] + _navStrings[2];
                outText += page0Name;
                outText += _navStrings[3] + _navStrings[4];
                pageName = "page_" + (pageID==0?0:(pageID-1));
                outText += pageName;
                outText += _navStrings[5];
                if (i != inputWidgetStrings.Count) {
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
            IDE_Main.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nPaste ")).AsButton().Click();
            TreeConfig.IdeMain.SaveAll();
            nav_editor.Close();
             if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Connecting it to Navigation Widget");
            }
            navcontent_editor = IDE_Main.Editors.Find(x => x.Name.Contains("Navigation.content"));
            navcontent_editor.Restore();
            Mouse.Click(navcontent_editor.ConfigWorkspace.BoundingRectangle.Center());
            SelectFromMappViewDropDown(new string [] {"Data", "navRefId"}, "navigation_0");
        }
        void TM611_8_Binding() {
            //navcontent_editor.Close();
            IDE_Main.Editor e;
             if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Inserting OPC UA/CS default view");
            }
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_Connectivity", "BR_OpcUaCs"}, new List<string> { "_Configuration", "_Configuration", "_Configuration" }, out e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "DefaultView");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_Connectivity", "BR_OpcUaCs", "BR_OpcUaCsMap.uad"}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" }, out var editor);
            AutomationElement ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(editor, "BR_<Default>");
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Generating Variables");
            }
            TreeConfig.IdeMain.GenerateProgram("Visualization", ST:true, AllInOne:true);
             if (toTestWidgetGroups[0])
                TreeConfig.IdeMain.GenerateVariables(Objects.ButtonValues, out Objects.ButtonValuesStrings, "Visualization");
            if (toTestWidgetGroups[4])
                TreeConfig.IdeMain.GenerateVariables(Objects.DateTimeValues, out Objects.DateTimeValuesStrings, "Visualization");
            if (toTestWidgetGroups[10]) {
                TreeConfig.IdeMain.GenerateVariables(Objects.NumericValues, out Objects.NumericValuesStrings, "Visualization");
                for (int i = 0; i < Objects.Numeric2DValues.Count(); i++)
                    Objects.Numeric2DValues[i] = new float[2];
                TreeConfig.IdeMain.GenerateVariables(Objects.Numeric2DValues, out Objects.Numeric2DValuesStrings, "Visualization");
            }
          TreeConfig.IdeMain.Build();
            if (Verbose >= Util.Environment.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Activating Variables in Default View");
            }
            editor.Restore();
            ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(editor, "BR_<Default>");
            AutomationElement visuRoot = ConfigRoot.FindFirstDescendant(cf => cf.ByName("BR_Visualizat"));
            Button enableTag = editor.ConfigWorkspace.FindFirstChild(cf => cf.ByName("OPC UA Default View")).FindFirstChild(cf => cf.ByName("Enable Tag")).AsButton();
            AutomationElement [] vars = visuRoot.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem));
            foreach(var v in vars) {
                AutomationElement aname = v.FindAllChildren().First(cf => cf.Name.IndexOf("_Name") > 0);
                TreeConfig.ClickAutomationElement(aname);
                enableTag.Click();
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
            }
            TreeConfig.IdeMain.Build();
            editor.Close();
            foreach(var w1 in TestWidgets) {
                MappViewPage p = null;
                string c ="";
                foreach (var page in Objects.Pages)
                    foreach (var w2 in page.Widgets)
                        if (w2[1] == w1) {
                            p = page;
                            c = w2[0];
                        }
                e = OpenTextEditor(p, c);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                Keyboard.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                IDE_Main.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nCopy ")).AsButton().Click();
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
                string copiedText = Clipboard.GetText();
                XDocument doc = XDocument.Parse(copiedText);
                XElement content = doc.Root;
                int _top=0, _left=0, _width=0, _height=0;
                foreach (XElement widgetElement in content.Elements("Widgets")) {
                    XAttribute idAttr = widgetElement.Attribute("id");
                    if (idAttr != null && idAttr.Value == w1 + "1") {
                        _top = int.Parse(widgetElement.Attribute("top").Value);
                        _left = int.Parse(widgetElement.Attribute("left").Value);
                        _width = int.Parse(widgetElement.Attribute("width").Value);
                        _height = int.Parse(widgetElement.Attribute("height").Value);
                    }
                }
                e = OpenEditor(p, c);
                TreeConfig.IdeMain.SetIWorkspaceMinSize(IDE_Main.Workspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document).And(cf.ByName("Page-Editor"))));
                Mouse.MoveTo(new Point {X = IDE_Main.Workspace.BoundingRectangle.Left + (int)(IDE_Main.Workspace.BoundingRectangle.Width * (_left+_width/2)/width), Y = IDE_Main.Workspace.BoundingRectangle.Top + (int)(IDE_Main.Workspace.BoundingRectangle.Height * (_top+_height/2)/height)});
                Mouse.Click();
                int indexWidgetgroup = 0, indexWidget = 0;
                foreach (var WidgetGroup in AllWidgets) {
                    if (!toTestWidgetGroups[AllWidgets.IndexOf(WidgetGroup)])
                        continue;
                    if (WidgetGroup.Contains(w1)) {
                        indexWidgetgroup = AllWidgets.IndexOf(WidgetGroup);
                        indexWidget = WidgetGroup.IndexOf(w1);
                    }
                }
                Object o;
                string str = null;
                switch (indexWidgetgroup) {
                    case 0: o = Objects.ButtonValues[indexWidget]; str = Objects.ButtonValuesStrings[indexWidget][0]; break;
                    case 4: o = Objects.DateTimeValues[indexWidget]; str = Objects.DateTimeValuesStrings[indexWidget][0]; break;
                    case 10:
                        if (indexWidget<Objects.NumericValues.Count())
                            o = Objects.NumericValues[indexWidget];
                        else
                            o = Objects.Numeric2DValues[indexWidget-Objects.NumericValues.Count()]; 
                        str = indexWidget<Objects.NumericValues.Count()?Objects.NumericValuesStrings[indexWidget][0]:Objects.Numeric2DValuesStrings[indexWidget-Objects.NumericValues.Count()][0]; break;
                }
                EditValue(str);
            }

        }
        IDE_Main.Editor OpenEditor(MappViewPage page, string content) {
            CloseTextEditor(page, content);
            IDE_Main.Editor e = null;
            foreach (var v in page.Widgets) {
                if (content != v[0])
                    continue;
                string s = v[0] + ".content";
                e = IDE_Main.Editors.Find(x => x.Name.Contains(s));
                if (e.Name == String.Empty)
                    TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_" + page.Name, "BR_" + v[0] + ".content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out e);
                else
                    e.Restore();
                Mouse.Click(IDE_Main.Workspace.BoundingRectangle.Center());
            }
            return e;
        }
        IDE_Main.Editor OpenTextEditor(MappViewPage page, string content) {
            CloseEditor(page, content);
            IDE_Main.Editor e = null;
            foreach (var v in page.Widgets) {
                if (content != v[0])
                    continue;
                string s = page.Name + "::" + v[0] + ".content";
                e = IDE_Main.Editors.Find(x => x.Name.Contains(s));
                if (e.Name == String.Empty) {
                    TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_" + page.Name}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out e);
                    Mouse.RightClick(IDE_Main.Workspace.FindFirstDescendant(cf => cf.ByName("BR_" + content + ".content" + "_Object Name")).BoundingRectangle.Center());
                    TreeConfig.ClickContextMenuItem(IDE_Main.MainWindow, "Open", "Open As Text");
                }
                else
                    e.Restore();
                Mouse.Click(IDE_Main.Workspace.BoundingRectangle.Center());
            }
            return e;
        }
        void CloseTextEditor(MappViewPage page, string content) {
            IDE_Main.Editor e = null;
            foreach (var v in page.Widgets) {
                if (content != v[0])
                    continue;
                string s = page.Name + "::" + v[0] + ".content";
                e = IDE_Main.Editors.Find(x => x.Name.Contains(s));
                if (e.Name != String.Empty)
                    e.Close();
            }
        }
        void CloseEditor(MappViewPage page, string content) {
            IDE_Main.Editor e = null;
            foreach (var v in page.Widgets) {
                if (content != v[0])
                    continue;
                string s = v[0] + ".content";
                e = IDE_Main.Editors.Find(x => x.Name.Contains(s));
                if (e.Name != String.Empty)
                    e.Close();
            }
        }
        void SelectFromMappViewDropDown(string [] stree, string select) {
            if (Verbose >= Util.Environment.Verbose.FULL)
                Console.WriteLine("Selecting " + select + " from Dropdown: " + stree[0] + ", " + stree[1]);
            AutomationElement properties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            Mouse.Position = properties.BoundingRectangle.Center();
            Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
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
            Mouse.MoveTo(new Point {X = atree.ElementAt(1).BoundingRectangle.Right - 15, Y = atree.ElementAt(1).BoundingRectangle.Top + atree.ElementAt(1).BoundingRectangle.Height/2});
            Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
            Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Rectangle rec = TreeConfig.IdeMain.FindWordinCapture(properties, select);
            Point point = new Point {X = properties.BoundingRectangle.Left + rec.Left + rec.Width/2, Y = properties.BoundingRectangle.Top + rec.Top + rec.Height/2};
            Mouse.Click(point);
        }
        void EditSize(int width = -1, int height = -1, bool content = false, bool area = false) {
            AutomationElement aproperties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            Mouse.Position = aproperties.BoundingRectangle.Center();
            Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            AutomationElement afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
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
                    AutomationElement alayout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
                    AutomationElement asize = alayout.FindFirstChild(cf => cf.ByName("Size"));
                    while (!aproperties.BoundingRectangle.IntersectsWith(afirst.BoundingRectangle)) {
                        Mouse.Scroll(1d);
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        afirst = aproperties.FindFirstChild();
                    }
                    while (!aproperties.BoundingRectangle.IntersectsWith(asize.BoundingRectangle)) {
                        Mouse.Scroll(-1d);
                        System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                        alayout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
                        asize = alayout.FindFirstChild(cf => cf.ByName("Size"));
                    }
                    Mouse.Scroll(-2d);
                    Mouse.Click(new Point {X = asize.BoundingRectangle.Left + 5, Y = asize.BoundingRectangle.Top + 5});
                    Mouse.Scroll(-2d);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    AutomationElement awidth = asize.FindFirstChild(cf => cf.ByName("width"));
                    if (width != -1 && int.Parse(awidth.Patterns.Value.Pattern.Value) != width) {
                        Mouse.DoubleClick(new Point {X = awidth.BoundingRectangle.Right - 20, Y = awidth.BoundingRectangle.Top + awidth.BoundingRectangle.Height/2});
                        Keyboard.Type("" + width);
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                    }
                    AutomationElement aheight = asize.FindFirstChild(cf => cf.ByName("height"));
                    if (height != -1 && int.Parse(aheight.Patterns.Value.Pattern.Value) != height) {
                        Mouse.DoubleClick(new Point {X = aheight.BoundingRectangle.Right - 20, Y = aheight.BoundingRectangle.Top + aheight.BoundingRectangle.Height/2});
                        Keyboard.Type("" + height);
                        Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
                    }
                }
            }
            TreeConfig.IdeMain.SaveAll();
        }
        void EditPosition(int top = -1, int left = -1, bool area = false) {
            AutomationElement aproperties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            Mouse.Position = aproperties.BoundingRectangle.Center();
            Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            AutomationElement afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
            AutomationElement alayout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
            AutomationElement aposition = null;
            AutomationElement atop;
            while (!aproperties.BoundingRectangle.IntersectsWith(afirst.BoundingRectangle)) {
                Mouse.Scroll(1d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
            }
            if (area) {
                while (!aproperties.BoundingRectangle.IntersectsWith(alayout.BoundingRectangle)) {
                    Mouse.Scroll(-1d);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    alayout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
                }
                Mouse.Scroll(-2d);
            }
            else {
                aposition = alayout.FindFirstChild(cf => cf.ByName("Position"));
                while (aposition == null || !aproperties.BoundingRectangle.IntersectsWith(aposition.BoundingRectangle)) {
                    Mouse.Scroll(-1d);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    alayout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
                    aposition = alayout.FindFirstChild(cf => cf.ByName("Position"));
                }
                Mouse.Scroll(-2d);
                atop = aposition.FindFirstChild(cf => cf.ByName("top"));
                if (!aproperties.BoundingRectangle.IntersectsWith(atop.BoundingRectangle))
                    Mouse.Click(new Point {X = aposition.BoundingRectangle.Left + 5, Y = aposition.BoundingRectangle.Top + 5});
                Mouse.Scroll(-2d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
            atop = (area?alayout:aposition).FindFirstChild(cf => cf.ByName("top"));
            AutomationElement aleft = (area?alayout:aposition).FindFirstChild(cf => cf.ByName("left"));
            if (top != -1 && int.Parse(atop.Patterns.Value.Pattern.Value) != top) {
                Mouse.DoubleClick(new Point {X = atop.BoundingRectangle.Right - 20, Y = atop.BoundingRectangle.Top + atop.BoundingRectangle.Height/2});
                Keyboard.Type("" + top);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            }
            if (left != -1 && int.Parse(aleft.Patterns.Value.Pattern.Value) != left) {
                Mouse.DoubleClick(new Point {X = aleft.BoundingRectangle.Right - 20, Y = aleft.BoundingRectangle.Top + aleft.BoundingRectangle.Height/2});
                Keyboard.Type("" + left);
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            }
            TreeConfig.IdeMain.SaveAll();
        }
        void EditValue(string variablestring) {
            AutomationElement aproperties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            Mouse.Position = aproperties.BoundingRectangle.Center();
            Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            AutomationElement afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
            AutomationElement alast;
            AutomationElement adata = aproperties.FindFirstChild(cf => cf.ByName("Data"));
            AutomationElement avalue;
            while (!aproperties.BoundingRectangle.IntersectsWith(afirst.BoundingRectangle)) {
                Mouse.Scroll(1d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
            }
            avalue = adata.FindFirstChild(cf => cf.ByName("Value"));
            while (avalue == null || !aproperties.BoundingRectangle.IntersectsWith(avalue.BoundingRectangle)) {
                Mouse.Scroll(-1d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                adata = aproperties.FindFirstChild(cf => cf.ByName("Data"));
                avalue = adata.FindFirstChild(cf => cf.ByName("Value"));
                alast = aproperties.FindAllChildren(cf => cf.ByControlType(ControlType.DataItem)).Last();
                if (avalue == null && alast.BoundingRectangle.Left != 0)
                    return;
            }
            Mouse.Scroll(-2d);
            Mouse.Click(new Point {X = avalue.BoundingRectangle.Left + 5, Y = avalue.BoundingRectangle.Top + 5});
            Mouse.Scroll(-2d);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            AutomationElement abinding = avalue.FindFirstChild(cf => cf.ByName("Binding"));
            Mouse.DoubleClick(new Point {X = abinding.BoundingRectangle.Right - 20, Y = abinding.BoundingRectangle.Top + abinding.BoundingRectangle.Height/2});
            FlaUI.Core.AutomationElements.Window selectVariableWindow;
            while ((selectVariableWindow = TreeConfig.IdeMain.GetModalWindow("Select Variable")) == null)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.BindingWindow, new List<string> { "BR_Visualizat", "BR_" + variablestring, "BR_value"}, new List<string> {  "_Address Space", "_Address Space", "_Address Space" }, out var e, selectVariableWindow);
            TreeConfig.IdeMain.SaveAll();
        }
        void EditText(string text) {
            AutomationElement aproperties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            Mouse.Position = aproperties.BoundingRectangle.Center();
            Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            AutomationElement afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
            AutomationElement alast;
            AutomationElement appearance = aproperties.FindFirstChild(cf => cf.ByName("Appearance"));
            AutomationElement atext;
            while (!aproperties.BoundingRectangle.IntersectsWith(afirst.BoundingRectangle)) {
                Mouse.Scroll(1d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
            }
            atext = appearance.FindFirstChild(cf => cf.ByName("Text"));
            while (atext == null || !aproperties.BoundingRectangle.IntersectsWith(atext.BoundingRectangle)) {
                Mouse.Scroll(-1d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                appearance = aproperties.FindFirstChild(cf => cf.ByName("Appearance"));
                atext = appearance.FindFirstChild(cf => cf.ByName("Text"));
                alast = aproperties.FindAllChildren(cf => cf.ByControlType(ControlType.DataItem)).Last();
                if (atext == null && alast.BoundingRectangle.Left != 0)
                    return;
            }
            Mouse.Scroll(-2d);
            Mouse.Click(new Point {X = atext.BoundingRectangle.Left + 5, Y = atext.BoundingRectangle.Top + 5});
            Mouse.Scroll(-2d);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            AutomationElement adefault = atext.FindFirstChild(cf => cf.ByName("Default"));
            Mouse.DoubleClick(new Point {X = adefault.BoundingRectangle.Right - 20, Y = adefault.BoundingRectangle.Top + adefault.BoundingRectangle.Height/2});
            Keyboard.Type("$IAT/" + text);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            TreeConfig.IdeMain.SaveAll();
        }
        public static void SomethingToRunInThread(Object o) {
            System.Windows.Clipboard.SetText((string) o);
        }
        protected void Copy_to_clipboard(string text) {
            Thread th = new Thread(SomethingToRunInThread);
            th.SetApartmentState(ApartmentState.STA);
            th.IsBackground = false;
            th.Start(text);
            th.Join();
        }
        void ReadConfiguration() {
            string file = System.Environment.CurrentDirectory + "\\FlaUITests\\config\\Widgets.txt";
            if (!System.IO.File.Exists(file))
                Console.WriteLine($"Warning: file not found at path: {file}");
            try {
                var lines = File.ReadLines(file);
                foreach (var line in lines) {
                    inputWidgetStrings.Add(new string[] {line, "fr_" + line, "de_" + line, "en_" + line});
                }
            } catch (Exception ex) { Console.WriteLine($"Error reading {file}: {ex.Message}"); }

        }
    }
}