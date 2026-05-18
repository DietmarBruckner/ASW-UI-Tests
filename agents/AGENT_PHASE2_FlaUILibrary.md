---
description: "Phase 2: Develop FlaUILibrary wrapper for Robot Framework integration with FlaUI"
applyTo: "RobotTests/libraries/FlaUILibrary/**"
---

# Agent Instruction: Create FlaUILibrary Wrapper (Phase 2)

## Task Summary

Develop and implement the **FlaUILibrary** — a wrapper that exposes FlaUI (Windows UI Automation library) functionality to Robot Framework as custom keywords. This is a critical dependency for Phases 3 and 4.

## Inputs & Context

### Source Code Reference
- **FlaUITests Project**: Located in `FlaUITests/` — reference for FlaUI usage patterns
  - [FlaUITests/AutomationStudio6.cs](../FlaUITests/AutomationStudio6.cs) — IDE initialization and attachment
  - [FlaUITests/Program.cs](../FlaUITests/Program.cs) — example test flow
  - [FlaUITests/Util/IDE_Main.cs](../FlaUITests/Util/IDE_Main.cs) — comprehensive IDE automation patterns (400+ lines)
  - [FlaUITests/Util/Project.cs](../FlaUITests/Util/Project.cs) — project lifecycle management
  - [FlaUITests/Util/Components.cs](../FlaUITests/Util/Components.cs) — component enumeration
  - [FlaUITests/Util/TreeConfig.cs](../FlaUITests/Util/TreeConfig.cs) — UI tree navigation helpers
  - [FlaUITests/Util/HardwareConfigReader.cs](../FlaUITests/Util/HardwareConfigReader.cs) — configuration parsing
  - [FlaUITests/Util/MappView.cs](../FlaUITests/Util/MappView.cs) — component-specific patterns

### Dependencies
- **FlaUI**: v5.0.0 (currently used by FlaUITests)
  - FlaUI.Core, FlaUI.UIA2
  - UIAutomationClient.dll (Windows built-in)
- **Tesseract**: v5.2.0 (for OCR-based text recognition)
- **Robot Framework**: v7.0+ (keyword library integration)

### Existing Keyword Stubs
Review the template keyword files in `RobotTests/keywords/`:
- [project_keywords.robot](../RobotTests/keywords/project_keywords.robot)
- [ide_keywords.robot](../RobotTests/keywords/ide_keywords.robot)
- [component_keywords.robot](../RobotTests/keywords/component_keywords.robot)
- [widget_keywords.robot](../RobotTests/keywords/widget_keywords.robot)

These define the expected keyword signatures that FlaUILibrary must support.

### Configuration
- [RobotTests/config/hardware_config.robot](../RobotTests/config/hardware_config.robot) — runtime variables (IDE path, CPU type, ports, timeouts)

## Deliverables

### 1. FlaUILibrary Implementation (Choose One Approach)

#### **Option A: C# Class Library** (Recommended)
Create a C# class library (.csproj) that:
- Inherits from `RobotLibrary` or uses `RobotLibraryListener` interface
- Exposes public methods as Robot Framework keywords (method name = keyword name)
- Packages as a .NET DLL
- Registers with Robot Framework via Python wrapper or `robot_library_scope` metadata

**Advantages**: Direct FlaUI integration, type safety, better performance  
**Disadvantages**: Requires .NET/C# build, additional wrapper complexity

**File structure**:
```
RobotTests/libraries/FlaUILibrary/
├── FlaUILibrary.csproj          # .NET project file
├── FlaUILibraryKeywords.cs      # Main keyword class
├── FlaUILibraryListener.cs      # Event listener (optional)
├── packages.config              # NuGet packages
└── bin/Release/FlaUILibrary.dll # Built output
```

#### **Option B: Python Wrapper with IronPython** (Alternative)
Create a Python module that:
- Uses `clr` (IronPython) to call FlaUI from C#
- Implements Robot Library interface as Python class
- Registers keywords as Python methods

**Advantages**: Single language, easier to maintain  
**Disadvantages**: IronPython performance, dependency management

**File structure**:
```
RobotTests/libraries/FlaUILibrary/
├── __init__.py                  # Robot Library entry point
├── flaui_keywords.py            # Keyword implementations
├── flaui_utils.py               # Utility functions
└── requirements.txt             # Python dependencies
```

### 2. Core Keywords (Minimum Required)

Implement the following keywords (as Robot Framework methods):

#### **Initialization & Lifecycle**
- `Initialize Automation Studio` — Launch IDE process via config path
  - Args: (optional timeout, optional args)
  - Returns: process handle or True/False
- `Close Application` — Close IDE and cleanup
  - Args: save_changes=True
  - Returns: success status
- `Wait Until IDE Is Ready` — Polls for IDE window readiness
  - Args: timeout (seconds)
  - Returns: success

#### **Element Finding & Waiting**
- `Find UI Element` — Locate element by criteria (name, ID, xpath, etc.)
  - Args: element_identifier, search_type="name", timeout=10
  - Returns: element reference or raises exception
- `Wait For Element` — Wait for element to appear
  - Args: element_identifier, timeout=10
  - Returns: success or timeout exception
- `Element Should Exist` — Assert element is present
  - Args: element_identifier
- `Element Should Not Exist` — Assert element is absent
  - Args: element_identifier

#### **User Interactions**
- `Click Element` — Click on element
  - Args: element_identifier, button="left", double=False
- `Type Text` — Type into focused element
  - Args: text
- `Type Into Dialog Field` — Type into labeled dialog field
  - Args: field_label, text
- `Right Click Element` — Right-click for context menu
  - Args: element_identifier
- `Double Click Element` — Double-click element
  - Args: element_identifier
- `Hover Element` — Hover/move mouse to element
  - Args: element_identifier

#### **Dialog & Menu Handling**
- `Click Dialog Button` — Click button in modal dialog
  - Args: button_name (OK, Cancel, Yes, No, etc.)
- `Click Menu Item` — Click menu item
  - Args: menu_item_name
- `Set Dialog Field Value` — Set value in dialog field
  - Args: field_label, value
- `Get Dialog Text` — Read text from dialog element
  - Args: element_label
  - Returns: text value

#### **Tree Navigation (Project Tree)**
- `Activate Tree Leaf` — Expand/navigate to tree node and select
  - Args: tree_path (e.g., "Logical View/MyComponent")
- `Expand Tree Node` — Expand a tree node
  - Args: node_name
- `Collapse Tree Node` — Collapse a tree node
  - Args: node_name
- `Get Tree Node Count` — Count children of tree node
  - Args: node_name
  - Returns: integer count

#### **Text & Property Inspection**
- `Get Text From Element` — Read visible text
  - Args: element_identifier
  - Returns: text string
- `Get Property Value` — Read element property
  - Args: element_identifier, property_name
  - Returns: property value
- `Set Property Value` — Set element property
  - Args: element_identifier, property_name, value

#### **Diagnostics & Screenshots**
- `Take Screenshot` — Capture screen or element screenshot
  - Args: (optional filename, optional outputdir)
  - Returns: file path
- `Get IDE State` — Capture IDE tree, open dialogs, state info
  - Returns: structured state dictionary

#### **Wait & Synchronization**
- `Wait For Tooltip` — Wait for tooltip to appear
  - Args: timeout=2s
- `Wait For Build To Complete` — Wait for IDE build to finish
  - Args: timeout=60s
- `Wait For Transfer To Complete` — Wait for CPU transfer
  - Args: timeout=120s

#### **Text Recognition (OCR via Tesseract)**
- `Capture Text From Screen` — Extract text from screen region
  - Args: (optional region)
  - Returns: recognized text
- `Find Text On Screen` — Locate text via OCR
  - Args: text_string
  - Returns: x, y coordinates

### 3. Component-Specific Helpers (Optional but Recommended)

Implement higher-level keywords based on [FlaUITests/Util/](../FlaUITests/Util/) patterns:

- `Select Widget Type` — Select widget from toolbox by name
- `Get Widgets From Config` — Read widget list from file
- `Launch Application` — Generic app launch wrapper
- `Attach To Process` — Attach debugger to running process

### 4. Documentation & Configuration

Create or update:
- **[RobotTests/libraries/FlaUILibrary/README.md](../RobotTests/libraries/FlaUILibrary/README.md)** — Library setup, keyword docs
- **[RobotTests/requirements.txt](../RobotTests/requirements.txt)** — Add FlaUILibrary installation instructions
- **[RobotTests/robot.cfg](../RobotTests/robot.cfg)** — Configure library path

## Implementation Guidelines

### Code Structure
1. **Separate concerns**: UI interaction logic separate from keyword wrappers
2. **Error handling**: All keywords should catch and wrap FlaUI exceptions with meaningful messages
3. **Logging**: Use Robot Framework's `BuiltIn.log()` for execution tracing
4. **Timeouts**: Respect timeout parameters; raise `TimeoutError` on failure
5. **Resource cleanup**: Implement `__del__()` or lifecycle methods to close handles

### Example Keyword Implementation (Python)

```python
from robot.api.deku import BuiltIn
from FlaUI.Core import Application

class FlaUILibrary:
    ROBOT_LIBRARY_SCOPE = 'SUITE'
    
    def __init__(self):
        self.app = None
        self.logger = BuiltIn().get_library_instance('BuiltIn')
    
    def initialize_automation_studio(self, app_path=None, timeout=10):
        """
        Initialize and launch Automation Studio IDE.
        
        Args:
            app_path: Path to pg.exe (from config if not provided)
            timeout: Wait timeout in seconds
        
        Returns:
            True if successful
        """
        try:
            if app_path is None:
                app_path = self._get_from_config('AS_IDE_PATH')
            
            self.app = Application.Launch(app_path)
            self.logger.log(f"Automation Studio launched: {app_path}", 'INFO')
            
            # Wait for IDE window
            if not self._wait_for_ide_window(timeout):
                raise TimeoutError(f"IDE did not appear within {timeout}s")
            
            return True
        except Exception as e:
            self.logger.log(f"Failed to initialize AS: {e}", 'ERROR')
            raise
    
    def click_element(self, element_id):
        """Click on element identified by element_id."""
        try:
            element = self._find_element(element_id)
            element.Click()
            self.logger.log(f"Clicked: {element_id}", 'INFO')
        except Exception as e:
            self.logger.log(f"Failed to click {element_id}: {e}", 'ERROR')
            raise
```

### Example Keyword Implementation (C#)

```csharp
using Robot.Framework;
using FlaUI.Core;

public class FlaUILibraryKeywords
{
    private Application _app;
    
    [RobotKeyword("Initialize and launch Automation Studio IDE.")]
    public bool InitializeAutomationStudio(string appPath = null, string timeout = "10")
    {
        try
        {
            if (string.IsNullOrEmpty(appPath))
                appPath = GetConfigValue("AS_IDE_PATH");
            
            _app = Application.Launch(appPath);
            BuiltIn.Log($"Automation Studio launched: {appPath}", "INFO");
            
            if (!WaitForIDEWindow(int.Parse(timeout)))
                throw new TimeoutException($"IDE did not appear within {timeout}s");
            
            return true;
        }
        catch (Exception ex)
        {
            BuiltIn.Log($"Failed to initialize AS: {ex.Message}", "ERROR");
            throw;
        }
    }
}
```

## Verification Checklist

- [ ] Library implements minimum 25+ core keywords listed above
- [ ] All keywords follow Robot Framework naming conventions
- [ ] Keywords handle timeouts and errors gracefully
- [ ] Keywords are documented with docstrings
- [ ] Library integrates with FlaUITests.csproj dependencies (FlaUI v5.0.0)
- [ ] Library registers successfully with Robot Framework
- [ ] `robot --dryrun` passes without errors
- [ ] Sample test runs (e.g., `robot tests/project_creation/`) validate keywords work
- [ ] FlaUILibrary path is set in RobotTests configuration

## Success Criteria

1. ✅ FlaUILibrary can be imported by Robot Framework
2. ✅ `robot --list` shows all 25+ keywords
3. ✅ Template keywords in [RobotTests/keywords/](../RobotTests/keywords/) resolve to FlaUILibrary implementations
4. ✅ Sample test execution (dry-run at minimum): `robot --dryrun tests/`
5. ✅ Documentation describes all keywords and usage

## Next Steps (After Completion)

Once FlaUILibrary is complete:
1. **Phase 3 Agent** will implement Robot keywords in [RobotTests/keywords/](../RobotTests/keywords/) using FlaUILibrary
2. **Phase 4 Agent** will convert FlaUITests C# to .robot test cases
3. **Phase 5** will execute full test suite for validation

## Related Files

- [RobotTests/keywords/](../RobotTests/keywords/) — Keyword templates
- [FlaUITests/Util/](../FlaUITests/Util/) — Reference implementations
- [RobotTests/config/hardware_config.robot](../RobotTests/config/hardware_config.robot) — Runtime config
- [RobotTests/README.md](../RobotTests/README.md) — Project overview

---

**Phase**: 2 / 5  
**Priority**: Critical (blocks Phases 3 & 4)  
**Estimated Effort**: 40-60 hours (depending on approach and testing)
