---
description: "Phase 4: Convert FlaUITests C# to Robot Framework test cases"
applyTo: "RobotTests/tests/**"
---

# Agent Instruction: Convert Test Cases to Robot Framework (Phase 4)

## Task Summary

Convert all test scenarios from FlaUITests C# project into Robot Framework .robot test files. This uses the keywords implemented in Phase 3 to create executable test cases.

## Inputs & Context

### Dependencies: Phases 2 & 3 Completion
- ✅ **FlaUILibrary** (Phase 2) — Available as Robot keywords
- ✅ **Robot Keywords** (Phase 3) — Implemented in RobotTests/keywords/

### Source Code Reference

#### C# Tests to Convert

| C# File | Test Purpose | Target .robot File |
|---------|--------------|-------------------|
| [Program.cs](../FlaUITests/Program.cs) | Project creation with full component stack | [project_creation.robot](../RobotTests/tests/project_creation/project_creation.robot) |
| [MappView.cs](../FlaUITests/Util/MappView.cs) | MappView setup, visualization, widgets | [mappview_tests.robot](../RobotTests/tests/mappview/mappview_tests.robot) |
| [AutomationStudio6.cs](../FlaUITests/AutomationStudio6.cs) | IDE initialization patterns | [project_creation.robot](../RobotTests/tests/project_creation/project_creation.robot) |
| [CalculatorTests.cs](../FlaUITests/CalculatorTests.cs) | Placeholder (if content exists) | [integration_tests.robot](../RobotTests/tests/integration/integration_tests.robot) (optional) |

#### Configuration Reference
- [FlaUITests/config/Widgets.txt](../FlaUITests/config/Widgets.txt) → [RobotTests/config/Widgets.txt](../RobotTests/config/Widgets.txt) — Widget list for parametrized tests
- [RobotTests/config/hardware_config.robot](../RobotTests/config/hardware_config.robot) — Runtime variables

#### Existing Test Stubs
All five test files have **skeleton test cases** (with [Tags] and comments):
- [project_creation.robot](../RobotTests/tests/project_creation/project_creation.robot) — Project tests (skeleton)
- [mappview_tests.robot](../RobotTests/tests/mappview/mappview_tests.robot) — MappView tests (skeleton)
- [opcua_tests.robot](../RobotTests/tests/opcua/opcua_tests.robot) — OPCUA tests (skeleton)
- [automationruntime_tests.robot](../RobotTests/tests/automationruntime/automationruntime_tests.robot) — Runtime tests (skeleton)
- [integration_tests.robot](../RobotTests/tests/integration/integration_tests.robot) — Integration tests (skeleton)

**Task**: Replace skeleton test cases with full implementations.

## Deliverables

### 1. [project_creation.robot](../RobotTests/tests/project_creation/project_creation.robot)

**Source**: [Program.cs](../FlaUITests/Program.cs) + [AutomationStudio6.cs](../FlaUITests/AutomationStudio6.cs)

#### Test Cases to Implement

**`Create Project With OPCUA Component`**
- **Purpose**: Basic project creation with OPCUA component
- **C# Logic** (from Program.cs):
  ```csharp
  var project = new Project("Project_OPCUA");
  project.CreateProject(CPU_TYPE);
  var opcua = new OPCUACS();
  opcua.InitComponent();
  opcua.InsertComponent();
  ```
- **Robot Implementation**:
  ```robot
  [Tags]    project-creation    smoke
  Initialize Automation Studio
  ${project_name}=    Set Variable    Project_OPCUA_${TEST_INDEX}
  ${project_path}=    Evaluate    "${PROJECT_TEMP_PATH}${project_name}"
  Create New Project In Automation Studio    ${project_name}    ${project_path}
  Initialize OPCUA Component
  Build And Transfer Project
  Should Exist    ${project_path}
  ```

**`Create Project With MappView Visualization`**
- **Purpose**: Project with MappView component and basic visualization
- **C# Logic** (from MappView.cs):
  ```csharp
  var mappView = new MappViewComponent();
  mappView.InitComponent();
  mappView.InsertComponent();
  // Create visualization project (vis_0.vis → Test_Visu)
  ```
- **Robot Implementation**:
  ```robot
  [Tags]    project-creation    mappview
  Initialize Automation Studio
  ${project_name}=    Set Variable    Project_MappView_${TEST_INDEX}
  ${project_path}=    Evaluate    "${PROJECT_TEMP_PATH}${project_name}"
  Create New Project In Automation Studio    ${project_name}    ${project_path}
  Initialize MappView Component
  Create Visualization Project
  Build And Transfer Project
  Should Exist    ${project_path}
  ```

**`Create Project With Full Component Stack`**
- **Purpose**: Comprehensive project with all components (OPCUA, MappView, AutomationRuntime)
- **C# Logic** (from Program.cs main flow):
  - Create project
  - Initialize all components (OPCUA, MappView, AutomationRuntime)
  - Create visualization
  - Build and transfer
- **Robot Implementation**:
  ```robot
  [Tags]    project-creation    integration    full-stack    smoke
  [Setup]    Initialize Automation Studio
  [Teardown]    Close Automation Studio
  ${project_name}=    Set Variable    Project_Full_${TEST_INDEX}
  ${project_path}=    Evaluate    "${PROJECT_TEMP_PATH}${project_name}"
  Create New Project In Automation Studio    ${project_name}    ${project_path}    ${CPU_TYPE}
  Initialize OPCUA Component
  Initialize MappView Component
  Initialize AutomationRuntime Component
  Create Visualization Project    Full_Stack_Visu
  Build And Transfer Project
  Should Exist    ${project_path}
  ```

**`Load And Modify Existing Project`**
- **Purpose**: Load existing project, make modifications, rebuild
- **C# Logic** (from Project.cs LoadProject):
  - Load project from disk
  - Make modification (e.g., add component)
  - Save changes
- **Robot Implementation**:
  ```robot
  [Tags]    project-creation    load    modify
  Initialize Automation Studio
  ${project_name}=    Set Variable    Project_Load_${TEST_INDEX}
  ${project_path}=    Evaluate    "${PROJECT_TEMP_PATH}${project_name}"
  # Create initial project
  Create New Project In Automation Studio    ${project_name}    ${project_path}
  Close Automation Studio    save_changes=True
  # Reopen and modify
  Initialize Automation Studio
  Load Existing Project    ${project_path}
  Switch To Configuration View
  Initialize OPCUA Component
  Build And Transfer Project
  Close Automation Studio
  ```

**Additional Test Cases** (from AutomationStudio6.cs patterns):
- `Create Project With Multiple Visualizations` — Create project with multiple MappView visualizations
- `Project Creation With Different CPU Types` — Test with various CPU models (X20CP1684, X20CP1586, etc.)
- `Project Creation With Error Handling` — Test error cases (invalid path, duplicate name, etc.)

### 2. [mappview_tests.robot](../RobotTests/tests/mappview/mappview_tests.robot)

**Source**: [MappView.cs](../FlaUITests/Util/MappView.cs) (150+ lines)

#### Test Cases to Implement

**`Configure MappView Server With HTTP Protocol`**
- **Purpose**: Test MappView protocol configuration
- **C# Logic** (from MappView.cs InitComponent):
  ```csharp
  // Configure mapp View server with HTTP protocol
  mappView.Protocol = "HTTP";
  mappView.Port = 8080;
  ```
- **Robot Implementation**:
  ```robot
  [Tags]    mappview    configuration    smoke
  Initialize Automation Studio
  ${project_name}=    Set Variable    MappView_HTTP_${TEST_INDEX}
  ${project_path}=    Evaluate    "${PROJECT_TEMP_PATH}${project_name}"
  Create New Project In Automation Studio    ${project_name}    ${project_path}
  Initialize MappView Component    protocol=HTTP    port=${MAPPVIEW_HTTP_PORT}
  # Verify configuration persisted
  Log    MappView configured with HTTP on port ${MAPPVIEW_HTTP_PORT}
  Close Automation Studio
  ```

**`Create Visualization Project`**
- **Purpose**: Create MappView visualization (vis_0.vis → Test_Visu)
- **C# Logic** (from MappView.cs):
  ```csharp
  // Creates visualization projects
  // Manages LocalizableTexts.tmx
  ```
- **Robot Implementation**:
  ```robot
  [Tags]    mappview    visualization
  Initialize Automation Studio
  ${project_name}=    Set Variable    MappView_Visu_${TEST_INDEX}
  Create New Project In Automation Studio    ${project_name}    ${PROJECT_TEMP_PATH}${project_name}
  Initialize MappView Component
  Create Visualization Project    TestVisualization
  Close Automation Studio
  ```

**`Insert All Widget Types`**
- **Purpose**: Comprehensive widget insertion test (85+ widgets from Widgets.txt)
- **C# Logic** (from MappView.cs widget insertion loop):
  - Iterate through all widget types
  - Insert each widget into visualization
  - Verify insertion success
- **Robot Implementation**:
  ```robot
  [Tags]    mappview    widgets    comprehensive    regression
  Initialize Automation Studio
  ${project_name}=    Set Variable    MappView_AllWidgets_${TEST_INDEX}
  Create New Project In Automation Studio    ${project_name}    ${PROJECT_TEMP_PATH}${project_name}
  Initialize MappView Component
  Create Visualization Project    AllWidgetsVisu
  Insert All Widget Types    # Inserts all 85+ widgets
  Build And Transfer Project
  Close Automation Studio
  ```

**`Test Widget Category [Category]`**
- **Purpose**: Test widgets by category (Buttons, Charts, Data Input, etc.)
- **C# Logic** (from MappViewObjects.cs categories):
  - Extract widgets by category
  - Test each widget in category
- **Robot Implementation** (parametrized):
  ```robot
  Test Widget Category Buttons
      [Tags]    mappview    widgets    button-widgets
      Initialize Automation Studio
      Create New Project In Automation Studio    ...
      Initialize MappView Component
      Create Visualization Project    ButtonWidgetVisu
      Test Widget Category    Buttons
      Close Automation Studio
  
  Test Widget Category Charts
      [Tags]    mappview    widgets    chart-widgets
      [Similar structure for Charts category]
  
  # Add more category tests for: DataInput, Visualization, Layout, etc.
  ```

**`Configure Widget Properties`**
- **Purpose**: Test widget property configuration
- **C# Logic** (from MappView.cs property setting):
  ```csharp
  // Sets widget properties (Name, Type, Color, etc.)
  ```
- **Robot Implementation**:
  ```robot
  [Tags]    mappview    widgets    properties
  Initialize Automation Studio
  ${project_name}=    Set Variable    MappView_Props_${TEST_INDEX}
  Create New Project In Automation Studio    ${project_name}    ${PROJECT_TEMP_PATH}${project_name}
  Initialize MappView Component
  Create Visualization Project    PropertyTestVisu
  Insert MappView Widget    Button    TestButton    BTN_001
  Configure Widget Property    TestButton    Text    "Click Me"
  Configure Widget Property    TestButton    BackgroundColor    "0xFF0000"
  Close Automation Studio
  ```

**Additional Test Cases**:
- `Manage Localization In Visualization` — Test LocalizableTexts.tmx handling
- `Configure MappView HTTPS Protocol` — Test HTTPS instead of HTTP
- `Create Complex Visualization Layout` — Test complex layouts with multiple widget types

### 3. [opcua_tests.robot](../RobotTests/tests/opcua/opcua_tests.robot)

**Source**: [OPCUACS.cs](../FlaUITests/Util/OPCUACS.cs)

#### Test Cases to Implement

**`Initialize OPCUA Component`**
- **Purpose**: Basic OPCUA setup
- **Robot**:
  ```robot
  [Tags]    opcua    configuration    smoke
  Initialize Automation Studio
  ${project_name}=    Set Variable    OPCUA_Init_${TEST_INDEX}
  Create New Project In Automation Studio    ${project_name}    ${PROJECT_TEMP_PATH}${project_name}
  Initialize OPCUA Component    port=${OPCUA_PORT}
  Build And Transfer Project
  Close Automation Studio
  ```

**`Configure OPCUA Port`**
- Test port configuration (default 4840)

**`OPCUA With MappView Integration`**
- Test OPCUA and MappView working together
- Create project with both components

### 4. [automationruntime_tests.robot](../RobotTests/tests/automationruntime/automationruntime_tests.robot)

**Source**: [AutomationRuntime.cs](../FlaUITests/Util/AutomationRuntime.cs)

#### Test Cases to Implement

**`Initialize AutomationRuntime Component`**
- **Purpose**: Basic AutomationRuntime 6.5+ setup
- **Robot**:
  ```robot
  [Tags]    automationruntime    configuration    smoke
  Initialize Automation Studio
  ${project_name}=    Set Variable    Runtime_Init_${TEST_INDEX}
  Create New Project In Automation Studio    ${project_name}    ${PROJECT_TEMP_PATH}${project_name}
  Initialize AutomationRuntime Component    min_version=${AUTOMATIONRUNTIME_MIN_VERSION}
  Build And Transfer Project
  Close Automation Studio
  ```

**`AutomationRuntime With MappView`**
- Test AutomationRuntime and MappView together

### 5. [integration_tests.robot](../RobotTests/tests/integration/integration_tests.robot)

**Source**: [Program.cs](../FlaUITests/Program.cs) main flow + cross-component scenarios

#### Test Cases to Implement

**`Full Stack Project With All Components`**
- **Purpose**: Comprehensive multi-component test
- **Robot**:
  ```robot
  [Tags]    integration    full-stack    smoke
  Initialize Automation Studio
  ${project_name}=    Set Variable    FullStack_All_${TEST_INDEX}
  Create New Project In Automation Studio    ${project_name}    ${PROJECT_TEMP_PATH}${project_name}    ${CPU_TYPE}
  Initialize OPCUA Component
  Initialize MappView Component
  Initialize AutomationRuntime Component
  Create Visualization Project    FullStackVisu
  Insert All Widget Types
  Build And Transfer Project
  Close Automation Studio
  ```

**`Complete Workflow Build Transfer Deploy`**
- Test full workflow: create → configure → build → transfer

**`Multiple Projects Lifecycle`**
- Create multiple projects, verify all created, then cleanup

## Implementation Guidelines

### Test Case Structure

Every test case should follow this pattern:

```robot
Test Case Name
    [Documentation]    Brief description of what is tested
    [Tags]            tag1    tag2    tag3
    [Setup]           Optional setup keyword
    [Teardown]        Optional teardown keyword
    
    # Arrange (setup test data/state)
    Initialize Automation Studio
    ${project_name}=    Set Variable    TestProject_${TEST_INDEX}
    
    # Act (perform test actions)
    Create New Project In Automation Studio    ${project_name}    ${PROJECT_TEMP_PATH}${project_name}
    Initialize MappView Component
    
    # Assert (verify results)
    Build And Transfer Project
    Should Exist    ${PROJECT_TEMP_PATH}${project_name}
    
    # Cleanup
    [Teardown]    Close Automation Studio    save_changes=False
```

### Best Practices

1. **Isolation**: Each test case should be independent (can run in any order)
2. **Setup/Teardown**: Use `[Setup]` and `[Teardown]` to manage IDE state
3. **Unique identifiers**: Use `${TEST_INDEX}` (timestamp) to generate unique project names
4. **Assertions**: Include assertions to verify expected behavior
5. **Documentation**: Document test purpose and assertions
6. **Tags**: Use tags for filtering (smoke, regression, component, etc.)
7. **Error scenarios** (optional): Include negative test cases (invalid inputs, missing paths, etc.)

### Example: Full Test Implementation

```robot
*** Settings ***
Documentation       Test cases for Automation Studio project creation
Library             FlaUILibrary
Resource            ../../keywords/project_keywords.robot
Resource            ../../keywords/ide_keywords.robot
Resource            ../../config/hardware_config.robot


*** Test Cases ***

Create Project With Full Component Stack
    [Documentation]    Creates comprehensive project with all components enabled
    [Tags]              project-creation    integration    full-stack    smoke
    
    # Arrange
    Initialize Automation Studio
    ${project_name}=    Set Variable    Project_Full_${TEST_INDEX}
    ${project_path}=    Evaluate    "${PROJECT_TEMP_PATH}${project_name}"
    
    # Act
    Create New Project In Automation Studio    ${project_name}    ${project_path}    ${CPU_TYPE}
    Initialize OPCUA Component
    Initialize MappView Component
    Initialize AutomationRuntime Component
    Create Visualization Project    Full_Stack_Visu
    Insert All Widget Types
    Build And Transfer Project
    
    # Assert
    Should Exist    ${project_path}
    Log    Full stack project created successfully: ${project_name}
    
    # Cleanup
    [Teardown]    Close Automation Studio    save_changes=False


*** Keywords ***

TEST_INDEX
    [Documentation]    Generate unique test index based on timestamp
    ${timestamp}=    Get Time    epoch
    [Return]    ${timestamp}
```

## Verification Checklist

- [ ] All five test files have 15+ test cases total
- [ ] Each test case has [Documentation] and [Tags]
- [ ] Tests follow Arrange-Act-Assert pattern
- [ ] Setup/Teardown properly manages IDE state
- [ ] Keywords from Phase 3 are used correctly
- [ ] Assertions verify expected behavior
- [ ] Error handling in keywords (not in tests)
- [ ] Syntax validation passes: `robot --dryrun tests/`
- [ ] Tags enable filtering: `robot --include smoke tests/`

## Success Criteria

1. ✅ All test scenarios from FlaUITests converted to .robot files
2. ✅ `robot --dryrun tests/` shows all tests parse correctly
3. ✅ `robot --list tests/` shows 15+ executable test cases
4. ✅ Tests tagged appropriately for filtering
5. ✅ Tests are independent and can run in any order
6. ✅ Example test execution (manual or dry-run) validates keyword calls

## Next Steps (After Completion)

Once tests are converted:
1. **Phase 5** — Execute full test suite against real Automation Studio 6 instance
2. **Verification** — Cross-validate Robot results with original FlaUITests results
3. **Migration** — Optionally archive FlaUITests or keep as reference

## Related Files

- [FlaUITests/](../FlaUITests/) — Reference implementations
- [RobotTests/keywords/](../RobotTests/keywords/) — Phase 3 keywords
- [RobotTests/config/hardware_config.robot](../RobotTests/config/hardware_config.robot) — Test variables

---

**Phase**: 4 / 5  
**Depends On**: Phases 2 & 3  
**Blocks**: Phase 5 (Verification)  
**Estimated Effort**: 20-30 hours
