*** Settings ***
Documentation       High-level keywords for Automation Studio IDE interactions.
...                 These wrap FlaUILibrary calls and provide a stable API for
...                 project_keywords, component_keywords and test suites.
Library             ${CURDIR}\\..\\libraries\\FlaUILibrary\\robot_flaulib.py    WITH NAME    FlaUILib
Resource            ${CURDIR}\\..\\..\\config\\Config.robot


*** Keywords ***

Initialize Automation Studio
    [Documentation]    Launches (or attaches to) Automation Studio IDE and waits until ready.
    FlaUILib.Initialize Automation Studio    ${AS_DEFAULT_TIMEOUT}
    Log    Automation Studio initialised.

Close Automation Studio
    [Documentation]    Closes Automation Studio IDE.
    [Arguments]        ${save_changes}=True
    FlaUILib.Close Application    ${save_changes}
    Log    Automation Studio closed (save_changes=${save_changes})

Invoke IDE Menu
    [Documentation]    Clicks a top-level menu and optionally a menu/sub-menu item.
    [Arguments]        ${menu_name}    ${menu_item}=${NONE}    ${submenu_item}=${NONE}
    FlaUILib.Invoke Menu    ${menu_name}    ${menu_item}    ${submenu_item}
    Log    Menu invoked: ${menu_name} > ${menu_item} > ${submenu_item}

Wait Until IDE Is Ready
    [Documentation]    Waits for the IDE to become idle (no busy indicator).
    [Arguments]        ${timeout}=30
    FlaUILib.Wait For Idle    ${timeout}
    Log    IDE is ready (idle)

Select From ComboBox
    [Documentation]    Selects a value from a combo box by opening it and clicking the item.
    [Arguments]        ${combo_label}    ${item_text}
    FlaUILib.Select From ComboBox    ${combo_label}    ${item_text}
    Log    Selected from combo box: ${combo_label} > ${item_text}

Activate Simulation Mode
    [Documentation]    Activates simulation mode in the IDE, if not already active.
    FlaUILib.Activate Simulation Mode
    Log    Simulation mode activated

    