*** Settings ***
Documentation       Test cases for MappView component configuration and widget insertion.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_property_keywords.robot
Library             FlaUILibrary    uia=UIA2

#Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise MappControl Version
    [Documentation]    Scenario: Initialize mapp Control version
    ...                Traceability ID: FW-MCT-C1
    ...                Component: mappControl
    ...                Source Manual: 
    ...                Source Section: 
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappCockpit component available
    [Tags]             mappcontrol    configuration    smoke    trace:fw-mcp-c1
    Initialize Automation Studio
    Select Working Version for Component    mapp Control     ${DEFAULT_CON_VERSION}
    Build Project
    Log    mappControl version initialised


Configure Temp Control System
    Start FlaUI Server
    Initialize Automation Studio
    Expand and Click Tree Leaf    Configuration View     BR_${CPU_TYPE}|BR_mappControl
    Insert From ToolBox           Configuration View    Temperature Controller
    Expand and Click Tree Leaf    Configuration View    BR_${CPU_TYPE}|BR_mappControl|BR_Config.tempcontroller
    Expand and Click Tree Leaf    Workspace     rootname=BR_gTempController_1    editorname=e    tree_path=BR_Temperature controller|BR_Type   shortcut=-1
    Select From TreeComboBox      item_number=2
    Close Editor
    Generate Program              name=TempControl    ansi_c=True
    Expand and Click Tree Leaf    Logical View    BR_TempControl|BR_Cyclic.c
    Press Key                     s'CTRL+A'
    Press Key                     s'DELETE'
    ${myList}=    Create List     t'#include <bur/plctypes.h>'    t' '    t'#ifdef _DEFAULT_INCLUDES'    t'#include <AsDefault.h>'    t'#endif'    t' '    t'void _CYCLIC ProgramCyclic(void)'    t'{'    t'MpTempController_0.MpLink = &gTempController_1;'    t'MpTempController_0.Enable = 1;'    t'MpTempController_0.Parameters = &tempPar;'    t'MpTempController(&MpTempController_0);'    t'}'
    Send Inputs from List         ${myList}    s'ENTER'
    Close Editor
    Generate Variables            MpTempController_0   MpTempController    TempControl
    Generate Variables            tempPar   MpTempControllerParType    TempControl
    Close Editor
    Build Project
    Stop FlaUI Server
    Log    Basic Temp Control created