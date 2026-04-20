using System;
using System.Data;

namespace FlaUITests.Util {
    public abstract class ComponentInProject : AppProject{
        public string ComponentVersion { get; set; }
        public Components component;
        public ComponentInProject(IDE_Main ideMain, Components component) : base(ideMain) {
            InitComponent(this.component = component);
        }
        public ComponentInProject(IDE_Main ideMain, Components component, string name, string path, string config, string cpu, string workingVersion = null, string ComponentVersion = null) : base(ideMain, name, path, config, cpu, workingVersion) {
            this.ComponentVersion = ComponentVersion;
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(4));
            InitComponent(this.component = component);
        }
        public void Init(Components comp)
        {
            _ideMain.ActivateSimulation();
            InitComponent(comp);
            Update();
        }
        public void Update ()
        {
            _ideMain.Save();
            _ideMain.Build();
            //_ideMain.Transfer();
        }
        public abstract void InitComponent(Components comp);
        public abstract void InsertComponent(Components comp);

    }
}