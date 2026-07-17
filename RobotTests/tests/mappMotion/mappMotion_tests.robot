*** Settings ***
Documentation       First-wave test cases for mappMotion component configuration.
Resource            ${CURDIR}/../../keywords/project_keywords.robot
Resource            ${CURDIR}/../../keywords/motion_keywords.robot
Resource            ${CURDIR}/../../config/mappMotion/axis_profiles.robot
#Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise mappMotion Component
    [Documentation]    Scenario: Initialize mappMotion component in a clean project
    ...                Traceability ID: FW-MOTION-E0
    ...                Component: mappMotion
    ...                Source Manual: agents/TM415TRE.492-ENG_Introduction_to_mapp_Axis_V3004.md
    ...                Source Section: 4.1 Configuring the single axis
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Automation Studio starts and project wizard is available
    [Tags]              mappmotion    configuration    smoke    trace:fw-motion-e0    trace:tm415    trace:sec-4.1
    #${project_name}=    Set Variable    Motion_Init
    #${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    #Create New Project In Automation Studio    ${project_name}    ${project_path}
    #Initialize MappMotion Component
    #Build Project
    #Log    mappMotion component initialized and project built

    Initialize Automation Studio
    Select Working Version for Component    mapp Motion     ${DEFAULT_MOT_VERSION}
    Build Project
    Log    mappMotion version initialised


Add Axis And Configure Basic Parameters
    [Documentation]    Scenario: Add axis and set base drive parameters
    ...                Traceability ID: FW-MOTION-E1
    ...                Component: mappMotion
    ...                Source Manual: agents/TM415TRE.492-ENG_Introduction_to_mapp_Axis_V3004.md
    ...                Source Section: 4.1 Configuring the single axis; 6.2 Drive parameters and homing
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappMotion component is available in toolbox
    [Tags]              mappmotion    axis    commissioning    trace:fw-motion-e1    trace:tm415    trace:sec-4.1    trace:sec-6.2
    ${project_name}=    Set Variable    Motion_Axis_Config
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappMotion Component
    Add Axis Component    ${AXIS_DEFAULT_NAME}
    Configure Axis Parameters    ${AXIS_NOMINAL_SPEED}    ${AXIS_GEAR_RATIO}    ${AXIS_HOMING_METHOD}
    ${configured_speed}=    Get IDE Property    Nominal Speed
    Should Be Equal As Strings    ${configured_speed}    ${AXIS_NOMINAL_SPEED}
    Run Axis Commissioning Smoke
    Log    Axis configuration smoke completed


Commission Axis With Drive Mapping
    [Documentation]    Scenario: Commission axis with drive mapping and homing profile
    ...                Traceability ID: FW-MOTION-E2
    ...                Component: mappMotion
    ...                Source Manual: agents/TM415TRE.492-ENG_Introduction_to_mapp_Axis_V3004.md
    ...                Source Section: 4.1 Configuring the single axis; 6.2 Drive parameters and homing; 6.3 Diagnostics
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Axis can be added and drive reference is configurable
    [Tags]              mappmotion    axis    commissioning    smoke    trace:fw-motion-e2    trace:tm415    trace:sec-4.1    trace:sec-6.2    trace:sec-6.3
    ${project_name}=    Set Variable    Motion_Commission
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappMotion Component
    Commission Axis End To End    ${AXIS_DEFAULT_NAME}    ${AXIS_DRIVE_REFERENCE}
    ${configured_drive}=    Get IDE Property    Drive Reference
    Should Be Equal As Strings    ${configured_drive}    ${AXIS_DRIVE_REFERENCE}
    Log    Axis commissioning workflow completed


Run Axis Autotuning Workflow
    [Documentation]    Scenario: Trigger axis autotuning from diagnostics context
    ...                Traceability ID: FW-MOTION-E3
    ...                Component: mappMotion
    ...                Source Manual: agents/TM415TRE.492-ENG_Introduction_to_mapp_Axis_V3004.md
    ...                Source Section: 6.4 Determine controller settings using autotuning
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Axis exists and is selectable in diagnostics
    [Tags]              mappmotion    diagnostics    autotuning    trace:fw-motion-e3    trace:tm415    trace:sec-6.4
    ${project_name}=    Set Variable    Motion_Autotune
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappMotion Component
    Add Axis Component    ${AXIS_DEFAULT_NAME}
    Run Axis Autotuning Session
    Wait Until IDE Is Ready
    Log    Axis autotuning workflow triggered
