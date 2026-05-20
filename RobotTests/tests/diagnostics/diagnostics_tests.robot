*** Settings ***
Documentation       First-wave test cases for diagnostics workflows.
Resource            ${CURDIR}/../../keywords/project_keywords.robot
Resource            ${CURDIR}/../../keywords/diagnostics_keywords.robot
Resource            ${CURDIR}/../../config/hardware_config.robot
Resource            ${CURDIR}/../../config/Diagnostics/targets.robot
Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Open SDM Session From IDE
    [Documentation]    Scenario: Open System Diagnostics Manager from IDE
    ...                Traceability ID: FW-DIAG-G0
    ...                Component: Diagnostics
    ...                Source Manual: agents/TM920TRE.001-ENG_Diagnostics_and_service_V2001.md
    ...                Source Section: 6.1 Diagnostics using the System Diagnostics Manager
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: IDE is running and tools menu is accessible
    [Tags]              diagnostics    sdm    smoke    trace:fw-diag-g0    trace:tm920    trace:sec-6.1
    ${project_name}=    Set Variable    Diagnostics_SDM
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Open SDM Session
    Wait Until IDE Is Ready
    Log    SDM session opened


Record Diagnostics Logger Data
    [Documentation]    Scenario: Record and stop diagnostics logger capture
    ...                Traceability ID: FW-DIAG-G1
    ...                Component: Diagnostics
    ...                Source Manual: agents/TM920TRE.001-ENG_Diagnostics_and_service_V2001.md
    ...                Source Section: 6.3 Saving Logger files
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: SDM session can be opened
    [Tags]              diagnostics    logger    trace:fw-diag-g1    trace:tm920    trace:sec-6.3
    ${project_name}=    Set Variable    Diagnostics_Logger
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Open SDM Session
    Record Logger Data    smoke_capture
    Log    Diagnostics logger capture completed


Connect SDM And Capture Hardware Snapshot
    [Documentation]    Scenario: Connect SDM to target and capture hardware tree state
    ...                Traceability ID: FW-DIAG-G2
    ...                Component: Diagnostics
    ...                Source Manual: agents/TM920TRE.001-ENG_Diagnostics_and_service_V2001.md
    ...                Source Section: 6.2 Establishing a connection; 6.5 Information in the hardware tree
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Target controller IP is configured
    [Tags]              diagnostics    sdm    hardware    trace:fw-diag-g2    trace:tm920    trace:sec-6.2    trace:sec-6.5
    ${project_name}=    Set Variable    Diagnostics_Hardware
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Open SDM Session
    Connect SDM To Controller    ${DIAGNOSTICS_TARGET_IP}
    Browse Hardware Tree Snapshot
    Log    SDM connected and hardware snapshot captured


Monitor Status And Generate System Dump
    [Documentation]    Scenario: Monitor status points and trigger system dump
    ...                Traceability ID: FW-DIAG-G3
    ...                Component: Diagnostics
    ...                Source Manual: agents/TM920TRE.001-ENG_Diagnostics_and_service_V2001.md
    ...                Source Section: 6.6 Diagnostics via I/O and status points; 6.4 Generating and saving a system dump
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Active SDM connection to target controller
    [Tags]              diagnostics    monitor    dump    trace:fw-diag-g3    trace:tm920    trace:sec-6.6    trace:sec-6.4
    ${project_name}=    Set Variable    Diagnostics_Dump
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Open SDM Session
    Connect SDM To Controller    ${DIAGNOSTICS_TARGET_IP}
    Monitor IO Status Points
    Generate System Dump
    Open MappView Diagnostics Page    ${MAPPVIEW_DIAGNOSTICS_ENDPOINT}
    Should Contain    ${MAPPVIEW_DIAGNOSTICS_ENDPOINT}    /server/info
    Log    Diagnostics monitor and dump workflow completed
