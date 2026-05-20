*** Settings ***
Documentation       First-wave test cases for mapp Safety workflows.
Resource            ${CURDIR}/../../keywords/project_keywords.robot
Resource            ${CURDIR}/../../keywords/safety_keywords.robot
Resource            ${CURDIR}/../../config/Safety/safeio_profiles.robot
Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise Safety Component
    [Documentation]    Scenario: Initialize mapp Safety component in a clean project
    ...                Traceability ID: FW-SAFETY-F0
    ...                Component: Safety
    ...                Source Manual: agents/TM515TRE.472-ENG_Programming_and_commissioning_mapp_Safety_V2002.md
    ...                Source Section: 4 Configuration in Automation Studio
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Safety technology package is available
    [Tags]              safety    configuration    smoke    trace:fw-safety-f0    trace:tm515    trace:sec-4
    ${project_name}=    Set Variable    Safety_Init
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize Safety Component
    Build Project
    Log    Safety component initialized and build completed


Create Safe Domain And Open SafeDESIGNER
    [Documentation]    Scenario: Create SafeDOMAIN and open SafeDESIGNER editor
    ...                Traceability ID: FW-SAFETY-F1
    ...                Component: Safety
    ...                Source Manual: agents/TM515TRE.472-ENG_Programming_and_commissioning_mapp_Safety_V2002.md
    ...                Source Section: 4 Configuration in Automation Studio; 5.1 Getting started in SafeDESIGNER
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Safety branch exists in configuration view
    [Tags]              safety    safedesigner    trace:fw-safety-f1    trace:tm515    trace:sec-4    trace:sec-5.1
    ${project_name}=    Set Variable    Safety_Domain
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize Safety Component
    Create Safe Domain    ${SAFETY_DOMAIN_NAME}
    Open SafeDesigner    ${SAFETY_DOMAIN_NAME}
    Wait Until IDE Is Ready
    Log    SafeDOMAIN created and SafeDESIGNER opened


Add SafeIO And Link Channels
    [Documentation]    Scenario: Add SafeIO module and link I/O channels
    ...                Traceability ID: FW-SAFETY-F2
    ...                Component: Safety
    ...                Source Manual: agents/TM515TRE.472-ENG_Programming_and_commissioning_mapp_Safety_V2002.md
    ...                Source Section: 4.4 Adding SafeIO modules; 5.4 Linking I/O channels
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: SafeDOMAIN exists and is selected
    [Tags]              safety    safeio    mapping    trace:fw-safety-f2    trace:tm515    trace:sec-4.4    trace:sec-5.4
    ${project_name}=    Set Variable    Safety_IO_Link
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize Safety Component
    Create Safe Domain    ${SAFETY_DOMAIN_NAME}
    Add SafeIO Module    ${SAFEIO_DEFAULT_NAME}
    Link SafeIO Channels    ${SAFEIO_INPUT_CHANNEL}    ${SAFEIO_OUTPUT_CHANNEL}
    ${configured_input}=    Get IDE Property    Input Channel
    Should Be Equal As Strings    ${configured_input}    ${SAFEIO_INPUT_CHANNEL}
    Log    SafeIO channel mapping configured


Compile Download And Commission Safety
    [Documentation]    Scenario: Compile, download, and run safety commissioning checks
    ...                Traceability ID: FW-SAFETY-F3
    ...                Component: Safety
    ...                Source Manual: agents/TM515TRE.472-ENG_Programming_and_commissioning_mapp_Safety_V2002.md
    ...                Source Section: 6.1 Compiling a project; 6.2 Download via Remote Control dialog; 6.4 Commissioning checklist
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Safety logic is configured and target is reachable
    [Tags]              safety    commissioning    smoke    trace:fw-safety-f3    trace:tm515    trace:sec-6.1    trace:sec-6.2    trace:sec-6.4
    ${project_name}=    Set Variable    Safety_Commission
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize Safety Component
    Create Safe Domain    ${SAFETY_DOMAIN_NAME}
    Run Safety Commissioning Checklist
    Wait Until IDE Is Ready
    Log    Safety compile/download and checklist run completed
