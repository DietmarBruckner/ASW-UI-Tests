*** Settings ***
Documentation       Test cases for Automation Studio project creation and initialization.
Resource            ${CURDIR}/../../keywords/project_keywords.robot
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Create Project With OPCUA Component
    [Documentation]    Scenario: Create project and initialize OPC UA component
    ...                Traceability ID: FW-CORE-A1
    ...                Component: Automation Studio Core
    ...                Source Manual: agents/TM210-ENG_Working_with_Automation_Studio_V4003_AS60.md
    ...                Source Section: 3 My first project; 7.2 Project installation
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: New project wizard opens successfully
    [Tags]              project-creation    smoke    trace:fw-core-a1    trace:tm210    trace:sec-3    trace:sec-7.2
    ${project_name}=    Set Variable    Project_OPCUA
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize OPCUA Component
    Build Project
    Directory Should Exist    ${project_path}
    Log    Project with OPCUA created successfully: ${project_name}


Create Project With MappView Visualization
    [Documentation]    Scenario: Create project with mappView visualization scaffold
    ...                Traceability ID: FW-CORE-A2
    ...                Component: Automation Studio Core
    ...                Source Manual: agents/TM210-ENG_Working_with_Automation_Studio_V4003_AS60.md
    ...                Source Section: 3 My first project; 4.3 The workspace
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappView package is available in component list
    [Tags]              project-creation    mappview    trace:fw-core-a2    trace:tm210    trace:sec-3    trace:sec-4.3
    ${project_name}=    Set Variable    Project_MappView
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    TestVisualization
    Build Project
    Directory Should Exist    ${project_path}
    Log    Project with MappView created successfully: ${project_name}


Create Project With Full Component Stack
    [Documentation]    Scenario: Create project with full first-wave component stack
    ...                Traceability ID: FW-CORE-A3
    ...                Component: Automation Studio Core
    ...                Source Manual: agents/TM210-ENG_Working_with_Automation_Studio_V4003_AS60.md
    ...                Source Section: 3 My first project; 6 Configure the hardware; 7.2 Project installation
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: CPU and all components are installable
    [Tags]              project-creation    integration    full-stack    trace:fw-core-a3    trace:tm210    trace:sec-3    trace:sec-6    trace:sec-7.2
    ${project_name}=    Set Variable    Project_Full
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}    ${CPU_TYPE}
    Initialize Full Component Stack
    Create Visualization Project    Full_Stack_Visu
    Build Project
    Directory Should Exist    ${project_path}
    Log    Comprehensive project created successfully: ${project_name}
    
    # Cleanup
    [Teardown]    Close Automation Studio    save_changes=False


Load And Modify Existing Project
    [Documentation]    Scenario: Re-open existing project and extend component set
    ...                Traceability ID: FW-CORE-A4
    ...                Component: Automation Studio Core
    ...                Source Manual: agents/TM210-ENG_Working_with_Automation_Studio_V4003_AS60.md
    ...                Source Section: 4.3 The workspace; 5.3 Organizing configurations
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Project path remains available after close
    [Tags]              project-creation    load    modify    trace:fw-core-a4    trace:tm210    trace:sec-4.3    trace:sec-5.3
    ${project_name}=    Set Variable    Project_Load
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Close Automation Studio    save_changes=True
    Load Existing Project    ${project_path}
    Switch To Configuration View
    Initialize OPCUA Component
    Build Project
    Log    Project loaded and modified successfully: ${project_name}
