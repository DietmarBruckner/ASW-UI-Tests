using System;
using System.Linq;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using System.Drawing;
using System.Collections.Generic;

namespace FlaUILibrary.Util {
    public class AppProject {
        protected IDE_Main _ideMain;
        public string Name { get; set; }
        public string Path { get; set; }
        public string Config { get; set; }
        public string CPU { get; set; }
        public string WorkingVersion { get; set; }
        public Util.Verbose verbose;
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
        public AppProject(IDE_Main ideMain, string name, string path, string config, string cpu, Dictionary<Components, string> dictComponents, string workingVersion = null, Util.Verbose verbose = Util.Verbose.NONE) {
            _ideMain = ideMain;
            Name = name;
            Path = path;
            Config = config;
            CPU = cpu;
            WorkingVersion = workingVersion;
            DictComponents = dictComponents;
            this.verbose = verbose;

            Name += ".apj";
            TreeConfig.CurrentProject = this;
            if (dictComponents != null)
                components = new List<ComponentInProject>();
            Util.ConsoleOut(Util.Verbose.LIGHT, "Activating Simulation");
            IDE_Main.ActivateSimulation();
            foreach (KeyValuePair<Components, string> kvp in DictComponents) {
                ComponentInProject cip = null;
                   switch (kvp.Key) {
                    case Components.AutomationRuntime:  cip = new AutomationRuntime(this, kvp.Value);   break;
                    case Components.mappView:           cip = new MappView(this, kvp.Value);            break;
                    case Components.OPCUACS:            cip = new OPCUACS(this, kvp.Value);             break;
                }
                components.Add(cip);
                cip.Verbose = this.verbose;
                Util.ConsoleOut(Util.Verbose.LIGHT, "Initializing component: " + cip.ToString());
                Init(cip);
            }
            Util.ConsoleOut(Util.Verbose.LIGHT, "Transferring ...");
            _ideMain.Transfer();
        }
        public void Init(ComponentInProject cip) {
            cip.InitComponent();
            Update();
        }
        public void Update () {
            _ideMain.SaveAll();
            Util.ConsoleOut(Util.Verbose.LIGHT, "Building ...");
            _ideMain.Build();
        }
        public void DeleteProject() {
            if (!IDE_Main.App.HasExited) {
                    CloseProject();
                }
            //System.IO.Directory.Delete(Path + "\\" + Name + "\\", true);
        }
        public void CloseProject() {
            if (_ideMain.IsProjectLoaded()) {
                IDE_Main.InvokeMenuItem(IDE_Main.GetMenu("File"), "Close Project");
                Console.WriteLine("Project " + Name + " closed.");
            }
        }
        public void OpenProject(string projectPath) {
            IDE_Main.InvokeMenuItem(IDE_Main.GetMenu("File"), "Open Project...");
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
            while (IDE_Main.StatusBar.Name.IndexOf("Opening", StringComparison.OrdinalIgnoreCase) >= 0);
            Console.WriteLine("Project " + projectPath + "\\" + s + " opened.");
        }
        public void ReadProject() {
            if (_ideMain.IsProjectLoaded()) {
                Util.ConsoleOut(Util.Verbose.LIGHT, "Reading project content ...");
                new HardwareConfigReader(Path, Config).ReadHardwareTopology();
            }
        }
    }
}