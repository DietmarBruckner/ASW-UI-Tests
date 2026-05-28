*** Settings ***
Documentation       Test cases for OPCUA component configuration.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
#Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise OPCUA Version
    [Documentation]    Scenario: Initialize OPC UA CS version
    ...                Traceability ID: FW-OPCUA-C1
    ...                Component: OPC UA CS
    ...                Source Manual: 
    ...                Source Section: 
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: OPC UA CS TP available
    [Tags]             opcua    configuration    smoke    trace:fw-opcua-c1
    Initialize Automation Studio
    Select Working Version for Component    OPC     ${UACS_VERSION}
    Build Project
    Log    OPC UA CS version initialised

Activate OPCUA Client Server With Anonymous Access and BR_Engineer
    [Documentation]    Scenario: Activate OPC UA CS with anonymous access and BR_Engineer user
    ...                Traceability ID: FW-OPCUA-C4
    ...                Component: OPC UA CS
    ...                Source Manual:
    ...                Source Section: TM611_3_1_ActivateOPCUACS
    ...                Evidence Type: Automated UI procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: OPC UA CS package is available and project is open
    [Tags]             opcua    configuration    security    trace:fw-opcua-c4    trace:tm611
    Initialize Automation Studio
    Navigate To OPCUA Default View
    Activate Button in Workspace Editor   Change Advanced Parameter Visibility
    Expand and Click Tree Leaf            Workspace     BR_OPC UA Client/Server    rootname=BR_ClientServerConfiguration    editorname=e
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_ClientServerConfiguration    editorname=e    filename=OPCUACS    filetree=Anonymous    version=${UACS_VERSION}
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_ClientServerConfiguration    editorname=e    filename=OPCUACS    filetree=Anonymous Access|BR_User Role 1    version=${UACS_VERSION}
    Select From TreeComboBox              item_number=2
    Build Project
    Log    OPC UA client/server activation with anonymous access and BR_Engineer configured successfully


Configure OPCUA RBAC Roles, Users, And Default Permissions
    [Documentation]    Scenario: Replicate TM611_10 RBAC flow for OPC UA/CS
    ...                Traceability ID: FW-OPCUA-C5
    ...                Component: OPC UA CS
    ...                Source Manual:
    ...                Source Section: TM611_10_RBAC
    ...                Evidence Type: Automated UI procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: OPC UA CS package is available and project is open
    [Tags]             opcua    rbac    security    trace:fw-opcua-c5    trace:tm611
    Initialize Automation Studio
    Navigate To User/Role System
    Insert From ToolBox                    Configuration View    Role
    Expand and Click Tree Leaf             Configuration View    BR_${CPU_TYPE}|BR_AccessAndSecurity|BR_UserRoleSystem|BR_Role.role    shortcut=0
    Add User Role in Role.role             Operator    addrole=False
    Add User Role in Role.role             Service
    Add User Role in Role.role             Observer
    Close Editor
    Navigate To User/Role System
    Insert From ToolBox                    Configuration View    User
    Expand and Click Tree Leaf             Configuration View    BR_${CPU_TYPE}|BR_AccessAndSecurity|BR_UserRoleSystem|BR_User.user    shortcut=0
    Add User in User.user                  UserOperator    5555    Operator    adduser=False
    Add User in User.user                  UserService     9999    Service
    Add User in User.user                  UserObserver    0000    Observer
    Close Editor
    Navigate To OPCUA Default View Configuration
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 1|BR_Name    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_label=Operator
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 2|BR_Name    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_label=Service
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 3|BR_Name    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_label=Observer
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 1|BR_Permissions|BR_Browse    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 1|BR_Permissions|BR_Read    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 1|BR_Permissions|BR_Write    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 1|BR_Permissions|BR_Call    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 1|BR_Permissions|BR_ReadRolePermissions    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 1|BR_Permissions|BR_ReadHistory    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 2|BR_Permissions|BR_Browse    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 2|BR_Permissions|BR_Read    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 2|BR_Permissions|BR_Write    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 2|BR_Permissions|BR_Call    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 2|BR_Permissions|BR_ReadRolePermissions    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 2|BR_Permissions|BR_ReadHistory    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 3|BR_Permissions|BR_Browse    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 3|BR_Permissions|BR_Read    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 3|BR_Permissions|BR_Call    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 3|BR_Permissions|BR_ReadRolePermissions    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Expand and Click Tree Leaf            Workspace     rootname=BR_DefaultViewConfiguration    editorname=e    filename=OPCUACSCONF    filetree=DefaultRolePermissions|BR_Role 3|BR_Permissions|BR_ReadHistory    version=${UACS_VERSION}    shortcut=0
    Select From TreeComboBox              item_number=1
    Close Editor
    Build Project
    Log    OPC UA RBAC roles, users and default permissions configured successfully


OPCUA With MappView Integration
    [Documentation]    Scenario: Combine OPC UA server setup with mappView visualization
    ...                Traceability ID: FW-OPCUA-C3
    ...                Component: OPC UA CS
    ...                Source Manual: agents/TM980-ENG_OPC_UA_basics_ans_use_V3000_AS4C.md
    ...                Source Section: 5 Automation Runtime OPC UA server; 3.3 Reading data
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappView and OPC UA packages are available
    [Tags]              opcua    integration    mappview    trace:fw-opcua-c3    trace:tm980    trace:sec-5    trace:sec-3.3
    Initialize Automation Studio
    Initialize OPCUA Component
    Initialize MappView Component
    Create Visualization Project    OpcuaVisu
    Build Project
    Log    OPCUA and MappView integration project built successfully

Configure OPCUA Custom Port
    [Documentation]    Scenario: Configure OPC UA custom port and verify property
    ...                Traceability ID: FW-OPCUA-C2
    ...                Component: OPC UA CS
    ...                Source Manual: agents/TM980-ENG_OPC_UA_basics_ans_use_V3000_AS4C.md
    ...                Source Section: 5.1 Configuration of the default view
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: OPC UA property pane is editable
    [Tags]              opcua    configuration    trace:fw-opcua-c2    trace:tm980    trace:sec-5.1
    ${project_name}=    Set Variable    OPCUA_Port
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize OPCUA Component    4841
    ${port}=    Get IDE Property    Port
    Should Be Equal As Strings    ${port}    4841
    Log    OPCUA custom port configured successfully