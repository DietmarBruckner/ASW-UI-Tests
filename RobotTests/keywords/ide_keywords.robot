*** Settings ***
Documentation       High-level keywords for Automation Studio IDE interactions.
...                 These wrap FlaUILibrary calls and provide a stable API for
...                 project_keywords, component_keywords and test suites.
Library             ${CURDIR}\\..\\libraries\\FlaUILibrary\\robot_flaulib.py    WITH NAME    FlaUILib
Resource            ${CURDIR}\\..\\..\\config\\Config.robot


*** Keywords ***

Initialize Automation Studio
    [Documentation]    Launches (or attaches to) Automation Studio IDE and waits until ready.
    FlaUILib.Initialize Automation Studio    ${AS_IDE_PATH}    ${AS_DEFAULT_TIMEOUT}
    Log    Automation Studio initialised at: ${AS_IDE_PATH}


Close Automation Studio
    [Documentation]    Closes Automation Studio IDE.
    [Arguments]        ${save_changes}=True
    FlaUILib.Close Application    ${save_changes}
    Log    Automation Studio closed (save_changes=${save_changes})