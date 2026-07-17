*** Settings ***
Documentation       Test cases for MappView component configuration and widget insertion.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_property_keywords.robot
Library             FlaUILibrary    uia=UIA2

#Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise MappServices Version
    [Documentation]    Scenario: Initialize mapp Services version
    ...                Traceability ID: FW-MCT-C1
    ...                Component: MappServices
    ...                Source Manual: 
    ...                Source Section: 
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappCockpit component available
    [Tags]             mappservices    configuration    smoke    trace:fw-mcp-c1
    Initialize Automation Studio
    Select Working Version for Component    mapp Services     ${DEFAULT_SERV_VERSION}
    #Insert mapp View with Default Template
    Build Project
    Log    MappServices version initialised


