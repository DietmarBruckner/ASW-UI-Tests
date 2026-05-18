*** Settings ***
Documentation       Test cases for MappView component configuration and widget insertion.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_keywords.robot
Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Configure MappView Server
    [Documentation]    Creates a project, adds MappView, verifies property panel shows port.
    [Tags]              mappview    configuration    smoke
    ${project_name}=    Set Variable    MappView_HTTP
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component    ${MAPPVIEW_HTTP_PORT}
    ${port}=    Get IDE Property    Port
    Should Be Equal As Strings    ${port}    ${MAPPVIEW_HTTP_PORT}
    Log    MappView configured with port ${MAPPVIEW_HTTP_PORT}


Create Visualization Project And Open
    [Documentation]    Creates a visualization project and opens it in the editor.
    [Tags]              mappview    visualization
    ${project_name}=    Set Variable    MappView_Visu
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    TestVisualization
    Open Visualization For Editing    TestVisualization
    Wait Until IDE Is Ready
    Log    Visualization project created and opened successfully


Insert All Widget Types
    [Documentation]    Inserts all widgets from Widgets.txt into a visualization, then builds.
    [Tags]              mappview    widgets    comprehensive    regression
    ${project_name}=    Set Variable    MappView_AllWidgets
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    AllWidgetsVisu
    Open Visualization For Editing    AllWidgetsVisu
    Insert All Widgets From Config
    Build Project
    Log    All widget types inserted and project built


Insert Button Widget And Configure Properties
    [Documentation]    Inserts a Button widget and sets its Text and BackgroundColor properties.
    [Tags]              mappview    widgets    properties
    ${project_name}=    Set Variable    MappView_Props
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    PropertyTestVisu
    Open Visualization For Editing    PropertyTestVisu
    Insert MappView Widget    Button    TestButton    BTN_001
    Configure Widget Property    Text    Click Me
    Configure Widget Property    BackgroundColor    0xFF0000
    Log    Widget properties configured successfully
