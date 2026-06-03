*** Settings ***
Documentation       Keywords for configuring Automation Studio components:
...                 MappView, OPCUA, and AutomationRuntime.
Resource            ${CURDIR}/project_keywords.robot
Library             FlaUILibrary    uia=UIA2

*** Keywords ***

# ── MappView ─────────────────────────────────────────────────────────────────

Configure Widget Property
    [Documentation]    Sets a property on the currently selected widget.
    [Arguments]        ${property_name}    ${property_value}
    Set IDE Property    ${property_name}    ${property_value}
    Log    Widget property set: ${property_name} = ${property_value}

Insert mapp View with Default Template
    [Documentation]    Inserts a mappView component using the default template.
    Expand and Click Tree Leaf             Logical View
    Insert From ToolBox                    Logical View    mapp View    mapp View
    ${dialog_appeared}=    FlaUILib.Wait For Dialog    Insert mapp View solution    10
    IF    ${dialog_appeared}
        FlaUILib.Find And Select Item      Default
        FlaUILib.Click Dialog Button       Finish      dialog_close=True
    ELSE
        Log                                No insert mapp View dialog appeared.
    END
    FlaUILib.Wait For Message              Build widget library finished    timeout=30
    Log                                    mappView inserted with default template

Navigate To mapp View
    [Documentation]    Navigates to the DefaultView node under OpcUa in the Configuration View
    [Arguments]        ${logical view}=False
    IF   ${logical view}
        Expand and Click Tree Leaf         Logical View      BR_mappView
    ELSE
        Expand and Click Tree Leaf    Configuration View     BR_${CPU_TYPE}|BR_mappView
    END
    Log    Navigated to mappView in ${logical view} ? "Logical View" : "Configuration View"

Fill TMX Entries
    [Arguments]    @{test_enabled_widgets}    ${xpath}
    ${text_tree}=    Find One Element    ${xpath}
    FOR    ${widget}    IN    @{test_enabled_widgets}
        @{all_rows}=    Find All Elements    ${text_tree.Xpath}/TreeItem
        ${last_row}=    Get From List    ${all_rows}    -1
        @{fields}=    Find All Elements    ${last_row.Xpath}/*
        Click    ${fields[0].Xpath}
        Click Into IDE    position=True
        Press Key    t'ID_${widget}'
        Press Key    s'ENTER'
#        Sleep    0.8s
        Click    ${fields[1].Xpath}
        Press Key    t'${widget}_fr'
        Click    ${fields[2].Xpath}
        Press Key    t'${widget}_de'
        Click    ${fields[3].Xpath}
        Press Key    t'${widget}_en'
        Press Key    s'ENTER'
        Click Into IDE    position=True
#        Sleep    0.2s
    END

Read Widget Test Configuration
    [Arguments]    @{toTestWidgetGroups}
    @{test_enabled_widgets}=    Create List
    @{widget_groups}=    Create List    button_Widgets    chart_Widgets    container_Widgets    data_Widgets    dateTime_Widgets    drawing_Widgets    image_Widgets    login_Widgets    media_Widgets    motion_Widgets    numeric_Widgets    selector_Widgets    system_Widgets    text_Widgets    process_Widgets
    ${group_index}=    Set Variable    0
    FOR    ${group_name}    IN    @{widget_groups}
        ${should_test}=    Get From List    ${toTestWidgetGroups}    ${group_index}
        IF    ${should_test}
            ${widget_group}=    Get Variable Value    @{${group_name}}
            FOR    ${widget}    IN    @{widget_group}
                Append To List    ${test_enabled_widgets}    ${widget}
            END
        END
        ${group_index}=    Evaluate    ${group_index} + 1
    END
    LOG    Test enabled widget types: @{test_enabled_widgets}
    RETURN    @{test_enabled_widgets}

# ── OPCUA ─────────────────────────────────────────────────────────────────────

Navigate To OPCUA Default View
    [Documentation]    Navigates to the DefaultView node under OpcUa in the Configuration View
    Expand and Click Tree Leaf    Configuration View     BR_${CPU_TYPE}|BR_Connectivity|BR_OpcUaCs|BR_UaCsConfig.uacfg

Navigate To OPCUA Default View Configuration
    [Documentation]    Navigates to the DefaultView configuration node under OpcUa in the Configuration View
    Expand and Click Tree Leaf    Configuration View     BR_${CPU_TYPE}|BR_Connectivity|BR_OpcUaCs|BR_UaDvConfig.uadcfg

# ── User/Role management ─────────────────────────────────────────────────────────

Navigate To User/Role System
    [Documentation]    Navigates to the User/Role System node in the Configuration View
    Expand and Click Tree Leaf    Configuration View     BR_${CPU_TYPE}|BR_AccessAndSecurity|BR_UserRoleSystem

Add User Role in Role.role
    [Documentation]    Adds a user role in the Role.role tree under User/Role System.
    [Arguments]        ${rolename}   ${xpath}     ${addrole}=True
    ${text_tree}=    Find One Element    ${xpath}
    Click Into IDE   editor=True
    IF    ${addrole}
        Click Toolbar Button    Add "Role" Element
    END
    @{all_rows}=    Find All Elements    ${text_tree.Xpath}/TreeItem
    ${last_row}=    Get From List    ${all_rows}    -1
    ${role_name}=   ${last_row}.Name + _Name
    ${rolenamefield}=    Find One Element    ${last_row.Xpath}/*[@Name="$role_name"]
    Double Click    ${rolenamefield.Xpath}
    Press Key    t'${rolename}'
    Log    User role added: ${rolename}

Add User in User.user
    [Documentation]    Adds a user in the User.user tree under User/Role System.
    [Arguments]        ${username}    ${password}    ${role}    ${adduser}=True
    FlaUILib.Add User    ${username}    ${password}    ${role}    adduser=${adduser}
    Log    User added: ${username}


# ── AutomationRuntime ─────────────────────────────────────────────────────────


# ── Convenience ───────────────────────────────────────────────────────────────

