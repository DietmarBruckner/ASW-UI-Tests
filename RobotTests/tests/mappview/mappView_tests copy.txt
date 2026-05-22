*** Settings ***
Documentation       Test cases for MappView component configuration and widget insertion.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_keywords.robot
Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

AutomationRuntime With MappView
    [Documentation]    Scenario: Integrate Automation Runtime with mappView visualization
    ...                Traceability ID: FW-AR-B2
    ...                Component: Automation Runtime
    ...                Source Manual: agents/TM213TRE.462-ENG_Automation_Runtime_V3003.md
    ...                Source Section: 3.3 Project installation; 5.1 Start Automation Runtime
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Runtime and mappView components can coexist in project
    [Tags]              automationruntime    integration    mappview    trace:fw-ar-b2    trace:tm213    trace:sec-3.3    trace:sec-5.1
    ${project_name}=    Set Variable    Runtime_MappView
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize AutomationRuntime Component
    Initialize MappView Component
    Create Visualization Project    RuntimeVisu
    Build Project
    Log    AutomationRuntime and MappView integration project built successfully

Configure MappView Server
    [Documentation]    Scenario: Configure mappView server and verify communication port
    ...                Traceability ID: FW-MVIEW-D1
    ...                Component: mappView
    ...                Source Manual: agents/TM611-GER_Working_with_mapp_View_V6001_mV61.md
    ...                Source Section: 3.2 Configure mappView server
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappView component is available in project
    [Tags]              mappview    configuration    smoke    trace:fw-mview-d1    trace:tm611    trace:sec-3.2
    ${project_name}=    Set Variable    MappView_HTTP
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component    ${MAPPVIEW_HTTP_PORT}
    ${port}=    Get IDE Property    Port
    Should Be Equal As Strings    ${port}    ${MAPPVIEW_HTTP_PORT}
    Log    MappView configured with port ${MAPPVIEW_HTTP_PORT}


Create Visualization Project And Open
    [Documentation]    Scenario: Create visualization project and open editor
    ...                Traceability ID: FW-MVIEW-D2
    ...                Component: mappView
    ...                Source Manual: agents/TM611-GER_Working_with_mapp_View_V6001_mV61.md
    ...                Source Section: 4 mapp View Visualisierungsvorlagen
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Visualization node is available under mappView
    [Tags]              mappview    visualization    trace:fw-mview-d2    trace:tm611    trace:sec-4
    ${project_name}=    Set Variable    MappView_Visu
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    TestVisualization
    Open Visualization For Editing    TestVisualization
    Wait Until IDE Is Ready
    Log    Visualization project created and opened successfully


Insert All Widget Types
    [Documentation]    Scenario: Insert complete widget catalog into visualization
    ...                Traceability ID: FW-MVIEW-D3
    ...                Component: mappView
    ...                Source Manual: agents/TM642-ENG_Diagnostics__charts__customized_widgets_V6000_mV65.md
    ...                Source Section: 4 Customized widgets
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Widget catalog file is loaded and editor is open
    [Tags]              mappview    widgets    comprehensive    regression    trace:fw-mview-d3    trace:tm642    trace:sec-4
    ${project_name}=    Set Variable    MappView_AllWidgets
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    AllWidgetsVisu
    Open Visualization For Editing    AllWidgetsVisu
    Insert All Widgets From Config
    Build Project
    Log    All widget types inserted and project built


Insert Button Widget And Configure Properties
    [Documentation]    Scenario: Insert button widget and set visual properties
    ...                Traceability ID: FW-MVIEW-D4
    ...                Component: mappView
    ...                Source Manual: agents/TM611-GER_Working_with_mapp_View_V6001_mV61.md
    ...                Source Section: 5.4 Content and Widgets
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Visualization content editor is open
    [Tags]              mappview    widgets    properties    trace:fw-mview-d4    trace:tm611    trace:sec-5.4
    ${project_name}=    Set Variable    MappView_Props
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    PropertyTestVisu
    Open Visualization For Editing    PropertyTestVisu
    Insert MappView Widget    Button    TestButton    BTN_001
    Configure Widget Property    Text    Click Me
    Configure Widget Property    BackgroundColor    0xFF0000
    Log    Widget properties configured successfully
