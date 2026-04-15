using System;
using System.Linq;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using System.Drawing;


namespace FlaUITests.Util {
    public class AppProject {
        protected IDE_Main _ideMain;
        public string Name { get; set; }
        public string Path { get; set; }
        public string Config { get; set; }
        public string CPU { get; set; }

        public AppProject(IDE_Main ideMain) {
            _ideMain = ideMain;
            if (_ideMain.IsProjectLoaded()) {
                string[] paths = _ideMain.GetProjectpath();
                Name = paths[2];
                Path = paths[0];
                Config = paths[1];
                AutomationElement activeConfig = _ideMain.GetActiveConfigurtion();
                AutomationElement [] allTreeItems = activeConfig.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem));
                CPU = allTreeItems[2].Name.Substring(3); //Assuming the CPU tree item is always the third tree item and starts with "BR_"
            }
            else {
                Console.WriteLine("No project loaded.");
            }
        }
        public AppProject(IDE_Main ideMain, string name, string path, string config, string cpu) {
            _ideMain = ideMain;
            Name = name;
            Path = path;
            Config = config;
            CPU = cpu;

            _ideMain.InvokeMenuItem(_ideMain.GetMenu("File"), "New Project...");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1)); // Wait for the New Project dialog to appear
            Window newProjectDialog = _ideMain.GetModalWindow("New Project");
            if (newProjectDialog == null) {
                Console.WriteLine("Error: New Project dialog did not appear.");
                return;
            }
            TextBox nameTextBox = newProjectDialog.FindAllDescendants(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("projectNameTextBox")))[0].AsTextBox();
            Point point = new Point { X = nameTextBox.BoundingRectangle.Left+ nameTextBox.BoundingRectangle.Width / 2, Y = nameTextBox.BoundingRectangle.Top + nameTextBox.BoundingRectangle.Height / 2 };
            Mouse.LeftClick(point);
            Keyboard.Type(name);
            TextBox pathTextBox = newProjectDialog.FindAllDescendants(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("pathTextBox")))[0].AsTextBox();
            if (pathTextBox.Text != path) {
                point = new Point { X = pathTextBox.BoundingRectangle.Left+ pathTextBox.BoundingRectangle.Width / 2, Y = pathTextBox.BoundingRectangle.Top + pathTextBox.BoundingRectangle.Height / 2 };
                Mouse.LeftClick(point);
                Keyboard.TypeSimultaneously(new FlaUI.Core.WindowsAPI.VirtualKeyShort[] { FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A });
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.DELETE);
                Keyboard.Type(path);
            }
            Button nextButton = newProjectDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Next >"))).AsButton();
            nextButton.Invoke();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            TextBox configTextBox = newProjectDialog.FindAllDescendants(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("configurationNameTextBox")))[0].AsTextBox();
            if (configTextBox.Text != config) {
                point = new Point { X = configTextBox.BoundingRectangle.Left+ configTextBox.BoundingRectangle.Width / 2, Y = configTextBox.BoundingRectangle.Top + configTextBox.BoundingRectangle.Height / 2 };
                Mouse.LeftClick(point);
                Keyboard.Type(config);
            }
            nextButton.Invoke();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
            TextBox searchTextBox = newProjectDialog.FindAllDescendants(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("searchTermTextBox")))[0].AsTextBox();
            point = new Point { X = searchTextBox.BoundingRectangle.Left+ searchTextBox.BoundingRectangle.Width / 2, Y = searchTextBox.BoundingRectangle.Top + searchTextBox.BoundingRectangle.Height / 2 };
            Mouse.LeftClick(point);
            foreach (char ch in CPU) {
                Keyboard.Type(ch);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
            nextButton.Invoke();
            while (_ideMain.StatusBar.Name.IndexOf("Opening", StringComparison.OrdinalIgnoreCase) >= 0);
            Name += ".apj";
            TreeConfig.CurrentProject = this;
        }
        public void DeleteProject() {
            if (!_ideMain.App.HasExited) {
                    CloseProject();
                }
            System.IO.Directory.Delete(Path + "\\" + Name + "\\", true);
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
            if (openProjectDialog == null) {
                Console.WriteLine("Error: Open Project dialog did not appear.");
                return;
            }
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
        public void ReadProject() {
            if (_ideMain.IsProjectLoaded()) {
                HardwareTopology hardwareTopology = new HardwareConfigReader(Path, Config).ReadHardwareTopology();
             }
        }
    }
}