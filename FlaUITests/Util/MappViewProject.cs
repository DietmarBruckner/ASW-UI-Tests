using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Linq;

namespace FlaUITests.Util {
    public class MappViewProject : AppProject {
        AutomationElement toolbox;
        AutomationElement outputWindow;
        AutomationElement toolBoxCategories;
        AutomationElement toolBoxContextContent;
        public MappViewProject(IDE_Main ideMain) : base(ideMain) {
            InitMappView();
        }
        public MappViewProject(IDE_Main ideMain, string name, string path, string config, string cpu) : base(ideMain, name, path, config, cpu) {
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
            InitMappView();
        }
        public void InitMappView() {
            _ideMain.InitializeViews(projectExplorer: true, toolbox: true, outputResults: true);
            toolBoxCategories = toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByAutomationId("_categoriesListView")));
            toolBoxContextContent = toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("_elementsListView")));
            AutomationElement mappViewToolBoxItem = toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByName("mapp View"))) ?? throw new Exception("mapp View toolbox item not found - not installed?");
            AutomationElement [] allDesc = mappViewToolBoxItem.FindAllDescendants();
            if (allDesc[0].AsCheckBox().IsChecked == false) {
                mappViewToolBoxItem.Click();
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            AutomationElement mappViewElementItem = toolBoxContextContent.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataItem).And(cf.ByName("mapp View"))) ?? throw new Exception("mapp View element not found");
            mappViewElementItem.DoubleClick();  //mapp View wizard opens
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            Window newMappViewDialog = _ideMain.GetModalWindow("Insert mapp View solution");
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
            _ideMain.WaitParsing();
        }
    }
}