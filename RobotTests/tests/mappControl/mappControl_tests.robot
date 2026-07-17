*** Settings ***
Documentation       Test cases for MappView component configuration and widget insertion.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_property_keywords.robot
Library             FlaUILibrary    uia=UIA2

#Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise MappControl Version
    [Documentation]    Scenario: Initialize mapp Control version
    ...                Traceability ID: FW-MCT-C1
    ...                Component: mappControl
    ...                Source Manual: 
    ...                Source Section: 
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappCockpit component available
    [Tags]             mappcontrol    configuration    smoke    trace:fw-mcp-c1
    Initialize Automation Studio
    Select Working Version for Component    mapp Control     ${DEFAULT_CON_VERSION}
    #Insert mapp View with Default Template
    Build Project
    Log    mappControl version initialised


