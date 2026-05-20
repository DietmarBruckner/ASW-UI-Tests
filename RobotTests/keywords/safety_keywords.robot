*** Settings ***
Documentation       Keywords for mapp Safety and SafeDESIGNER workflows.
Resource            ${CURDIR}/component_keywords.robot


*** Keywords ***

Initialize Safety Component
    [Documentation]    Adds safety component and prepares configuration branch.
    [Arguments]        ${component_name}=mapp Safety
    Add Software Component    ${component_name}
    Switch To Configuration View
    Wait Until IDE Is Ready
    Log    Safety component initialized: ${component_name}


Create Safe Domain
    [Documentation]    Creates a safe domain container.
    [Arguments]        ${domain_name}=SafeDomain1
    Navigate To Tree Leaf    mapp Safety
    Open Context Menu For Element    mapp Safety
    Select Context Menu Item    Add SafeDOMAIN
    Wait For Modal And Fill Field    Add SafeDOMAIN    Name    ${domain_name}
    Wait Until IDE Is Ready
    Log    SafeDOMAIN created: ${domain_name}


Open SafeDesigner
    [Documentation]    Opens SafeDESIGNER from the selected domain.
    [Arguments]        ${domain_name}=SafeDomain1
    Navigate To Tree Leaf    mapp Safety/${domain_name}
    Open Context Menu For Element    ${domain_name}
    Select Context Menu Item    Open SafeDESIGNER
    Wait Until IDE Is Ready
    Log    SafeDESIGNER opened for ${domain_name}


Compile And Download Safety
    [Documentation]    Compiles and triggers download for safety project.
    Invoke IDE Menu    Project    Compile
    Wait Until IDE Is Ready
    Invoke IDE Menu    Project    Download
    Wait Until IDE Is Ready
    Log    Safety compile/download triggered


Add SafeIO Module
    [Documentation]    Adds a safe I/O module below the selected safety domain.
    [Arguments]        ${module_name}=SafeIO
    Open Context Menu For Element    SafeDomain1
    Select Context Menu Item    Add SafeIO
    Wait For Modal And Fill Field    Add SafeIO    Name    ${module_name}
    Wait Until IDE Is Ready
    Log    SafeIO module added: ${module_name}


Link SafeIO Channels
    [Documentation]    Performs a basic safe I/O link configuration in properties.
    [Arguments]        ${input_channel}=Ch1    ${output_channel}=ChO1
    Set IDE Property    Input Channel    ${input_channel}
    Set IDE Property    Output Channel    ${output_channel}
    Wait Until IDE Is Ready
    Log    SafeIO channels linked (${input_channel} -> ${output_channel})


Run Safety Commissioning Checklist
    [Documentation]    Executes compile and download plus readiness checks.
    Compile And Download Safety
    Build Project
    Wait Until IDE Is Ready
    Log    Safety commissioning checklist execution completed
