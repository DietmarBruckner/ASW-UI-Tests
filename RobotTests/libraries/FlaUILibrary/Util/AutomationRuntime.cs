using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Linq;

namespace FlaUILibrary.Util {
    public partial class AutomationRuntime {

        public override void InitComponent() {
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
            if (Verbose >= Util.Verbose.STEPS) {
                Console.WriteLine("==========================================");
                Console.WriteLine("Checking/setting Automation Runtime version to " + Version);
            }
            TreeConfig.IdeMain.SelectComponentVersion("Automation Runtime", Version);
        }
        public override void InsertComponent() {
            
        }
    }
}