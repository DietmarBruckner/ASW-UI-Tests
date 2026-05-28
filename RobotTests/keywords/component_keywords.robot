*** Settings ***
Documentation       Keywords for configuring Automation Studio components:
...                 MappView, OPCUA, and AutomationRuntime.
Resource            ${CURDIR}/project_keywords.robot


*** Keywords ***

# ── MappView ─────────────────────────────────────────────────────────────────

Initialize MappView Component
    [Documentation]    Adds and configures MappView in the project.
    [Arguments]        ${port}=${MAPPVIEW_HTTP_PORT}
    Add Software Component    mappView
    Navigate To MappView Settings
    Set IDE Property    Port    ${port}
    Log    MappView component initialised on port ${port}


Navigate To MappView Settings
    [Documentation]    Navigates to the mappView node in the Logical View.
    Switch To Logical View
    Navigate To Tree Leaf    mappView


Create Visualization Project
    [Documentation]    Creates a new visualization project under mappView.
    [Arguments]        ${project_name}=Test_Visu
    Navigate To MappView Settings
    Open Context Menu For Element    Visualizations
    Select Context Menu Item    New Visualization
    FlaUILib.Wait For Dialog    New Visualization
    FlaUILib.Type Into Dialog Field    Name    ${project_name}
    FlaUILib.Click Dialog Button    OK
    Wait Until IDE Is Ready
    Log    Visualization project created: ${project_name}


Open Visualization For Editing
    [Documentation]    Double-clicks a visualization to open it in the editor.
    [Arguments]        ${visualization_name}
    Double Click Tree Leaf    mappView/${visualization_name}
    Wait Until IDE Is Ready
    Log    Visualization opened: ${visualization_name}


#Insert MappView Widget
#    [Documentation]    Inserts a widget from the Toolbox into the open visualization.
#    [Arguments]        ${widget_type}    ${widget_name}    ${widget_id}
#    # Drag from toolbox by double-clicking the widget type entry
#    Double Click Tree Leaf    ${widget_type}
#    Wait Until IDE Is Ready
#    # Set widget properties
#    Set IDE Property    Name    ${widget_name}
#    Set IDE Property    id    ${widget_id}
#    Log    Widget ${widget_type} inserted: name=${widget_name}, id=${widget_id}


Configure Widget Property
    [Documentation]    Sets a property on the currently selected widget.
    [Arguments]        ${property_name}    ${property_value}
    Set IDE Property    ${property_name}    ${property_value}
    Log    Widget property set: ${property_name} = ${property_value}


# ── OPCUA ─────────────────────────────────────────────────────────────────────

Initialize OPCUA Component
    [Documentation]    Adds and configures the OPCUA component in the project.
    [Arguments]        ${port}=${OPCUA_PORT}
    Add Software Component    OpcUa
    Navigate To OPCUA Settings
    Set IDE Property    Port    ${port}
    Set IDE Property    Enabled    True
    Log    OPCUA component initialised on port ${port}


Navigate To OPCUA Settings
    [Documentation]    Navigates to the OpcUa node in the Logical View.
    Switch To Logical View
    Navigate To Tree Leaf    OpcUa


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
    [Arguments]        ${rolename}    ${addrole}=True
    FlaUILib.Add Role    ${rolename}    addrole=${addrole}
    Log    User role added: ${rolename}

Add User in User.user
    [Documentation]    Adds a user in the User.user tree under User/Role System.
    [Arguments]        ${username}    ${password}    ${role}    ${adduser}=True
    FlaUILib.Add User    ${username}    ${password}    ${role}    adduser=${adduser}
    Log    User added: ${username}


# ── AutomationRuntime ─────────────────────────────────────────────────────────

Initialize AutomationRuntime Component
    [Documentation]    Adds and configures the AutomationRuntime component.
    [Arguments]        ${min_version}=${AUTOMATIONRUNTIME_MIN_VERSION}
    Add Software Component    AutomationRuntime
    Navigate To AutomationRuntime Settings
    Set IDE Property    Version    ${min_version}
    Log    AutomationRuntime component initialised (v${min_version})


Navigate To AutomationRuntime Settings
    [Documentation]    Navigates to the AutomationRuntime node in the Logical View.
    Switch To Logical View
    Navigate To Tree Leaf    AutomationRuntime


# ── Convenience ───────────────────────────────────────────────────────────────

Initialize Full Component Stack
    [Documentation]    Initialises MappView, OPCUA and AutomationRuntime in one call.
    [Arguments]        ${mappview_port}=${MAPPVIEW_HTTP_PORT}
    ...                ${opcua_port}=${OPCUA_PORT}
    ...                ${ar_version}=${AUTOMATIONRUNTIME_MIN_VERSION}
    Initialize MappView Component    ${mappview_port}
    Initialize OPCUA Component    ${opcua_port}
    Initialize AutomationRuntime Component    ${ar_version}
    Log    Full component stack initialised
    Activate Tree Leaf    MappView
