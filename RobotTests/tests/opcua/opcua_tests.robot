*** Settings ***
Documentation       Test cases for OPCUA component configuration.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise OPCUA Component With Default Port
    [Documentation]    Creates a project, adds OPCUA on the default port, and builds.
    [Tags]              opcua    configuration    smoke
    ${project_name}=    Set Variable    OPCUA_Init
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize OPCUA Component    ${OPCUA_PORT}
    Build Project
    Log    OPCUA component initialised on port ${OPCUA_PORT}


Configure OPCUA Custom Port
    [Documentation]    Creates a project and configures OPCUA on a non-default port.
    [Tags]              opcua    configuration
    ${project_name}=    Set Variable    OPCUA_Port
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize OPCUA Component    4841
    ${port}=    Get IDE Property    Port
    Should Be Equal As Strings    ${port}    4841
    Log    OPCUA custom port configured successfully


OPCUA With MappView Integration
    [Documentation]    Creates a project with both OPCUA and MappView components.
    [Tags]              opcua    integration    mappview
    ${project_name}=    Set Variable    OPCUA_MappView
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize OPCUA Component
    Initialize MappView Component
    Create Visualization Project    OpcuaVisu
    Build Project
    Log    OPCUA and MappView integration project built successfully
