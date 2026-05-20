*** Settings ***
Documentation       Integration test cases combining multiple components and workflows.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_keywords.robot
Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Full Stack Project With All Components
    [Documentation]    Scenario: Build full stack project with core components and widgets
    ...                Traceability ID: FW-INTEG-I1
    ...                Component: Integration
    ...                Source Manual: agents/TM210-ENG_Working_with_Automation_Studio_V4003_AS60.md
    ...                Source Section: 6 Configure the hardware; 7.2 Project installation
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Required component packages are available
    [Tags]              integration    full-stack    smoke    trace:fw-integ-i1    trace:tm210    trace:sec-6    trace:sec-7.2
    ${project_name}=    Set Variable    FullStack_All
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}    ${CPU_TYPE}
    Initialize Full Component Stack
    Create Visualization Project    FullStackVisu
    Open Visualization For Editing    FullStackVisu
    Insert All Widgets From Config
    Build Project
    Directory Should Exist    ${project_path}
    Log    Full stack project with all components built successfully


Complete Workflow Build Transfer Deploy
    [Documentation]    Scenario: Run end-to-end build and transfer deployment workflow
    ...                Traceability ID: FW-INTEG-I2
    ...                Component: Integration
    ...                Source Manual: agents/TM210-ENG_Working_with_Automation_Studio_V4003_AS60.md
    ...                Source Section: 6.6 Compiling the project; 7.2.1 Online installation
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Transfer target is reachable from engineering station
    [Tags]              integration    workflow    deployment    trace:fw-integ-i2    trace:tm210    trace:sec-6.6    trace:sec-7.2.1
    ${project_name}=    Set Variable    Workflow_Complete
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    WorkflowVisu
    Open Visualization For Editing    WorkflowVisu
    Insert MappView Widget    Button    StartButton    BTN_001
    Build And Transfer Project
    Directory Should Exist    ${project_path}
    Log    Complete workflow executed successfully


Multiple Projects Lifecycle
    [Documentation]    Scenario: Validate multi-project lifecycle create/build/delete sequence
    ...                Traceability ID: FW-INTEG-I3
    ...                Component: Integration
    ...                Source Manual: agents/TM210-ENG_Working_with_Automation_Studio_V4003_AS60.md
    ...                Source Section: 3 My first project; 4.3 The workspace
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Filesystem paths are writable and deletable
    [Tags]              integration    lifecycle    cleanup    trace:fw-integ-i3    trace:tm210    trace:sec-3    trace:sec-4.3
    @{project_names}=    Create List    Project_1    Project_2    Project_3
    FOR    ${project_name}    IN    @{project_names}
        ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
        Create New Project In Automation Studio    ${project_name}    ${project_path}
        Build Project
        Close Automation Studio    save_changes=False
        Directory Should Exist    ${project_path}
    END
    FOR    ${project_name}    IN    @{project_names}
        ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
        Delete Project    ${project_path}
    END
    Log    Multiple projects lifecycle test completed
