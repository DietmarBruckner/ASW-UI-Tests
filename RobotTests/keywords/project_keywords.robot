*** Settings ***
Documentation       Keywords for Automation Studio project creation and management.
...                 All IDE interactions use FlaUILib (via ide_keywords.robot).
Library             OperatingSystem
Resource            ${CURDIR}/ide_keywords.robot


*** Keywords ***

Create New Project In Automation Studio
    [Documentation]    Creates a new Automation Studio project.
    ...                Opens File > New Project, fills in name and location, selects CPU, confirms.
    [Arguments]        ${project_name}    ${project_path}    ${config_name}=${PROJECT_DEFAULT_CONFIG_NAME}    ${cpu_type}=${CPU_TYPE}    ${working_version}=${AS_WORKING_VERSION}
    Initialize Automation Studio
    Invoke IDE Menu                    File    New Project...
    FlaUILib.Wait For Dialog           New Project
    FlaUILib.Type Into Dialog Field    projectNameTextBox         ${project_name}
    FlaUILib.Set Dialog Field Value    pathTextBox                ${project_path}
    Select Working Version In New Project Dialog                  ${working_version}
    FlaUILib.Click Dialog Button       Next
    ${dialog_appeared}=    FlaUILib.Wait For Dialog    Automation Studio    2
    IF    ${dialog_appeared}
        Log    Create path question dialog appeared, proceeding with yes.
        FlaUILib.Click Dialog Button   Yes    dialog_close=True
    ELSE
        Log    No Create path question dialog appeared, assuming path exists and proceeding.
    END
    FlaUILib.Set Dialog Field Value    configurationNameTextBox   ${config_name}
    FlaUILib.Click Dialog Button       Next
    FlaUILib.Type Slowly Into Dialog Field   searchTermTextBox    ${cpu_type}
    FlaUILib.Click Dialog Button       Finish
    FlaUILib.Wait For Message          finished.    timeout=20
    Log                                Project "${project_name}" created at ${project_path}

Select Working Version In New Project Dialog
    [Documentation]    Selects the AS working version inside the New Project dialog.
    [Arguments]        ${working_version}
    # Navigate the working version tree/list inside the dialog and select the given working_version string
    Select From ComboBox    cbWorkingVersion     ${working_version}
    Log    Working version selected: ${working_version}

Select Working Version for Component
    [Documentation]    Selects the component's version inside the Project - Change Runtime Version dialog.
    [Arguments]        ${component_name}     ${working_version} 
    # Navigate the working version tree/list inside the dialog and select the given working_version string
    Invoke IDE Menu                    Project    Change Runtime Versions...
    ${dialog_appeared}=    FlaUILib.Wait For Dialog    ${CPU_TYPE} - Properties    1
    IF    ${dialog_appeared}
        FlaUILib.Select Component Version    ${component_name}     ${working_version}
        FlaUILib.Click Dialog Button   OK    dialog_close=True
    ELSE
        Log    No Change Runtime Versions dialog appeared.
    END
    Log    Working version selected: ${working_version}