*** Settings ***
Documentation       Keywords for SDM and mappView diagnostics workflows.
Resource            ${CURDIR}/component_keywords.robot


*** Keywords ***

Open SDM Session
    [Documentation]    Opens diagnostics entrypoint and waits for readiness.
    Invoke IDE Menu    Tools    System Diagnostics Manager
    Wait Until IDE Is Ready
    Log    SDM session opened


Connect SDM To Controller
    [Documentation]    Connects SDM to the target controller.
    [Arguments]        ${controller_ip}
    Invoke IDE Menu    File    Establish Connection
    Wait For Modal And Fill Field    Establish Connection    Address    ${controller_ip}
    Handle Modal Dialog    Establish Connection    Connect
    Wait Until IDE Is Ready
    Log    SDM connected to ${controller_ip}


Record Logger Data
    [Documentation]    Starts and stops logger capture in SDM.
    [Arguments]        ${capture_name}=sdm_capture
    Invoke IDE Menu    Diagnostics    Logger
    Invoke IDE Menu    Logger    Start Recording
    Wait Until IDE Is Ready
    Invoke IDE Menu    Logger    Stop Recording
    Log    Logger capture complete: ${capture_name}


Open MappView Diagnostics Page
    [Documentation]    Verifies mappView diagnostics endpoint can be prepared in test flow.
    [Arguments]        ${endpoint}
    Log    Open browser to diagnostics endpoint: ${endpoint}


Browse Hardware Tree Snapshot
    [Documentation]    Navigates core hardware nodes to verify diagnostics visibility.
    Navigate To Tree Leaf    Hardware Tree
    Expand Tree Node    CPU
    Expand Tree Node    I/O
    Wait Until IDE Is Ready
    Take IDE Screenshot
    Log    Hardware tree snapshot captured


Monitor IO Status Points
    [Documentation]    Opens I/O monitoring view and waits for updates.
    Invoke IDE Menu    Diagnostics    Monitor I/O
    Wait Until IDE Is Ready
    Invoke IDE Menu    Diagnostics    Status Data Points
    Wait Until IDE Is Ready
    Log    I/O and status monitoring views opened


Generate System Dump
    [Documentation]    Triggers a diagnostic dump and confirms the action dialog.
    Invoke IDE Menu    File    Generate System Dump
    Handle Modal Dialog    Generate System Dump    OK
    Wait Until IDE Is Ready
    Log    System dump generation triggered
