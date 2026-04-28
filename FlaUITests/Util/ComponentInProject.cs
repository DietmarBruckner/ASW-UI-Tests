namespace FlaUITests.Util {
    public abstract class ComponentInProject {
        protected AppProject Project;
        protected string Version;
        public abstract void InitComponent();
        public abstract void InsertComponent();

        public ComponentInProject (AppProject project, string version) {
            Project = project;
            Version = version;
        }
        public override string ToString() {
            return "" + GetType().FullName + ", Version: " + Version;
        }
    }
    public partial class MappView :             ComponentInProject { public MappView(           AppProject project, string version) : base (project, version) {} }
    public partial class AutomationRuntime :    ComponentInProject { public AutomationRuntime(  AppProject project, string version) : base (project, version) {} }
    public partial class OPCUACS :              ComponentInProject { public OPCUACS(            AppProject project, string version) : base (project, version) {} }
/*     public partial class MappServices : ComponentInProject { }
    public partial class MappAxis : ComponentInProject { }
    public partial class Visualcomponents : ComponentInProject { }
    public partial class MappVision : ComponentInProject { }
    public partial class MappRobotics : ComponentInProject { }
    public partial class MappCNC : ComponentInProject { }
    public partial class MappTrak : ComponentInProject { }
 */}