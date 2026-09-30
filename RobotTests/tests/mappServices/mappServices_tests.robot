*** Settings ***
Documentation       Test cases for MappView component configuration and widget insertion.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_property_keywords.robot
Library             FlaUILibrary    uia=UIA2

#Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise MappServices Version
    [Documentation]    Scenario: Initialize mapp Services version
    ...                Traceability ID: FW-MCT-C1
    ...                Component: MappServices
    ...                Source Manual: 
    ...                Source Section: 
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappCockpit component available
    [Tags]             mappservices    configuration    smoke    trace:fw-mcp-c1
    Start FlaUI Server
    Initialize Automation Studio
    Select Working Version for Component    mapp Services     ${DEFAULT_SERV_VERSION}
    #Insert mapp View with Default Template
    Build Project
    Stop FlaUI Server
    Log    MappServices version initialised


Configure Basic Alarm System
    Start FlaUI Server
    Initialize Automation Studio
    Expand and Click Tree Leaf    Configuration View     BR_${CPU_TYPE}|BR_mappServices
    Insert From ToolBox           Configuration View    Basic Alarm System
    Expand and Click Tree Leaf    Configuration View    BR_${CPU_TYPE}|BR_mappServices|BR_AlarmCore.mpalarmxcore
    Expand and Click Tree Leaf             Workspace     rootname=BR_gAlarmXCore    editorname=e    tree_path=BR_General|BR_Enable Cockpit    shortcut=-1
    Select From TreeComboBox               item_number=0
    Close Editor
    Expand and Click Tree Leaf    Configuration View    BR_${CPU_TYPE}|BR_mappServices|BR_AlarmList.mpalarmxlist
    Expand and Click Tree Leaf             Workspace     rootname=BR_gAlarmXList    editorname=e    tree_path=BR_Alarm List|BR_Alarm: |BR_Name  shortcut=-1    single_click=True
    Press Key    t'test1'
    Press Key    s'ENTER'
    Expand and Click Tree Leaf             Workspace     rootname=BR_gAlarmXList    editorname=e    tree_path=BR_Alarm List|BR_Alarm: test1|BR_Behavior  shortcut=-1
    Select From TreeComboBox               item_number=3
    Close Editor
    Stop FlaUI Server