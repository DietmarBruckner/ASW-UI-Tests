*** Settings ***
Documentation       High-level keywords for Automation Studio IDE interactions.
...                 These wrap FlaUILibrary calls and provide a stable API for
...                 project_keywords, component_keywords and test suites.
Library             ${CURDIR}/../libraries/FlaUILibrary/robot_flaulib.py    WITH NAME    FlaUILib
Resource            ${CURDIR}/../config/hardware_config.robot


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


Navigate To Tree Leaf
    [Documentation]    Clicks through a slash-separated path in the project tree.
    [Arguments]        ${tree_path}
    FlaUILib.Activate Tree Leaf    ${tree_path}
    Log    Navigated to tree path: ${tree_path}


Double Click Tree Leaf
    [Documentation]    Double-clicks a leaf node in the project tree (e.g. to open a file).
    [Arguments]        ${tree_path}
    FlaUILib.Activate Tree Leaf    ${tree_path}    double_click=True
    Log    Double-clicked tree path: ${tree_path}


Expand Tree Node
    [Documentation]    Expands a collapsed tree node by name.
    [Arguments]        ${node_name}
    FlaUILib.Expand Tree Node    ${node_name}
    Log    Tree node expanded: ${node_name}


Collapse Tree Node
    [Documentation]    Collapses an expanded tree node by name.
    [Arguments]        ${node_name}
    FlaUILib.Collapse Tree Node    ${node_name}
    Log    Tree node collapsed: ${node_name}


Invoke IDE Menu
    [Documentation]    Clicks a top-level menu and optionally a menu/sub-menu item.
    [Arguments]        ${menu_name}    ${menu_item}=${NONE}    ${submenu_item}=${NONE}
    FlaUILib.Invoke Menu    ${menu_name}    ${menu_item}    ${submenu_item}
    Log    Menu invoked: ${menu_name} > ${menu_item} > ${submenu_item}


Open Context Menu For Element
    [Documentation]    Right-clicks a UI element to open its context menu.
    [Arguments]        ${element_name}    ${search_by}=name
    FlaUILib.Open Context Menu    ${element_name}    ${search_by}
    Log    Context menu opened for: ${element_name}


Select Context Menu Item
    [Documentation]    Clicks an item (and optional sub-item) in the visible context menu.
    [Arguments]        ${menu_item}    ${submenu_item}=${NONE}
    FlaUILib.Select From Context Menu    ${menu_item}    ${submenu_item}
    Log    Context menu item selected: ${menu_item}


Handle Modal Dialog
    [Documentation]    Waits for a modal dialog, then clicks the specified button.
    [Arguments]        ${dialog_title}    ${button}=OK    ${timeout}=15
    FlaUILib.Wait For Dialog    ${dialog_title}    ${timeout}
    FlaUILib.Click Dialog Button    ${button}    ${dialog_title}
    Log    Dialog "${dialog_title}" dismissed with "${button}"


Wait For Modal And Fill Field
    [Documentation]    Waits for a dialog, fills a field, then clicks a button.
    [Arguments]        ${dialog_title}    ${field_label}    ${field_value}    ${button}=OK
    FlaUILib.Wait For Dialog    ${dialog_title}
    FlaUILib.Type Into Dialog Field    ${field_label}    ${field_value}
    FlaUILib.Click Dialog Button    ${button}    ${dialog_title}
    Log    Dialog "${dialog_title}": set "${field_label}"="${field_value}", clicked "${button}"


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


Wait Until IDE Is Ready
    [Documentation]    Waits for the IDE to become idle (no busy indicator).
    [Arguments]        ${timeout}=30
    FlaUILib.Wait For Idle    ${timeout}
    Log    IDE is ready (idle)


Take IDE Screenshot
    [Documentation]    Captures a screenshot of the IDE window.
    [Arguments]        ${filename}=${NONE}    ${outputdir}=${NONE}
    FlaUILib.Take Screenshot    ${filename}    ${outputdir}
    Log    Screenshot captured


IDE Window Title Should Contain
    [Documentation]    Fails if the IDE window title does not contain the expected text.
    [Arguments]        ${expected_text}
    ${title}=    FlaUILib.Get Window Title
    Should Contain    ${title}    ${expected_text}
    Log    Window title verified: ${title}


Project Should Be Loaded
    [Documentation]    Fails if no project is currently loaded in the IDE.
    ${loaded}=    FlaUILib.Is Project Loaded
    Should Be True    ${loaded}    msg=No project is currently loaded in Automation Studio
