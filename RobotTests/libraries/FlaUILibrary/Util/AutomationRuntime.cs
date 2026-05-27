using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Linq;

namespace FlaUILibrary.Util {
    public partial class AutomationRuntime {

        public override void InitComponent() {
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
            Util.ConsoleOut(Util.Verbose.STEPS, "Checking/setting Automation Runtime version to " + Version);
            TreeConfig.IdeMain.SelectComponentVersion(null, "Automation Runtime", Version);
        }
        public override void InsertComponent() {
            
        }
    }
}