*** Settings ***
Documentation       Test cases for Automation Studio project creation and initialization.
Resource            ${CURDIR}/../../keywords/project_keywords.robot
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Create Project With OPCUA Component
    [Documentation]    Creates a new project with OPCUA component configured.
    [Tags]              project-creation    smoke
    ${project_name}=    Set Variable    Project_OPCUA
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize OPCUA Component
    Build Project
    Directory Should Exist    ${project_path}
    Log    Project with OPCUA created successfully: ${project_name}


Create Project With MappView Visualization
    [Documentation]    Creates a new project with MappView component and a visualization.
    [Tags]              project-creation    mappview
    ${project_name}=    Set Variable    Project_MappView
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    TestVisualization
    Build Project
    Directory Should Exist    ${project_path}
    Log    Project with MappView created successfully: ${project_name}


Create Project With Full Component Stack
    [Documentation]    Creates a project with all components: OPCUA, MappView, AutomationRuntime.
    [Tags]              project-creation    integration    full-stack
    ${project_name}=    Set Variable    Project_Full
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}    ${CPU_TYPE}
    Initialize Full Component Stack
    Create Visualization Project    Full_Stack_Visu
    Build Project
    Directory Should Exist    ${project_path}
    Log    Comprehensive project created successfully: ${project_name}
    
    # Cleanup
    [Teardown]    Close Automation Studio    save_changes=False


Load And Modify Existing Project
    [Documentation]    Creates a project, closes it, re-loads it, and adds a component.
    [Tags]              project-creation    load    modify
    ${project_name}=    Set Variable    Project_Load
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Close Automation Studio    save_changes=True
    Load Existing Project    ${project_path}
    Switch To Configuration View
    Initialize OPCUA Component
    Build Project
    Log    Project loaded and modified successfully: ${project_name}
