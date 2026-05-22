*** Settings ***
Documentation       Test cases for AutomationRuntime component configuration.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
*** Test Cases ***

Initialise Automation Runtime Version
    [Documentation]    Scenario: Initialize Automation Runtime version
    ...                Traceability ID: FW-AR-B1
    ...                Component: Automation Runtime
    ...                Source Manual: 
    ...                Source Section: 
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: 
    [Tags]              automationruntime    configuration    smoke    trace:fw-ar-b1
    Initialize Automation Studio
    Select Working Version for Component    Automation Runtime    ${AR_VERSION}

    Log    Automation Runtime version initialised