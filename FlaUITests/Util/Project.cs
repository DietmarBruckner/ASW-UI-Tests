using System;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using System.Drawing;

namespace FlaUITests.Util {
    public class AppProject {
        private IDE_Main _ideMain;
        public string Name { get; set; }
        public string Path { get; set; }
        public string Config { get; set; }

        public AppProject(IDE_Main ideMain) {
            _ideMain = ideMain;
                if (_ideMain.IsProjectLoaded()) {
                    string[] paths = _ideMain.GetProjectpath();
                    Name = paths[2];
                    Path = paths[0];
                    Config = paths[1];
                }
                else {
                    Console.WriteLine("No project loaded.");
                }
        }
        public void BuildProject() {
            if (_ideMain.IsProjectLoaded()) {
                _ideMain.InvokeMenuItem(_ideMain.GetMenu("Build"), "Build Solution");
                Console.WriteLine("Project " + Name + " build initiated.");
            }
        }
        public void CloseProject() {
            if (_ideMain.IsProjectLoaded()) {
                _ideMain.InvokeMenuItem(_ideMain.GetMenu("File"), "Close Project");
                Console.WriteLine("Project " + Name + " closed.");
            }
        }
        public void OpenProject(string projectPath) {
            _ideMain.InvokeMenuItem(_ideMain.GetMenu("File"), "Open Project...");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1)); // Wait for the Open Project dialog to appear
            Window openProjectDialog = _ideMain.GetModalWindow("Open");
            if (openProjectDialog != null) {
                AutomationElement pane3 = openProjectDialog.FindFirstChild(cf => cf.ByControlType(ControlType.Pane).And(cf.ByAutomationId("40965")));
                AutomationElement comboBox = pane3.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByAutomationId("41477")));
                ProgressBar progressBar = comboBox.FindFirstChild(cf => cf.ByControlType(ControlType.ProgressBar)).AsProgressBar();
                AutomationElement pane = progressBar.FindFirstChild(cf => cf.ByControlType(ControlType.Pane));
                Point point = new Point { X = pane.BoundingRectangle.Left+ pane.BoundingRectangle.Width / 2, Y = pane.BoundingRectangle.Top + pane.BoundingRectangle.Height / 2 };
                Mouse.LeftClick(point);
                Keyboard.Type(projectPath + "\n");
                AutomationElement pane1 = openProjectDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Shell Folder View")));
                AutomationElement fileList = pane1.FindFirstChild(cf => cf.ByControlType(ControlType.List));
                AutomationElement [] children = fileList.FindAllChildren();
                AutomationElement targetItem = children.FirstOrDefault(c => c.Name.Contains(".apj"));
                string s = targetItem?.Name ?? "null";
                if (targetItem == null)
                {
                    Console.WriteLine("Error: Could not find project file in Open Project dialog.");
                    return;
                }
                targetItem.DoubleClick();
            while (_ideMain.StatusBar.Name.IndexOf("Opening", StringComparison.OrdinalIgnoreCase) >= 0);
                Console.WriteLine("Project " + projectPath + "\\" + s + " opened.");
            }
        }
        public void ReadProject() {
            if (_ideMain.IsProjectLoaded()) {
                HardwareTopology hardwareTopology = new HardwareConfigReader(Path, Config).ReadHardwareTopology();
             }
        }
    }
}