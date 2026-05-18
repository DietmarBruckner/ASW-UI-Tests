*** Settings ***
Documentation       Keywords for MappView widget insertion and verification.
...                 Widget types are read from config/Widgets.txt (one per line).
Library             OperatingSystem
Library             String
Library             Collections
Resource            ${CURDIR}/component_keywords.robot


*** Keywords ***

Get Widgets From Config
    [Documentation]    Reads widget names from Widgets.txt, returns a clean list.
    [Arguments]        ${config_file_path}=${CURDIR}/../config/Widgets.txt
    ${raw}=           Get File    ${config_file_path}
    @{lines}=         Split To Lines    ${raw}
    ${result}=        Create List
    FOR    ${line}    IN    @{lines}
        ${stripped}=    Strip String    ${line}
        IF    '${stripped}' != ''
            Append To List    ${result}    ${stripped}
        END
    END
    RETURN    ${result}


Insert And Verify Widget
    [Documentation]    Inserts a single widget by type, verifies it in the property panel.
    [Arguments]        ${widget_type}    ${widget_name}    ${widget_id}
    Insert MappView Widget    ${widget_type}    ${widget_name}    ${widget_id}
    ${prop}=    Get IDE Property    Name
    Should Be Equal    ${prop}    ${widget_name}
    Log    Widget verified: ${widget_type} (name=${widget_name}, id=${widget_id})


Insert All Widgets From Config
    [Documentation]    Reads Widgets.txt, inserts every widget type into the open visualization.
    [Arguments]        ${config_file_path}=${CURDIR}/../config/Widgets.txt
    ${widgets}=    Get Widgets From Config    ${config_file_path}
    ${count}=      Get Length    ${widgets}
    Log    Inserting ${count} widget types from config
    FOR    ${index}    ${widget_type}    IN ENUMERATE    @{widgets}
        ${widget_id}=    Set Variable    widget_${index}
        Insert And Verify Widget    ${widget_type}    ${widget_id}    ${widget_id}
    END
    Log    All ${count} widgets inserted and verified


Widget Should Exist In Tree
    [Documentation]    Fails if a widget node is not visible in the project tree.
    [Arguments]        ${widget_name}
    Navigate To Tree Leaf    ${widget_name}
    Log    Widget found in tree: ${widget_name}


Select Widget In Visualization
    [Documentation]    Clicks a widget element by name in the visualization editor.
    [Arguments]        ${widget_name}
    FlaUILib.Click Element    ${widget_name}
    Log    Widget selected: ${widget_name}


Hover Over Widget
    [Documentation]    Moves the mouse over a widget to trigger tooltip/hover state.
    [Arguments]        ${widget_name}
    FlaUILib.Hover Element    ${widget_name}
    Log    Hovered over widget: ${widget_name}
