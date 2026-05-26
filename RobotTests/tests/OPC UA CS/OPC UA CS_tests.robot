*** Settings ***
Documentation       Test cases for OPCUA component configuration.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
#Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise OPCUA Version
    [Documentation]    Scenario: Initialize OPC UA CS version
    ...                Traceability ID: FW-OPCUA-C1
    ...                Component: OPC UA CS
    ...                Source Manual: 
    ...                Source Section: 
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: OPC UA CS TP available
    [Tags]             opcua    configuration    smoke    trace:fw-opcua-c1
    Initialize Automation Studio
    Select Working Version for Component    OPC     ${UACS_VERSION}
    Build Project
    Log    OPC UA CS version initialised

Configure OPCUA Custom Port
    [Documentation]    Scenario: Configure OPC UA custom port and verify property
    ...                Traceability ID: FW-OPCUA-C2
    ...                Component: OPC UA CS
    ...                Source Manual: agents/TM980-ENG_OPC_UA_basics_ans_use_V3000_AS4C.md
    ...                Source Section: 5.1 Configuration of the default view
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: OPC UA property pane is editable
    [Tags]              opcua    configuration    trace:fw-opcua-c2    trace:tm980    trace:sec-5.1
    ${project_name}=    Set Variable    OPCUA_Port
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize OPCUA Component    4841
    ${port}=    Get IDE Property    Port
    Should Be Equal As Strings    ${port}    4841
    Log    OPCUA custom port configured successfully


OPCUA With MappView Integration
    [Documentation]    Scenario: Combine OPC UA server setup with mappView visualization
    ...                Traceability ID: FW-OPCUA-C3
    ...                Component: OPC UA CS
    ...                Source Manual: agents/TM980-ENG_OPC_UA_basics_ans_use_V3000_AS4C.md
    ...                Source Section: 5 Automation Runtime OPC UA server; 3.3 Reading data
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappView and OPC UA packages are available
    [Tags]              opcua    integration    mappview    trace:fw-opcua-c3    trace:tm980    trace:sec-5    trace:sec-3.3
    ${project_name}=    Set Variable    OPCUA_MappView
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize OPCUA Component
    Initialize MappView Component
    Create Visualization Project    OpcuaVisu
    Build Project
    Log    OPCUA and MappView integration project built successfully
