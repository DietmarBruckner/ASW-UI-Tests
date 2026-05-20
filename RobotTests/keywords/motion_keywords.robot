*** Settings ***
Documentation       Keywords for mappMotion and axis commissioning workflows.
Resource            ${CURDIR}/component_keywords.robot


*** Keywords ***

Initialize MappMotion Component
    [Documentation]    Adds a motion component to the project and opens its configuration.
    [Arguments]        ${component_name}=mapp Motion
    Add Software Component    ${component_name}
    Switch To Configuration View
    Wait Until IDE Is Ready
    Log    mappMotion component initialized: ${component_name}


Add Axis Component
    [Documentation]    Adds and selects an axis entry in the motion branch.
    [Arguments]        ${axis_name}=Axis1
    Navigate To Tree Leaf    mapp Motion
    Open Context Menu For Element    mapp Motion
    Select Context Menu Item    Add Axis
    Wait For Modal And Fill Field    Add Axis    Name    ${axis_name}
    Wait Until IDE Is Ready
    Log    Axis added: ${axis_name}


Configure Axis Parameters
    [Documentation]    Sets basic axis values in the property pane.
    [Arguments]        ${nominal_speed}=3000    ${gear_ratio}=10:1    ${homing_method}=Home to Index
    Set IDE Property    Nominal Speed    ${nominal_speed}
    Set IDE Property    Gear Ratio    ${gear_ratio}
    Set IDE Property    Homing Method    ${homing_method}
    Log    Axis parameters configured


Run Axis Commissioning Smoke
    [Documentation]    Performs a minimal build readiness check for configured axis.
    Build Project
    Log    Axis commissioning smoke completed


Map Axis To Drive Module
    [Documentation]    Links the selected axis to an available drive module reference.
    [Arguments]        ${drive_reference}=ACOPOS
    Set IDE Property    Drive Reference    ${drive_reference}
    Wait Until IDE Is Ready
    Log    Axis drive reference set to ${drive_reference}


Configure Axis Homing Profile
    [Documentation]    Applies homing-specific values before commissioning tests.
    [Arguments]        ${home_offset}=0    ${home_velocity}=100
    Set IDE Property    Home Offset    ${home_offset}
    Set IDE Property    Home Velocity    ${home_velocity}
    Wait Until IDE Is Ready
    Log    Axis homing profile configured


Run Axis Autotuning Session
    [Documentation]    Triggers axis autotuning workflow from diagnostics context.
    Invoke IDE Menu    Diagnostics    Axis Diagnostics
    Wait Until IDE Is Ready
    Open Context Menu For Element    Axis Diagnostics
    Select Context Menu Item    Autotuning
    Wait Until IDE Is Ready
    Log    Axis autotuning command triggered


Commission Axis End To End
    [Documentation]    Executes the standard axis commissioning sequence.
    [Arguments]        ${axis_name}=Axis1    ${drive_reference}=ACOPOS
    Add Axis Component    ${axis_name}
    Configure Axis Parameters
    Map Axis To Drive Module    ${drive_reference}
    Configure Axis Homing Profile
    Run Axis Commissioning Smoke
    Log    Axis commissioning sequence completed for ${axis_name}
