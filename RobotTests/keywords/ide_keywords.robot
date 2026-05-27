*** Settings ***
Documentation       High-level keywords for Automation Studio IDE interactions.
...                 These wrap FlaUILibrary calls and provide a stable API for
...                 project_keywords, component_keywords and test suites.
Resource            ${CURDIR}\\..\\..\\config\\Config.robot
Library             ${CURDIR}\\..\\libraries\\FlaUILibrary\\robot_flaulib.py    timeout=${AS_DEFAULT_TIMEOUT}    WITH NAME    FlaUILib


*** Keywords ***

Initialize Automation Studio
    [Documentation]    Launches (or attaches to) Automation Studio IDE and waits until ready.
    FlaUILib.Initialize Automation Studio    ${AS_DEFAULT_TIMEOUT}    ${AS_VERBOSE}
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

Expand and Click Tree Leaf
    [Documentation]    Expands a tree node and (double)-clicks a leaf node in the active tree.
    [Arguments]        ${viewtype}    ${tree_path}=None    ${editorname}=None    ${rootname}=None    ${program}=False    ${shortcut}=-1     ${single_click}=False    ${filename}=None    ${filetree}=None    ${version}=None
    FlaUILib.Activate Tree Leaf    ${viewtype}    ${tree_path}    ${editorname}    ${rootname}    ${program}    ${shortcut}    ${single_click}    ${filename}    ${filetree}    ${version}
    Log    Double-clicked tree path: ${tree_path}

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

Select From TreeComboBox
    [Documentation]    Selects a value from a combo box in a tree by opening it and clicking the item.
    [Arguments]        ${item_label}=None    ${item_number}=-1
    FlaUILib.Select From TreeComboBox    ${item_label}    ${item_number}
    IF     ${item_label} != None
        Log    Selected from tree combo box: ${item_label}
    ELSE
        Log    Selected from tree combo box item number: ${item_number}
    END

Open Context Menu For Element
    [Documentation]    Right-clicks a UI element to open its context menu.
    [Arguments]        ${element_name}    ${search_by}=name
    FlaUILib.Open Context Menu    ${element_name}    ${search_by}
    Log    Context menu opened for: ${element_name}

Select Context Menu Item
    [Documentation]    Clicks an item and optional sub-item in the visible context menu.
    [Arguments]        ${menu_item}    ${submenu_item}=${NONE}
    FlaUILib.Select From Context Menu    ${menu_item}    ${submenu_item}
    Log    Context menu item selected: ${menu_item}

Get Dialog Field Value
    [Documentation]    Returns the current text in a dialog field.
    [Arguments]        ${field_label}    ${dialog_title}=${NONE}
    ${value}=    FlaUILib.Get Dialog Field Text    ${field_label}    ${dialog_title}
    RETURN    ${value}

Set IDE Property
    [Documentation]    Sets a value in the IDE property panel.
    [Arguments]        ${property_name}    ${value}
    FlaUILib.Set Property Value    ${property_name}    ${value}
    Log    Property set: ${property_name} = ${value}

Get IDE Property
    [Documentation]    Reads a value from the IDE property panel.
    [Arguments]        ${property_name}
    ${value}=    FlaUILib.Get Property Value    ${property_name}
    RETURN    ${value}

Activate Simulation Mode
    [Documentation]    Activates simulation mode in the IDE, if not already active.
    FlaUILib.Activate Simulation Mode
    Log    Simulation mode activated

Take IDE Screenshot
    [Documentation]    Captures a screenshot of the IDE window.
    [Arguments]        ${filename}=${NONE}    ${outputdir}=${NONE}
    FlaUILib.Take Screenshot    ${filename}    ${outputdir}
    Log    Screenshot captured

Project Should Be Loaded
    [Documentation]    Fails if no project is currently loaded in the IDE.
    ${loaded}=    FlaUILib.Is Project Loaded
    Should Be True    ${loaded}    msg=No project is currently loaded in Automation Studio

    