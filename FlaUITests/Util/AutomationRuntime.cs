using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using System;
using System.Linq;

namespace FlaUITests.Util {
    public partial class AutomationRuntime {

        public override void InitComponent() {
            TreeConfig.IdeMain.InitializeViews(projectExplorer: true);
            TreeConfig.IdeMain.SelectComponentVersion("Automation Runtime", Version);
        }
        public override void InsertComponent() {
            
        }
    }
}