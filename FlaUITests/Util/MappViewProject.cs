using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;

namespace FlaUITests.Util {
    public class MappViewProject : AppProject {
        AutomationElement toolbox;
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
            toolbox = _ideMain.Toolbox;
            if (toolbox == null) {
                _ideMain.InvokeMenuItem(_ideMain.GetMenu("View"), "Toolbox");
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
                toolbox = _ideMain.MainWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Toolbox")));
            }
            toolBoxCategories = toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.List).And(cf.ByAutomationId("_categoriesListView")));
            toolBoxContextContent = toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataGrid).And(cf.ByAutomationId("_elementsListView")));
            AutomationElement mappViewToolBoxItem = toolbox.FindFirstDescendant(cf => cf.ByControlType(ControlType.ListItem).And(cf.ByName("mapp View"))) ?? throw new Exception("mapp View toolbox item not found - not installed?");
            AutomationElement [] allDesc = mappViewToolBoxItem.FindAllDescendants();
            mappViewToolBoxItem.Click();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(2));
            AutomationElement mappViewElementItem = toolBoxContextContent.FindFirstDescendant(cf => cf.ByControlType(ControlType.DataItem).And(cf.ByName("mapp View"))) ?? throw new Exception("mapp View element not found");
            mappViewElementItem.DoubleClick();
        }
    }
}