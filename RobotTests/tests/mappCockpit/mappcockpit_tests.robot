*** Settings ***
Documentation       Test cases for MappView component configuration and widget insertion.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_property_keywords.robot
Library             FlaUILibrary    uia=UIA2
Library    ${CURDIR}/../../libraries/FlaUILibrary/robot_flaulib.py    server_url=http://localhost:5000

Suite Teardown      FlaUILib.Check App Alive


*** Test Cases ***

Initialise MappCockpit Version
    [Documentation]    Scenario: Initialize mapp Cockpit version
    ...                Traceability ID: FW-MCP-C1
    ...                Component: mappCockpit
    ...                Source Manual: 
    ...                Source Section: 
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappCockpit component available
    [Tags]             mappcockpit    configuration    smoke    trace:fw-mcp-c1
    Initialize Automation Studio
    Select Working Version for Component    mapp Cockpit     ${DEFAULT_CP_VERSION}
    Build Project
    Log    mappCockpit version initialised


Configure MappCockpit Server
    [Documentation]    Scenario: Configure mapp Cockpit server
    ...                Traceability ID: FW-MCP-C2
    ...                Component: mappCockpit
    ...                Source Manual: 
    ...                Source Section: 
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappCockpit component available
    [Tags]             mappcockpit    configuration    smoke    trace:fw-mcp-c2
    Initialize Automation Studio
    Expand and Click Tree Leaf             Configuration View    BR_${CPU_TYPE}|BR_mappCockpit
    Insert From ToolBox                    Configuration View    mapp Cockpit server configuration
    Expand and Click Tree Leaf             Configuration View    BR_${CPU_TYPE}|BR_mappCockpit|BR_mCoWebSc.mcowebservercfg
    Click Into IDE
    Sleep    2s
    Expand and Click Tree Leaf             Workspace     tree_path=BR_HTTPS Server    rootname=BR_mCoWebServerCfg    editorname=e   shortcut=0
    Select From TreeComboBox               item_number=0
    Expand and Click Tree Leaf             Workspace     tree_path=BR_HTTP Server    rootname=BR_mCoWebServerCfg    editorname=e   shortcut=0
    Select From TreeComboBox               item_number=1
    Close Editor
    Insert From ToolBox                    Configuration View    mapp Cockpit Settings
    Build Project
    
    Log    mappCockpit server configured
