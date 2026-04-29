using System;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using System.Drawing;
using System.Collections.Generic;

namespace FlaUITests.Util {
    public class AppProject {
        protected IDE_Main _ideMain;
        public string Name { get; set; }
        public string Path { get; set; }
        public string Config { get; set; }
        public string CPU { get; set; }
        public string WorkingVersion { get; set; }
        public Environment.Verbose verbose;
        readonly Dictionary<Components, string> DictComponents;
        List<ComponentInProject> components;

        public AppProject(IDE_Main ideMain) {
            _ideMain = ideMain;
        }
        public void LoadActiveProject() {
            if (_ideMain.IsProjectLoaded()) {
                string[] paths = _ideMain.GetProjectpath();
                Name = paths[2];
                Path = paths[0];
                Config = paths[1];
                AutomationElement activeConfig = _ideMain.GetActiveConfigurtion();
                AutomationElement [] allTreeItems = activeConfig.FindAllChildren(cf => cf.ByControlType(ControlType.TreeItem));
                CPU = allTreeItems[2].Name.Substring(3); //Assuming the CPU tree item is always the third tree item and starts with "BR_"
            }
            else
                Console.WriteLine("No project loaded.");
        }
        public AppProject(IDE_Main ideMain, string name, string path, string config, string cpu, Dictionary<Components, string> dictComponents, string workingVersion = null, Environment.Verbose verbose = Environment.Verbose.NONE) {
            _ideMain = ideMain;
            Name = name;
            Path = path;
            Config = config;
            CPU = cpu;
            WorkingVersion = workingVersion;
            DictComponents = dictComponents;
            this.verbose = verbose;

    /*        _ideMain.InvokeMenuItem(_ideMain.GetMenu("File"), "New Project...");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1)); // Wait for the New Project dialog to appear
            Window newProjectDialog = _ideMain.GetModalWindow("New Project");
            if (newProjectDialog == null) {
                Console.WriteLine("Error: New Project dialog did not appear.");
                return;
            }
            TreeConfig.ClickAutomationElement(newProjectDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("projectNameTextBox"))).AsTextBox());
            Keyboard.Type(name);
            TextBox pathTextBox = newProjectDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("pathTextBox"))).AsTextBox();
            if (pathTextBox.Text != path) {
                TreeConfig.ClickAutomationElement(pathTextBox);
                Keyboard.TypeSimultaneously(new FlaUI.Core.WindowsAPI.VirtualKeyShort[] { FlaUI.Core.WindowsAPI.VirtualKeyShort.CONTROL, FlaUI.Core.WindowsAPI.VirtualKeyShort.KEY_A });
                Keyboard.TypeVirtualKeyCode((ushort)FlaUI.Core.WindowsAPI.VirtualKeyShort.DELETE);
                Keyboard.Type(path);
            }
            if (workingVersion != null) {
                ComboBox versionComboBox = newProjectDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.ComboBox).And(cf.ByAutomationId("cbWorkingVersion"))).AsComboBox();
                if (versionComboBox.Value != workingVersion) {
                    TreeConfig.ClickAutomationElement(versionComboBox);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
                    TreeConfig.ClickComboBoxTreeItem(_ideMain.MainWindow, workingVersion);
                }
            }
            Button nextButton = newProjectDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Button).And(cf.ByName("Next >"))).AsButton();
            nextButton.Invoke();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            TextBox configTextBox = newProjectDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("configurationNameTextBox"))).AsTextBox();
            if (configTextBox.Text != config) {
                TreeConfig.ClickAutomationElement(configTextBox);
                Keyboard.Type(config);
            }
            nextButton.Invoke();
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
            TreeConfig.ClickAutomationElement(newProjectDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Edit).And(cf.ByAutomationId("searchTermTextBox"))).AsTextBox());
            foreach (char ch in CPU) {
                Keyboard.Type(ch);
                System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(500));
            }
            nextButton.Invoke();
            while (_ideMain.StatusBar.Name.IndexOf("Opening", StringComparison.OrdinalIgnoreCase) >= 0)
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));
            TreeConfig.IdeMain.WaitForMessage("finished.");
    */        Name += ".apj";
            TreeConfig.CurrentProject = this;
            if (dictComponents != null)
                components = new List<ComponentInProject>();
            if (this.verbose >= Environment.Verbose.LIGHT) {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Activating Simulation");
                Console.WriteLine("------------------------------------------");
            }
            //_ideMain.ActivateSimulation();
            foreach (KeyValuePair<Components, string> kvp in DictComponents) {
                ComponentInProject cip = null;
                System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
                switch (kvp.Key) {
                    case Components.AutomationRuntime:  cip = new AutomationRuntime(this, kvp.Value);   break;
                    case Components.mappView:           cip = new MappView(this, kvp.Value);            break;
                    case Components.OPCUACS:            cip = new OPCUACS(this, kvp.Value);             break;
                }
                components.Add(cip);
                cip.Verbose = this.verbose;
                if (this.verbose >= Environment.Verbose.LIGHT) {
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Initializing component: " + cip.ToString());
                    Console.WriteLine("------------------------------------------");
                }
                Init(cip);
            }
            if (this.verbose >= Environment.Verbose.LIGHT) {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Transferring ...");
                Console.WriteLine("------------------------------------------");
            }
            _ideMain.Transfer();
        }
        public void Init(ComponentInProject cip) {
            cip.InitComponent();
            if (verbose >= Environment.Verbose.LIGHT) {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("Building ...");
                Console.WriteLine("------------------------------------------");
            }
            Update();
        }
        public void Update () {
            _ideMain.Save();
            _ideMain.Build();
        }
        public void DeleteProject() {
            if (!_ideMain.App.HasExited) {
                    CloseProject();
                }
            //System.IO.Directory.Delete(Path + "\\" + Name + "\\", true);
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
            TreeConfig.ClickAutomationElement(progressBar.FindFirstChild(cf => cf.ByControlType(ControlType.Pane)));
            Keyboard.Type(projectPath + "\n");
            AutomationElement pane1 = openProjectDialog.FindFirstDescendant(cf => cf.ByControlType(ControlType.Pane).And(cf.ByName("Shell Folder View")));
            AutomationElement fileList = pane1.FindFirstChild(cf => cf.ByControlType(ControlType.List));
            AutomationElement [] children = fileList.FindAllChildren();
            AutomationElement targetItem = children.FirstOrDefault(c => c.Name.Contains(".apj"));
            string s = targetItem?.Name ?? "null";
            if (targetItem == null) {
                Console.WriteLine("Error: Could not find project file in Open Project dialog.");
                return;
            }
            targetItem.DoubleClick();
            while (_ideMain.StatusBar.Name.IndexOf("Opening", StringComparison.OrdinalIgnoreCase) >= 0);
            Console.WriteLine("Project " + projectPath + "\\" + s + " opened.");
        }
        public void ReadProject() {
            if (_ideMain.IsProjectLoaded()) {
                if (verbose >= Environment.Verbose.LIGHT) {
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("Reading project content ...");
                    Console.WriteLine("------------------------------------------");
                }
                new HardwareConfigReader(Path, Config).ReadHardwareTopology();
            }
        }
    }
}