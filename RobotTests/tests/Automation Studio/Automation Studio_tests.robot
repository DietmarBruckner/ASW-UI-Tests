*** Settings ***
Documentation       Test cases for AutomationRuntime component configuration.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
#Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise Automation Studio And Create New Project
    [Documentation]    Scenario: Initialize Automation Studio and create a new project
    ...                Traceability ID: FW-AR-B1
    ...                Component: Automation Studio
    ...                Source Manual: agents/TM213TRE.462-ENG_Automation_Runtime_V3003.md
    ...                Source Section: 3 Installation and commissioning
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Automation Runtime package is available
    [Tags]              automationruntime    configuration    smoke    trace:fw-ar-b1    trace:tm213    trace:sec-3
    ${project_name}=    Set Variable    Basic_Project
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}

    Log    Automation Studio and new project initialised
