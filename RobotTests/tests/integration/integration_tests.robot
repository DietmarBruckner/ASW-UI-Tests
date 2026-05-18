*** Settings ***
Documentation       Integration test cases combining multiple components and workflows.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_keywords.robot
Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Full Stack Project With All Components
    [Documentation]    Creates a project with OPCUA, MappView, AutomationRuntime and all widgets.
    [Tags]              integration    full-stack    smoke
    ${project_name}=    Set Variable    FullStack_All
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}    ${CPU_TYPE}
    Initialize Full Component Stack
    Create Visualization Project    FullStackVisu
    Open Visualization For Editing    FullStackVisu
    Insert All Widgets From Config
    Build Project
    Directory Should Exist    ${project_path}
    Log    Full stack project with all components built successfully


Complete Workflow Build Transfer Deploy
    [Documentation]    Creates a project with a Button widget, builds and transfers it.
    [Tags]              integration    workflow    deployment
    ${project_name}=    Set Variable    Workflow_Complete
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    WorkflowVisu
    Open Visualization For Editing    WorkflowVisu
    Insert MappView Widget    Button    StartButton    BTN_001
    Build And Transfer Project
    Directory Should Exist    ${project_path}
    Log    Complete workflow executed successfully


Multiple Projects Lifecycle
    [Documentation]    Creates, builds and deletes three projects in sequence.
    [Tags]              integration    lifecycle    cleanup
    @{project_names}=    Create List    Project_1    Project_2    Project_3
    FOR    ${project_name}    IN    @{project_names}
        ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
        Create New Project In Automation Studio    ${project_name}    ${project_path}
        Build Project
        Close Automation Studio    save_changes=False
        Directory Should Exist    ${project_path}
    END
    FOR    ${project_name}    IN    @{project_names}
        ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
        Delete Project    ${project_path}
    END
    Log    Multiple projects lifecycle test completed
