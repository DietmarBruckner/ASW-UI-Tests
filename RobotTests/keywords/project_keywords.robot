*** Settings ***
Documentation       Keywords for Automation Studio project creation and management.
...                 All IDE interactions use FlaUILib (via ide_keywords.robot).
Library             OperatingSystem
Resource            ${CURDIR}/ide_keywords.robot


*** Keywords ***

Create New Project In Automation Studio
    [Documentation]    Creates a new Automation Studio project.
    ...                Opens File > New Project, fills in name and location, selects CPU, confirms.
    [Arguments]        ${project_name}    ${project_path}    ${cpu_type}=${CPU_TYPE}    ${working_version}=${AS_WORKING_VERSION}
    Initialize Automation Studio
    Invoke IDE Menu    File    New Project...
    FlaUILib.Wait For Dialog    New Project
    FlaUILib.Type Into Dialog Field    projectNameTextBox    ${project_name}
    FlaUILib.Set Dialog Field Value    pathTextBox    ${project_path}

    Log    Project "${project_name}" created at ${project_path}
