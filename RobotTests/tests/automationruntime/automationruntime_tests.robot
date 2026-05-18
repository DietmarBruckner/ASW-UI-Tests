*** Settings ***
Documentation       Test cases for AutomationRuntime component configuration.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise AutomationRuntime Component
    [Documentation]    Creates a project, adds AutomationRuntime, and builds.
    [Tags]              automationruntime    configuration    smoke
    ${project_name}=    Set Variable    Runtime_Init
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize AutomationRuntime Component    ${AUTOMATIONRUNTIME_MIN_VERSION}
    Build Project
    Log    AutomationRuntime component initialised (v${AUTOMATIONRUNTIME_MIN_VERSION})


AutomationRuntime With MappView
    [Documentation]    Creates a project with AutomationRuntime and a MappView visualization.
    [Tags]              automationruntime    integration    mappview
    ${project_name}=    Set Variable    Runtime_MappView
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize AutomationRuntime Component
    Initialize MappView Component
    Create Visualization Project    RuntimeVisu
    Build Project
    Log    AutomationRuntime and MappView integration project built successfully
