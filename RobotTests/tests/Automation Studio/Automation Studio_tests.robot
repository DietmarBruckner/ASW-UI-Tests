*** Settings ***
Documentation       Test cases for Automation Studio component configuration.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Suite Teardown      FlaUILib.Check App Alive

*** Test Cases ***

Initialise Automation Studio And Create New Project
    [Documentation]    Scenario: Initialize Automation Studio and create a new project
    ...                Traceability ID: FW-AS-B1
    ...                Component: Automation Studio
    ...                Source Manual: agents/TM213TRE.462-ENG_Automation_Runtime_V3003.md
    ...                Source Section: 3 Installation and commissioning
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: 
    [Tags]              automationstudio    configuration    smoke    trace:fw-as-b1    trace:tm213    trace:sec-3
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${PROJECT_NAME}
    Create New Project In Automation Studio    ${PROJECT_NAME}    ${project_path}
    Activate Simulation Mode

    Log    Automation Studio and new project initialised
