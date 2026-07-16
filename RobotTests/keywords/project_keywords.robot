*** Settings ***
Documentation       Keywords for Automation Studio project creation and management.
...                 All IDE interactions use FlaUILib (via ide_keywords.robot).
Library             OperatingSystem
Library             XML
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
    ${dialog_appeared}=    FlaUILib.Wait For Dialog    ${CPU_TYPE} - Properties    5
    IF    ${dialog_appeared}
        FlaUILib.Select Component Version    ${component_name}     ${working_version}
        FlaUILib.Click Dialog Button   OK    dialog_close=True
    ELSE
        Log    No Change Runtime Versions dialog appeared.
    END
    Log    Working version selected: ${working_version}

Verify Working Version For Component
    [Documentation]                                    Verifies the component's version inside the Project - Change Runtime Version dialog.
    [Arguments]                                        ${component_name}     ${expected_working_version} 
    IF  "${component_name}" == "AutomationRuntime"
        ${XML_root}=              Parse XML                ${PROJECT_TEMP_PATH}${PROJECT_NAME}\\${PROJECT_NAME}\\Physical\\${PROJECT_DEFAULT_CONFIG_NAME}\\${DEFAULT_CPU_TYPE}\\Cpu.pkg
        ${configurations}=        Get Elements             ${XML_root}    Configuration
        FOR  ${config}    IN    @{configurations}
            ${moduleId}=          Get Element Attribute   ${config}    ModuleId
            IF  "${moduleId}" == "${DEFAULT_CPU_TYPE}"
                ${component}=     Get Element              ${config}    ${component_name}
            END
        END
    ELSE
        ${XML_root}=              Parse XML                ${PROJECT_TEMP_PATH}${PROJECT_NAME}\\${PROJECT_NAME}\\${PROJECT_NAME}.apj
        ${technology_packages}=   Get Element              ${XML_root}    TechnologyPackages
        ${component}=             Get Element              ${technology_packages}    ${component_name}
    END
    Element Attribute Should Be                        ${component}    Version   ${expected_working_version}
    Log    Working version verified: ${expected_working_version}

Activate Button in Workspace Editor
    [Documentation]    Clicks a button in the workspace editor, identified by its name.
    ...                If "activate" is True, only clicks the button if it is not already active/selected.   
    [Arguments]        ${button_name}
    FlaUILib.Click Toolbar Button    ${button_name}    activate=True
    Log    Clicked workspace editor button: ${button_name}

Build Project
    [Documentation]    Builds the active configuration and waits for completion.
    [Arguments]                                        ${timeout}=60
    FlaUILib.Click Toolbar Button                      Build Configuration
    FlaUILib.Wait For Message                          Build:           ${timeout}
    ${dialog_appeared}=    FlaUILib.Wait For Dialog    Build Project    10
    IF    ${dialog_appeared}
        FlaUILib.Click Dialog Button                   Don't Transfer    dialog_close=True
    ELSE
        Log    No Transfer dialog appeared.
    END
    Log    Project build complete

Transfer Project To CPU
    [Documentation]    Transfers the project to the target CPU (Online > Transfer To Target) and waits.
    [Arguments]        ${timeout}=120
    Invoke IDE Menu    Online    Transfer To Target
    FlaUILib.Wait For Transfer To Complete    ${timeout}
    Log    Project transferred to CPU

Switch To View Type
    [Documentation]    Switches the project tree to the specified view type.
    [Arguments]        ${view_type}    ${sizeX}=400    ${sizeY}=400
    FlaUILib.Switch to View    ${view_type}    sizeX=${sizeX}    sizeY=${sizeY}
    Log    Switched to view type: ${view_type} with size (${sizeX}, ${sizeY})

Insert From ToolBox
    [Documentation]    Inserts a component from the Toolbox into the project by double-clicking it.
    [Arguments]        ${view}    ${component_name}    ${category}=None     ${drag}=False     ${xoffset}=0    ${yoffset}=0
    FlaUILib.Insert From Toolbox    ${view}    ${component_name}    ${category}    ${drag}    ${xoffset}    ${yoffset}
    Log    Inserted from Toolbox: ${component_name}

