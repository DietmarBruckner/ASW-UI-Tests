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
using System.IO;
using System.Xml.Linq;

namespace FlaUITests.Util {
    public partial class MappView {
        string editorPathMV;
        string editorPathTS;
        IDE_Main.Editor content0_editor, navcontent_editor;
        readonly List<string[]> inputWidgetStrings = new List<string[]>();
        readonly MappViewObjects Objects = new MappViewObjects();
        readonly List<string> TestWidgets = new List<string>();
        static int width, height;
        public override void InitComponent() {
            editorPathMV = Util.Environment.InstallationPath + "\\AS\\TechnologyPackages\\mappView\\" + Version + "\\Editors\\";
            editorPathTS = Util.Environment.InstallationPath + "\\AS\\TechnologyPackages\\TextSystem\\n.d\\Editors\\";
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
            ReadConfiguration();
            Objects.Pages = new List<MappViewPage>();
            foreach (var WidgetGroup in MappViewObjects.AllWidgets) {
                if (!MappViewObjects.toTestWidgetGroups[MappViewObjects.AllWidgets.IndexOf(WidgetGroup)])
                    continue;
                foreach(var item in inputWidgetStrings)
                    if (WidgetGroup.Contains(item[0]))
                        TestWidgets.Add(item[0]);
            }
            Util.ConsoleOut(Util.Verbose.STEPS, "Checking/setting mapp View version to " + Version);
             TreeConfig.IdeMain.SelectComponentVersion("mapp View", Version);
            if (!TreeConfig.IdeMain.GetLogicalViewRoot(Project).FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem)).Any(cf => cf.Name.IndexOf("mappView") >= 0))
                InsertComponent();
            TreeConfig.IdeMain.Build();
            TM611_3_2_ConfigureMappViewServer();
            TreeConfig.IdeMain.Build();
            TM611_4_1_RenameVIS();
            TreeConfig.IdeMain.Build();
            TM611_11_Localization();
            TreeConfig.IdeMain.Build();
            TM611_5_Layout();
            TreeConfig.IdeMain.Build();
            InsertWidgets();
            TreeConfig.IdeMain.Build();
            TM611_6_Navigation();
            TreeConfig.IdeMain.Build();
            //CreatePageContentsShortcut();
            TM611_8_Binding();
        }
        void CreatePageContentsShortcut() {
            int pageID = 0;
            string pageName, contentName;
            foreach(string text in TestWidgets) {
                pageID++;
                pageName = "page_" + pageID;
                contentName = "content_" + pageID;
                List<string[]> ls = new List<string[]> { new string[] { contentName, text } };
                MappViewPage p = new MappViewPage(pageName, ls);
                Objects.Pages.Add(p);
            }
            Objects.ButtonValuesStrings = new string[MappViewObjects.buttonDenominators.Count][];
            for (int i = 0; i < MappViewObjects.buttonDenominators.Count; i++)
                Objects.ButtonValuesStrings[i] = new string[] { "System_Boolean_" + i, "BOOL" };
            Objects.DateTimeValuesStrings = new string[MappViewObjects.dateTimeDenominators.Count][];
            for (int i = 0; i < MappViewObjects.dateTimeDenominators.Count; i++)
                Objects.DateTimeValuesStrings[i] = new string[] { "System_DateTime_" + i, "DT" };
            Objects.NumericValuesStrings = new string[MappViewObjects.numericDenominators.Count][];
            for (int i = 0; i < MappViewObjects.numericDenominators.Count - 2; i++)
                Objects.NumericValuesStrings[i] = new string[] { "System_Single_" + i, "REAL" };
            Objects.Numeric2DValuesStrings = new string[2][];
            for (int i = 0; i < 2; i++)
                Objects.Numeric2DValuesStrings[i] = new string[] { "System_Single_a" + i, "REAL[0..2]" };
            width = 800;
            height = 600;        }
        public override void InsertComponent() {
            TM611_4_InsertComponent();
        }
        void TM611_4_InsertComponent() {
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, null, null, out var e);
            Util.ConsoleOut(Util.Verbose.STEPS, "Adding mapp View object");
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
                Util.ConsoleOut(Util.Verbose.STEPS, "Default template not found in mapp View wizard");
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
                Util.ConsoleOut(Util.Verbose.STEPS, "Inserting new mapp View configuration");
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView"}, new List<string> { "_Configuration", "_Configuration" }, out e);
                TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "mapp View", "mapp View Configuration");
            }
            Util.ConsoleOut(Util.Verbose.STEPS, "Opening new mapp View configuration in workspace");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", "BR_" + mvconfig }, new List<string> { "_Configuration", "_Configuration", "_Configuration" }, out var editor);
            AutomationElement ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(editor, "BR_MappViewConfiguration");
            Util.ConsoleOut(Util.Verbose.STEPS, "Selecting HTTP as communication protocol");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathMV + "mappviewcfg.xml", "Protocol"), new List<string> { "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 0); //Select "HTTP"
            Util.ConsoleOut(Util.Verbose.STEPS, "Selecting anonymous token as Startup User");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathMV + "mappviewcfg.xml", "Startup User"), new List<string> { "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 2); //Select "force login"
            TreeConfig.ClickAutomationElement(IDE_Main.MainWindow.TitleBar);
            editor.Close();
        }
        void TM611_4_1_RenameVIS() {
            Util.ConsoleOut(Util.Verbose.STEPS, "Renaming Visu");
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
            Util.ConsoleOut(Util.Verbose.STEPS, "Inserting new Project Language container");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView"}, new List<string> { "_Object Name" }, out var e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.LogicalView, "", "Project Languages");
            TreeConfig.IdeMain.SaveAll();
            Util.ConsoleOut(Util.Verbose.STEPS, "Inserting new Localizable Texts container and changing namespace to IAT");
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
            Util.ConsoleOut(Util.Verbose.STEPS, "Inserting and editing Textsystem Config File");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_TextSystem"}, new List<string> { "_Configuration", "_Configuration" }, out e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "Textsystem Configuration");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_TextSystem", "BR_TC.textconfig"}, new List<string> { "_Configuration", "_Configuration", "_Configuration" }, out editor);
            AutomationElement ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(editor, "BR_TextConfig");
            Mouse.Click(IDE_Main.Workspace.BoundingRectangle.Center());

            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "System language"), new List<string> { "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "en");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Fallback language"), new List<string> { "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "de");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Target languages", new string [] { "BR_Target language 1" }), new List<string> { "_Name", "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "en");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Target languages", new string [] { "BR_Target language 2" }), new List<string> { "_Name", "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "de");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Target languages", new string [] { "BR_Target language 3" }), new List<string> { "_Name", "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, "fr");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.Workspace, TreeConfig.FindXMLPath(editorPathTS + "TextConfig.xml", "Tmx files for target", new string [] { "BR_Tmx file 1" }), new List<string> { "_Name", "_Value" }, out e, ConfigRoot);
            TreeConfig.ClickComboBoxTreeItem(IDE_Main.MainWindow, 0);
            TreeConfig.IdeMain.SaveAll();
            editor.Close();
        }
        void TM611_5_Layout() {
            Util.ConsoleOut(Util.Verbose.STEPS, "Inserting navigation and info content");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0", "BR_content_0.content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out content0_editor);
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            Point workspaceCenter = IDE_Main.Workspace.BoundingRectangle.Center();
            AutomationElement docIATeditor = content0_editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document).And(cf.ByName("IAT-Editor")));
            AutomationElement defaultLabel = docIATeditor.FindFirstDescendant(cf => cf.ByAutomationId("content_0_Label1"));
            TreeConfig.ClickAutomationElement(defaultLabel);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.DELETE); 
            TreeConfig.IdeMain.SaveAll(); 
            
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_AreaContents"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out var e);
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
            EditPosition(left:50, top:5); 
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", "LanguageSelector", drag:true, toDrag:workspaceCenter);
            EditPosition(left:680, top:35);
            Util.ConsoleOut(Util.Verbose.STEPS, "Preparing Layout for all Pages");
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
            SelectFromMappViewDropDown("Common", "refId", "Navigation");
            Mouse.MoveTo(new Point {X = editor.BoundingRectangle.Left + (int)(editor.BoundingRectangle.Width * 400/800), Y = editor.BoundingRectangle.Top + (int)(editor.BoundingRectangle.Height * 50/600)});
            Mouse.Click();
            SelectFromMappViewDropDown("Common", "refId", "Info_Pane");
            TreeConfig.IdeMain.SaveAll();
            page0_editor.Close();
            layout0_editor.Close();
            ip_editor.Close();
        }
        void InsertWidgets() {
            Util.ConsoleOut(Util.Verbose.STEPS, "Inserting widgets");
            content0_editor = IDE_Main.Editors.Find(x => x.Name.Contains("content_0.content"));
            content0_editor.Restore();
            Point editorCenter = IDE_Main.Workspace.BoundingRectangle.Center();
            Mouse.Click(editorCenter);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
            AutomationElement content_0Properties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement docIATeditor = content0_editor.ConfigWorkspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document).And(cf.ByName("IAT-Editor")));
            TreeConfig.IdeMain.SetIWorkspaceMinSize(docIATeditor);
            int pageID = 0;
            string pageName, contentName;
            foreach(string text in TestWidgets) {
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_page_0"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out var e, shortcut:0, singleclicklast:true);
                IDE_Main.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nCopy ")).AsButton().Click();
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages"}, new List<string> { "_Object Name", "_Object Name", "_Object Name" }, out e, shortcut:0, singleclicklast:true);
                IDE_Main.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nPaste ")).AsButton().Click();
                pageID++;
                pageName = "page_" + pageID;
                contentName = "content_" + pageID;
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_" + pageName, "BR_" + contentName + ".content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out e, shortcut:1);
                TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.Workspace, "", text, drag:true, toDrag:editorCenter);
                EditSize(width:500, height:400);
                EditPosition(left:100, top:50);
                EditText(text);
                List<string[]> ls = new List<string[]> { new string[] { contentName, text } };
                MappViewPage p = new MappViewPage(pageName, ls);
                Objects.Pages.Add(p);
                e.Close();
            }
        }
        void TM611_6_Navigation() {
            string[] _navStrings = new string[] {"    <NavigationPath refId=\"", "\">\r\n", "      <Destination refId=\"", "\" index=\"0\" />\r\n", "      <Destination refId=\"", "\" index=\"1\" />\r\n", "      <Destination refId=\"", "\" index=\"2\" />\r\n", "    </NavigationPath>\r\n"};
            Util.ConsoleOut(Util.Verbose.STEPS, "Creating navigation file");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView"}, new List<string> { "_Configuration", "_Configuration" }, out var e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "Navigation");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_mappView", "BR_navigation_0.nav"}, new List<string> { "_Configuration", "_Configuration", "_Configuration" }, out var nav_editor);
            AutomationElement editor = nav_editor.ConfigWorkspace.FindAllDescendants().FirstOrDefault(cf => cf.Name.Contains("<?xml version")).AsTextBox();
            TreeConfig.IdeMain.RemoveTrailingWhitespaceFromXML(editor);
            string copiedText = GetTextFromEditor();
            while (copiedText.ElementAt(0) != '<') copiedText = copiedText.Substring(1);
            int firstIndex = copiedText.IndexOf("    <NavigationPath ");
            int secondIndex = copiedText.IndexOf("  </NavigationPaths>");
            string outText = copiedText.Substring(0, firstIndex);
            int pageID = 0;
            string pageName, page0Name = "page_0";
            for(int i=0; i<TestWidgets.Count+1; i++) {
                outText += _navStrings[0];
                pageName = "page_" + pageID;
                outText += pageName;
                outText += _navStrings[1] + _navStrings[2];
                outText += page0Name;
                outText += _navStrings[3] + _navStrings[4];
                pageName = "page_" + (pageID==0?0:(pageID-1));
                outText += pageName;
                outText += _navStrings[5];
                if (i != TestWidgets.Count) {
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
            Util.ConsoleOut(Util.Verbose.STEPS, "Connecting it to Navigation Widget");
            navcontent_editor = IDE_Main.Editors.Find(x => x.Name.Contains("Navigation.content"));
            navcontent_editor.Restore();
            Mouse.Click(navcontent_editor.ConfigWorkspace.BoundingRectangle.Center());
            SelectFromMappViewDropDown("Data", "navRefId", "navigation_0");
            navcontent_editor.Close();
        }
        void TM611_8_Binding() {
            IDE_Main.Editor e;
            Util.ConsoleOut(Util.Verbose.STEPS, "Inserting OPC UA/CS default view");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_Connectivity", "BR_OpcUaCs"}, new List<string> { "_Configuration", "_Configuration", "_Configuration" }, out e);
            TreeConfig.IdeMain.InsertObjectFromToolBox(TreeConfig.ViewType.ConfigurationView, "", "DefaultView");
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.ConfigurationView, new List<string> { "BR_" + Project.CPU, "BR_Connectivity", "BR_OpcUaCs", "BR_OpcUaCsMap.uad"}, new List<string> { "_Configuration", "_Configuration", "_Configuration", "_Configuration" }, out var editor);
            AutomationElement ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(editor, "BR_<Default>");
            Util.ConsoleOut(Util.Verbose.STEPS, "Generating Variables");
            TreeConfig.IdeMain.GenerateProgram("Visualization", ST:true, AllInOne:true);
            if (MappViewObjects.toTestWidgetGroups[0])
                TreeConfig.IdeMain.GenerateVariables(Objects.ButtonValues, out Objects.ButtonValuesStrings, "Visualization");
            if (MappViewObjects.toTestWidgetGroups[4])
                TreeConfig.IdeMain.GenerateVariables(Objects.DateTimeValues, out Objects.DateTimeValuesStrings, "Visualization");
            if (MappViewObjects.toTestWidgetGroups[10]) {
                TreeConfig.IdeMain.GenerateVariables(Objects.NumericValues, out Objects.NumericValuesStrings, "Visualization");
                for (int i = 0; i < Objects.Numeric2DValues.Count(); i++)
                    Objects.Numeric2DValues[i] = new float[2];
                TreeConfig.IdeMain.GenerateVariables(Objects.Numeric2DValues, out Objects.Numeric2DValuesStrings, "Visualization");
            }
            TreeConfig.IdeMain.Build();
            Util.ConsoleOut(Util.Verbose.STEPS, "Activating Variables in Default View");
            editor.Restore();
            ConfigRoot = TreeConfig.IdeMain.GetWorkspaceConfigRoot(editor, "BR_<Default>");
            AutomationElement visuRoot = ConfigRoot.FindFirstDescendant(cf => cf.ByName("BR_Visualizat"));
            TreeConfig.ClickConfigTreeItem(TreeConfig.ViewType.Workspace, visuRoot, "_Name");
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.RIGHT);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Button enableTag = editor.ConfigWorkspace.FindFirstChild(cf => cf.ByName("OPC UA Default View")).FindFirstChild(cf => cf.ByName("Enable Tag")).AsButton();
            visuRoot = ConfigRoot.FindFirstDescendant(cf => cf.ByName("BR_Visualizat"));
            AutomationElement [] vars = visuRoot.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem));
            foreach(var v in vars) {
                AutomationElement aname = v.FindAllChildren().First(cf => cf.Name.IndexOf("_Name") > 0);
                TreeConfig.ClickAutomationElement(aname);
                enableTag.Click();
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
            }
            TreeConfig.IdeMain.Build();
            editor.Close();
            //int ind = -1;    
            foreach(var w1 in TestWidgets) {
                //if (ind++ < 8)
                //    continue;
                MappViewPage p = null;
                string c ="";
                foreach (var page in Objects.Pages)
                    foreach (var w2 in page.Widgets)
                        if (w2[1] == w1) {
                            p = page;
                            c = w2[0];
                        }
                e = OpenEditor(p, c, textEditor:true);
                XDocument doc = XDocument.Parse(GetTextFromEditor());
                XElement xContent = doc.Root;
                XElement xWidgets = xContent.Nodes().OfType<XElement>().FirstOrDefault(x => x.Name.LocalName == "Widgets");
                int _top=0, _left=0, _width=0, _height=0;
                foreach (XElement widgetElement in xWidgets.Nodes().OfType<XElement>()) {
                    XAttribute idAttr = widgetElement.Attribute("id");
                    if (idAttr != null && idAttr.Value == w1 + "1") {
                        _top = int.Parse(widgetElement.Attribute("top").Value);
                        _left = int.Parse(widgetElement.Attribute("left").Value);
                        _width = int.Parse(widgetElement.Attribute("width").Value);
                        _height = int.Parse(widgetElement.Attribute("height").Value);
                    }
                }
                e = OpenEditor(p, c, textEditor:false);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
                TreeConfig.IdeMain.SetIWorkspaceMinSize(IDE_Main.Workspace.FindFirstDescendant(cf => cf.ByControlType(ControlType.Document).And(cf.ByName("Page-Editor"))));
                Mouse.MoveTo(new Point {X = IDE_Main.Workspace.BoundingRectangle.Left + (int)(IDE_Main.Workspace.BoundingRectangle.Width * (_left+_width/2)/width), Y = IDE_Main.Workspace.BoundingRectangle.Top + (int)(IDE_Main.Workspace.BoundingRectangle.Height * (_top+_height/2)/height)});
                Mouse.Click();
                int indexWidgetgroup = 0, indexWidget = 0;
                foreach (var WidgetGroup in MappViewObjects.AllWidgets) {
                    if (!MappViewObjects.toTestWidgetGroups[MappViewObjects.AllWidgets.IndexOf(WidgetGroup)])
                        continue;
                    if (WidgetGroup.Contains(w1)) {
                        indexWidgetgroup = MappViewObjects.AllWidgets.IndexOf(WidgetGroup);
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
                doc = null;
                e.Close();
            }
        }
        string GetTextFromEditor() {
            Keyboard.TypeSimultaneously(FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            IDE_Main.ToolBarStandard.FindFirstChild(cf => cf.ByName("BR_\nCopy ")).AsButton().Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            return Clipboard.GetText();
        }
        IDE_Main.Editor OpenEditor(MappViewPage page, string content, bool textEditor = false) {
            CloseEditor(page, content, !textEditor);
            IDE_Main.Editor e = null;
            string s = textEditor ? page.Name + "::" + content + ".content" : content + ".content";
            e = IDE_Main.Editors.Find(x => x.Name == s);
            if (e == null) {
                TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.LogicalView, new List<string> { "BR_mappView", "BR_Visualization", "BR_Pages", "BR_" + page.Name, "BR_" + content + ".content"}, new List<string> { "_Object Name", "_Object Name", "_Object Name", "_Object Name", "_Object Name" }, out e, singleclicklast:textEditor);
                if (textEditor) {
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
                    Mouse.RightClick();
                    TreeConfig.ClickContextMenuItem(IDE_Main.MainWindow, "Open", "Open As Text");
                    e.Rename(s);
                }
            }
            else
                e.Restore();
            Mouse.Click(IDE_Main.Workspace.BoundingRectangle.Center());
            return e;
        }
        void CloseEditor(MappViewPage page, string content, bool textEditor = false) {
            IDE_Main.Editor e = null;
            string s = textEditor ? page.Name + "::" + content + ".content" : content + ".content";
            e = IDE_Main.Editors.Find(x => x.Name == s);
            if (e != null && e.Name != String.Empty)
                e.Close();
        }
        void SelectFromMappViewDropDown(string property, string subproperty, string select) {
            Util.ConsoleOut(Util.Verbose.FULL, "Selecting " + select + " in " + property + "." + subproperty);
            ScrollFindProperty(property, subproperty);
            AutomationElement properties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement aproperty = properties.FindFirstChild(cf => cf.ByName(property));
            AutomationElement asubproperty = aproperty.FindFirstChild(cf => cf.ByName(subproperty));
            Mouse.MoveTo(new Point {X = asubproperty.BoundingRectangle.Right - 15, Y = asubproperty.BoundingRectangle.Top + asubproperty.BoundingRectangle.Height/2});
            Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(200));
            Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            Rectangle rec = TreeConfig.IdeMain.FindWordinCapture(properties, select);
            Point point = new Point {X = properties.BoundingRectangle.Left + rec.Left + rec.Width/2, Y = properties.BoundingRectangle.Top + rec.Top + rec.Height/2};
            Mouse.Click(point);
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(300));
        }
        void EditSize(int width = -1, int height = -1, bool content = false, bool area = false) {
            AutomationElement aproperties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            Mouse.Position = aproperties.BoundingRectangle.Center();
            Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            AutomationElement afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
            if (content || area) {
                AutomationElement aproperty = aproperties.FindFirstChild(cf => cf.ByName(content?"Property":"Layout"));
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
            else {
                ScrollFindProperty("Layout", "Size", true);
                AutomationElement alayout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
                AutomationElement asize = alayout.FindFirstChild(cf => cf.ByName("Size"));
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
            TreeConfig.IdeMain.SaveAll();
        }
        void EditPosition(int top = -1, int left = -1, bool area = false) {
            AutomationElement aproperties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
            AutomationElement alayout, aposition = null, atop;
            if (area) {
                ScrollFindProperty("Layout", opensub:true);
                alayout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
            }
            else {
                ScrollFindProperty("Layout", "Position", true);
                alayout = aproperties.FindFirstChild(cf => cf.ByName("Layout"));
                aposition = alayout.FindFirstChild(cf => cf.ByName("Position"));
                atop = aposition.FindFirstChild(cf => cf.ByName("top"));
                if (atop == null || !aproperties.BoundingRectangle.IntersectsWith(atop.BoundingRectangle))
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
            ScrollFindProperty("Data", "value", true);
            AutomationElement aproperties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement adata = aproperties.FindFirstChild(cf => cf.ByName("Data"));
            if (adata == null)
                return;
            AutomationElement avalue = adata.FindFirstChild(cf => cf.ByName("value"));
            if (avalue == null)
                return;
            AutomationElement abinding = avalue.FindFirstChild(cf => cf.ByName("Binding"));
            Mouse.DoubleClick(new Point {X = abinding.BoundingRectangle.Right - 20, Y = abinding.BoundingRectangle.Top + abinding.BoundingRectangle.Height/2});
            FlaUI.Core.AutomationElements.Window selectVariableWindow;
            while ((selectVariableWindow = TreeConfig.IdeMain.GetModalWindow("Select Variable")) == null)
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            TreeConfig.ActivateTreeLeaf(TreeConfig.ViewType.BindingWindow, new List<string> { "BR_Visualizat", "BR_" + variablestring, "BR_value"}, new List<string> {  "_Address Space", "_Address Space", "_Address Space" }, out var e, selectVariableWindow);
            TreeConfig.IdeMain.SaveAll();
        }
        void EditText(string text) {
            ScrollFindProperty("Appearance", "Text", true);
            AutomationElement aproperties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            AutomationElement appearance = aproperties.FindFirstChild(cf => cf.ByName("Appearance"));
            AutomationElement atext = appearance.FindFirstChild(cf => cf.ByName("Text"));
            AutomationElement adefault = atext.FindFirstChild(cf => cf.ByName("Default"));
            Mouse.DoubleClick(new Point {X = adefault.BoundingRectangle.Right - 20, Y = adefault.BoundingRectangle.Top + adefault.BoundingRectangle.Height/2});
            Keyboard.Type("$IAT/" + text);
            Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.ENTER);
            TreeConfig.IdeMain.SaveAll();
        }
        void ScrollFindProperty(string property, string sub = null, bool opensub = false) {
            AutomationElement aproperties = IDE_Main.PropertyWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Table));
            Mouse.Position = aproperties.BoundingRectangle.Center();
            Mouse.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            AutomationElement afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
            AutomationElement aproperty = aproperties.FindFirstChild(cf => cf.ByName(property));
            AutomationElement alast, asub = null;
            while (!aproperties.BoundingRectangle.IntersectsWith(afirst.BoundingRectangle)) {
                Mouse.Scroll(1d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                afirst = aproperties.FindFirstChild(cf => cf.ByControlType(ControlType.DataItem));
            }
            aproperty = aproperties.FindFirstChild(cf => cf.ByName(property));
            if (aproperty == null)
                return;
            if (sub != null) {
                asub = aproperty.FindFirstChild(cf => cf.ByName(sub));
                while (asub == null || !aproperties.BoundingRectangle.IntersectsWith(asub.BoundingRectangle)) {
                    Mouse.Scroll(-1d);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    aproperty = aproperties.FindFirstChild(cf => cf.ByName(property));
                    asub = aproperty.FindFirstChild(cf => cf.ByName(sub));
                    alast = aproperties.FindAllChildren(cf => cf.ByControlType(ControlType.DataItem)).Last();
                    if (asub == null && aproperties.BoundingRectangle.IntersectsWith(alast.BoundingRectangle))
                        return;
                }
            }
            else {
                while (aproperty == null || !aproperties.BoundingRectangle.IntersectsWith(aproperty.BoundingRectangle)) {
                    Mouse.Scroll(-1d);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
                    aproperty = aproperties.FindFirstChild(cf => cf.ByName(property));
                    alast = aproperties.FindAllChildren(cf => cf.ByControlType(ControlType.DataItem)).Last();
                    if (aproperty == null && aproperties.BoundingRectangle.IntersectsWith(alast.BoundingRectangle))
                        return;
                }
                
            }
            Mouse.Scroll(-2d);
            if (opensub) {
                Mouse.Click(new Point {X = asub.BoundingRectangle.Left + 5, Y = asub.BoundingRectangle.Top + 5});
                Mouse.Scroll(-2d);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(100));
            }
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