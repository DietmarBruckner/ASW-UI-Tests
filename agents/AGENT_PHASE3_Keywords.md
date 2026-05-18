---
description: "Phase 3: Implement Robot Framework keywords for Automation Studio automation"
applyTo: "RobotTests/keywords/**"
---

# Agent Instruction: Implement Robot Framework Keywords (Phase 3)

## Task Summary

Implement all Robot Framework keywords defined in the template files by converting logic from the FlaUITests C# utilities. This bridges the gap between FlaUILibrary (Phase 2) and actual test cases (Phase 4).

## Inputs & Context

### Dependency: Phase 2 Completion
- ✅ **FlaUILibrary** must be complete and registered with Robot Framework
- All FlaUILibrary keywords available as `BuiltIn` keywords in .robot files

### Source Code Reference

#### Utilities to Convert
Review and extract patterns from FlaUITests:

| C# File | Purpose | Keyword Target |
|---------|---------|-----------------|
| [Project.cs](../FlaUITests/Util/Project.cs) | Project lifecycle (create, load, delete) | [project_keywords.robot](../RobotTests/keywords/project_keywords.robot) |
| [AutomationStudio6.cs](../FlaUITests/AutomationStudio6.cs) | IDE initialization and project dialogs | [project_keywords.robot](../RobotTests/keywords/project_keywords.robot) |
| [IDE_Main.cs](../FlaUITests/Util/IDE_Main.cs) | IDE UI automation (menus, tree, dialogs) - 400+ lines | [ide_keywords.robot](../RobotTests/keywords/ide_keywords.robot) |
| [MappView.cs](../FlaUITests/Util/MappView.cs) | MappView component setup, visualization config | [component_keywords.robot](../RobotTests/keywords/component_keywords.robot) |
| [TreeConfig.cs](../FlaUITests/Util/TreeConfig.cs) | UI tree navigation helpers | [ide_keywords.robot](../RobotTests/keywords/ide_keywords.robot) |
| [Components.cs](../FlaUITests/Util/Components.cs) | Component enumeration | [component_keywords.robot](../RobotTests/keywords/component_keywords.robot) |
| [HardwareConfigReader.cs](../FlaUITests/Util/HardwareConfigReader.cs) | Config file parsing | All keyword files (config support) |
| [MappViewObjects.cs](../FlaUITests/Util/AS_Objects/MappViewObjects.cs) | Widget taxonomy & categories | [widget_keywords.robot](../RobotTests/keywords/widget_keywords.robot) |

#### Configuration Reference
- [RobotTests/config/hardware_config.robot](../RobotTests/config/hardware_config.robot) — Runtime variables to use in keywords
- [RobotTests/config/Widgets.txt](../RobotTests/config/Widgets.txt) — Widget list (85+ types)

### Existing Keyword Stubs
All four keyword files have **skeleton implementations**:
- [project_keywords.robot](../RobotTests/keywords/project_keywords.robot) — Project management keywords (stub form)
- [ide_keywords.robot](../RobotTests/keywords/ide_keywords.robot) — IDE interaction keywords (stub form)
- [component_keywords.robot](../RobotTests/keywords/component_keywords.robot) — Component setup keywords (stub form)
- [widget_keywords.robot](../RobotTests/keywords/widget_keywords.robot) — Widget testing keywords (stub form)

**Task**: Replace stubs with full implementations calling FlaUILibrary.

## Deliverables

### 1. Implement [project_keywords.robot](../RobotTests/keywords/project_keywords.robot)

**Source**: [Project.cs](../FlaUITests/Util/Project.cs) + [AutomationStudio6.cs](../FlaUITests/AutomationStudio6.cs)

#### Keywords to Implement

**`Create New Project In Automation Studio`**
```robot
[Arguments]    ${project_name}    ${project_path}    ${cpu_type}=${CPU_TYPE}
```
- Implements: `new Project().CreateProject(...)` flow
- Steps:
  1. Call `Initialize Automation Studio` (from ide_keywords)
  2. Trigger "New Project" dialog via menu
  3. Enter project_name in dialog field
  4. Set project_path directory
  5. Select cpu_type (X20CP1684, etc.) from dropdown
  6. Click OK button
  7. Wait for project to appear in tree view
- Error handling: Catch dialog not found, invalid path, etc.

**`Select CPU And Configuration`**
```robot
[Arguments]    ${cpu_type}
```
- Implements: CPU selection from dialog or configuration view
- Steps: Find CPU dropdown → Select cpu_type → Verify selection
- Handles: X20CP1684, X20CP1586, etc.

**`Load Existing Project`**
```robot
[Arguments]    ${project_path}
```
- Implements: `Application.LoadProject(projectPath)` equivalent
- Steps:
  1. Call `Initialize Automation Studio`
  2. Open File → Open Project menu
  3. Enter project_path in file dialog
  4. Wait for project tree to populate
  5. Verify project loaded (check window title or tree root)

**`Delete Project`**
```robot
[Arguments]    ${project_path}
```
- Implements: File system cleanup
- Steps:
  1. Check if directory exists
  2. Recursively delete directory and contents
  3. Log result

**`Switch To Logical View`** / **`Switch To Configuration View`**
- Navigate to specific IDE views (tabs/tree branches)
- Calls: `Activate Tree Leaf    Logical View` or similar

**`Build Project`** / **`Transfer Project To CPU`**
- Build: File → Build → Build Active Project, wait for completion
- Transfer: Online → Transfer, wait for completion signal

### 2. Implement [ide_keywords.robot](../RobotTests/keywords/ide_keywords.robot)

**Source**: [IDE_Main.cs](../FlaUITests/Util/IDE_Main.cs) (400+ lines) + [TreeConfig.cs](../FlaUITests/Util/TreeConfig.cs)

#### Keywords to Implement

**`Initialize Automation Studio`**
```robot
```
- Implements: `IDE_Main.Initialize()` or `AutomationStudio6.Initialize()`
- Steps:
  1. Launch process: `${AS_IDE_PATH}` (from hardware_config)
  2. Wait for IDE main window (with timeout)
  3. Wait for UI tree to be responsive
  4. Log IDE version/status

**`Close Automation Studio`**
```robot
[Arguments]    ${save_changes}=True
```
- Implements: IDE shutdown with optional save
- Steps:
  1. Send Close command (Alt+F4 or File → Exit)
  2. If save_changes, click Save in dialog
  3. Wait for process to terminate
  4. Clean up handles

**`Navigate To Tree Leaf`** / **`Activate Tree Leaf`**
```robot
[Arguments]    ${tree_path}
```
- Implements: `TreeConfig.ActivateTreeLeaf(path)` equivalent
- Steps:
  1. Parse tree_path (e.g., "Logical View/MyProject/MyLibrary")
  2. For each path segment:
     - Find tree node by name
     - Expand if collapsed
     - Navigate to next level
  3. Select final leaf
  4. Return success or raise exception

**`Expand Tree Node`** / **`Collapse Tree Node`**
```robot
[Arguments]    ${node_name}
```
- Toggle tree node expansion state
- Use FlaUILibrary's `Double Click Element` or expand button

**`Open Menu`** / **`Click Menu Item`**
```robot
[Arguments]    ${menu_name}    (${menu_item})
```
- Implements menu navigation
- Steps:
  1. Find menu bar
  2. Click menu_name
  3. If menu_item provided, find and click menu_item in submenu
  4. Wait for menu to appear/close

**`Open Context Menu`** / **`Select From Context Menu`**
```robot
[Arguments]    ${element_name}    (${menu_item})
```
- Right-click element to show context menu
- If menu_item provided, click it
- Uses FlaUILibrary's `Right Click Element`

**`Handle Modal Dialog`**
```robot
[Arguments]    ${dialog_title}    ${button_to_click}=OK
```
- Generic modal handler
- Steps:
  1. Wait for dialog with title dialog_title
  2. Click button button_to_click (OK, Cancel, Yes, No, etc.)
  3. Wait for dialog to close

**`Take IDE Screenshot`**
```robot
[Arguments]    ${filename}=screenshot    (${outputdir}=../resources/)
```
- Capture IDE window or full screen
- Save to outputdir with filename
- Return file path for logging

**`Switch To View`**
```robot
[Arguments]    ${view_name}
```
- Switch between IDE views (LogicalView, ConfigurationView, OnlineView)
- May involve clicking tabs or navigating tree

**`Wait Until IDE Is Ready`**
```robot
[Arguments]    ${timeout}=30s
```
- Poll IDE readiness (window exists, not busy, tree responsive)
- Raise TimeoutError if IDE not ready after timeout

### 3. Implement [component_keywords.robot](../RobotTests/keywords/component_keywords.robot)

**Source**: [MappView.cs](../FlaUITests/Util/MappView.cs), [AutomationRuntime.cs](../FlaUITests/Util/AutomationRuntime.cs), [OPCUACS.cs](../FlaUITests/Util/OPCUACS.cs), [ComponentInProject.cs](../FlaUITests/Util/ComponentInProject.cs)

#### Keywords to Implement

**`Initialize MappView Component`**
```robot
[Arguments]    ${protocol}=${MAPPVIEW_PROTOCOL}    ${port}=${MAPPVIEW_HTTP_PORT}
```
- Implements: `MappViewComponent.InitComponent()` + `InsertComponent()`
- Steps:
  1. Navigate to Configuration View
  2. Expand Components section in tree
  3. Right-click → Add Component → Select "MappView Server"
  4. Configure protocol (HTTP/HTTPS)
  5. Configure port number
  6. Apply configuration

**`Configure MappView Protocol`** / **`Configure MappView Port`**
```robot
[Arguments]    ${protocol}    OR    ${port}
```
- Set specific MappView properties in configuration
- Navigate to MappView settings → Find property field → Set value

**`Create Visualization Project`**
```robot
[Arguments]    ${project_name}=Test_Visu
```
- Implements: MappView visualization creation (from MappView.cs)
- Steps:
  1. Navigate to MappView project area
  2. Right-click Visualizations → New Visualization
  3. Enter project_name
  4. Click OK
  5. Verify new visualization appears in tree

**`Insert MappView Widget`**
```robot
[Arguments]    ${widget_type}    ${widget_name}    ${widget_id}
```
- Implements: Single widget insertion from MappViewObjects
- Steps:
  1. Open visualization for editing (double-click)
  2. Right-click on canvas → Insert Widget
  3. Select widget_type (Button, Label, etc.)
  4. Set Name property = widget_name
  5. Set ID property = widget_id
  6. Verify widget appears in visualization

**`Configure Widget Property`**
```robot
[Arguments]    ${widget_name}    ${property_name}    ${property_value}
```
- Implements: Widget property modification
- Steps:
  1. Select widget by widget_name in properties panel
  2. Find property_name in properties list
  3. Set value to property_value
  4. Apply changes

**`Insert All Widget Types`**
```robot
```
- Bulk insert all widgets from config/Widgets.txt
- Steps:
  1. Read Widgets.txt → Extract widget list (85+ types)
  2. Create visualization if needed
  3. Loop through each widget:
     - Call `Insert MappView Widget    ${widget}    widget_${index}    ${index}`
  4. Log total count and success status

**`Initialize OPCUA Component`**
```robot
[Arguments]    ${port}=${OPCUA_PORT}
```
- Implements: OPCUA component setup (from OPCUACS.cs)
- Steps:
  1. Navigate to Configuration View → Components
  2. Right-click → Add Component → Select "OPCUA"
  3. Configure port number
  4. Enable component (Enabled = True)
  5. Apply configuration

**`Initialize AutomationRuntime Component`**
```robot
[Arguments]    ${min_version}=${AUTOMATIONRUNTIME_MIN_VERSION}
```
- Implements: AutomationRuntime setup
- Steps:
  1. Navigate to Configuration View → Components
  2. Right-click → Add Component → Select "AutomationRuntime"
  3. Set Version to min_version (6.5+)
  4. Apply configuration

**`Build And Transfer Project`**
```robot
```
- Combines build and transfer workflows
- Steps:
  1. Call `Build Project` (from project_keywords)
  2. Wait for build completion
  3. Call `Transfer Project To CPU` (from project_keywords)
  4. Wait for transfer completion

### 4. Implement [widget_keywords.robot](../RobotTests/keywords/widget_keywords.robot)

**Source**: [MappViewObjects.cs](../FlaUITests/Util/AS_Objects/MappViewObjects.cs) + [Widgets.txt](../RobotTests/config/Widgets.txt)

#### Keywords to Implement

**`Get Widgets From Config`**
```robot
[Arguments]    ${config_file_path}
```
- Reads widget list from file
- Steps:
  1. Open config_file_path
  2. Read all lines
  3. Strip whitespace and empty lines
  4. Return list of widget names
- Returns: List of 85+ widget types (AlarmHistory, Button, CheckBox, etc.)

**`Test Widget Category`**
```robot
[Arguments]    ${category_name}
```
- Tests all widgets in a category (e.g., "Buttons", "Charts")
- Requires mapping of widgets to categories (from MappViewObjects)
- Steps:
  1. Get widgets in category_name
  2. For each widget: Call `Test Single Widget    ${widget}    ${category_name}_${index}`
  3. Log pass/fail count

**`Test Single Widget`**
```robot
[Arguments]    ${widget_type}    ${widget_id}
```
- Test basic widget operations
- Steps:
  1. Call `Insert MappView Widget    ${widget_type}    ${widget_id}    ${widget_id}`
  2. Verify element exists (assert no error)
  3. Hover over widget (trigger tooltip)
  4. Select widget (click)
  5. Verify properties panel shows widget properties
  6. Log success

**`Hover Over Widget`** / **`Select Widget`**
```robot
[Arguments]    ${widget_id}
```
- Interaction helpers
- Hover: Move mouse to widget, wait for tooltip
- Select: Click widget, wait for properties to update

**`Widget Properties Should Be Visible`**
```robot
[Arguments]    ${widget_type}
```
- Assert properties panel shows widget properties
- Verify property grid is populated for widget_type

**`Verify Widget Count In Visualization`**
```robot
[Arguments]    ${visualization_id}    ${expected_count}
```
- Assert correct number of widgets in visualization
- Steps:
  1. Count widget elements in visualization_id
  2. Compare with expected_count
  3. Raise AssertionError if mismatch

**`Test All Widgets`**
```robot
```
- Comprehensive full widget test
- Steps:
  1. Create test visualization
  2. Read all widgets from Widgets.txt
  3. For each widget:
     - Try: `Test Single Widget    ${widget}    ${widget_${index}}`
     - Catch exceptions, log failures but continue
  4. Report summary (passed, failed, total)

### 5. Helper Keywords (Utility)

Implement support keywords used by main keywords:

- `Navigate To MappView Settings` — Navigate to MappView config area
- `Navigate To MappView Project` — Navigate to MappView project area
- `Get Widgets By Category` — Map widgets to categories
- `Strip And Filter List` — Utility to clean list from file reading
- `TEST_INDEX` — Generate unique test suffix (timestamp-based)

## Implementation Guidelines

### Style & Best Practices

1. **Documentation**:
   - Every keyword must have `[Documentation]` section
   - Include parameter descriptions
   - Include return value description

2. **Error Handling**:
   - Use `TRY-EXCEPT` blocks for operations that may fail
   - Provide context-specific error messages
   - Log failures with full details

3. **Logging**:
   - Use `Log    message    level` for execution tracing
   - Log major milestones (e.g., "Project created", "Widget inserted")
   - Log failures with actual vs. expected values

4. **Resource Management**:
   - Use `[Setup]` and `[Teardown]` in test cases to manage IDE state
   - Don't leave dialogs open or projects unsaved

5. **Naming Conventions**:
   - Keywords: Title Case with spaces (e.g., `Create New Project In Automation Studio`)
   - Variables: `${UPPERCASE_WITH_UNDERSCORE}` for constants
   - Variables: `${lowercase_with_underscore}` for locals

### Example Implementation

```robot
*** Keywords ***

Create New Project In Automation Studio
    [Documentation]    Creates a new Automation Studio project with specified CPU
    [Arguments]        ${project_name}    ${project_path}    ${cpu_type}=${CPU_TYPE}
    
    # Initialize IDE if not already running
    Initialize Automation Studio
    
    # Trigger new project dialog
    Open Menu                    File
    Click Menu Item              New Project
    
    # Configure project details
    TRY
        Type Into Dialog Field    Project Name        ${project_name}
        Set Dialog Field Value    Project Location    ${project_path}
        Select CPU In Dialog      ${cpu_type}
        Click Dialog Button       OK
        
        # Wait for project tree to populate
        Wait For Element          tree:${project_name}    timeout=10
        Log    Project created successfully: ${project_name}
    EXCEPT
        Log    Failed to create project: ${project_name}    WARN
        Take IDE Screenshot       error_create_project
        FAIL    Project creation failed
    END


Select CPU In Dialog
    [Documentation]    Internal keyword to select CPU from dropdown
    [Arguments]        ${cpu_type}
    
    # Find CPU dropdown
    Click Element              dropdown:CPU Type
    
    # Wait for options to appear
    Wait For Element           option:${cpu_type}    timeout=5s
    
    # Select CPU option
    Click Element              option:${cpu_type}
    Log    CPU selected: ${cpu_type}
```

## Verification Checklist

- [ ] All keyword stubs in four .robot files are implemented
- [ ] Each keyword calls appropriate FlaUILibrary keywords
- [ ] All keywords have documentation
- [ ] Error handling in place (TRY-EXCEPT where appropriate)
- [ ] Logging added for debugging
- [ ] Keywords follow naming conventions (Title Case)
- [ ] Resource imports correct (FlaUILibrary, config/hardware_config.robot)
- [ ] Syntax validation passes: `robot --dryrun keywords/`
- [ ] Sample keyword invocations work in test files

## Success Criteria

1. ✅ All 40+ keywords implemented across four files
2. ✅ Keywords call FlaUILibrary core keywords correctly
3. ✅ `robot --dryrun tests/` shows resolved keywords (no unresolved)
4. ✅ Keywords documented and maintainable
5. ✅ Error messages are descriptive and actionable

## Next Steps (After Completion)

Once keywords are implemented:
1. **Phase 4 Agent** will convert FlaUITests C# to .robot test cases using these keywords
2. **Phase 5** will execute tests for validation

## Related Files

- [FlaUITests/Util/](../FlaUITests/Util/) — Reference implementations
- [RobotTests/config/hardware_config.robot](../RobotTests/config/hardware_config.robot) — Runtime variables
- [RobotTests/config/Widgets.txt](../RobotTests/config/Widgets.txt) — Widget list

---

**Phase**: 3 / 5  
**Depends On**: Phase 2 (FlaUILibrary)  
**Blocks**: Phase 4 (Test Conversion)  
**Estimated Effort**: 30-40 hours
