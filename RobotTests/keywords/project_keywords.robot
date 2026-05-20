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

    Log    Project "${project_name}" created at ${project_path}
