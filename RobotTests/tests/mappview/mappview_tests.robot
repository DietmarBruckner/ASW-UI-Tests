*** Settings ***
Documentation       Test cases for MappView component configuration and widget insertion.
Resource            ${CURDIR}/../../keywords/component_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_keywords.robot
Resource            ${CURDIR}/../../keywords/widget_property_keywords.robot
Library             FlaUILibrary    uia=UIA2

#Suite Teardown      Close Automation Studio    save_changes=False


*** Test Cases ***

Initialise MappView Version
    [Documentation]    Scenario: Initialize mapp View version
    ...                Traceability ID: FW-MVIEW-C1
    ...                Component: mappView
    ...                Source Manual: 
    ...                Source Section: 
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappView component available
    [Tags]             mappview    configuration    smoke    trace:fw-mview-c1
    Initialize Automation Studio
    Select Working Version for Component    mapp View     ${VIEW_VERSION}
    Insert mapp View with Default Template
    Build Project
    Log    mappView version initialised


Configure MappView Server
    [Documentation]    Scenario: Configure mappView server and verify communication port
    ...                Traceability ID: FW-MVIEW-D1
    ...                Component: mappView
    ...                Source Manual: agents/TM611-GER_Working_with_mapp_View_V6001_mV61.md
    ...                Source Section: 3.2 Configure mappView server
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappView component is available in project
    [Tags]             mappview    configuration    smoke    trace:fw-mview-d1    trace:tm611    trace:sec-3.2
    Initialize Automation Studio
    Navigate To mapp View
    Insert From ToolBox                    Configuration View    mapp View Configuration
    Expand and Click Tree Leaf             Configuration View    BR_${CPU_TYPE}|BR_mappView|BR_Config.mappviewcfg
    Click Into IDE
    Expand and Click Tree Leaf             Workspace     rootname=BR_MappViewConfiguration    editorname=e    filename=MAPPVIEW    filetree=Protocol    version=${VIEW_VERSION}    shortcut=0
    Select From TreeComboBox               item_number=0
    Expand and Click Tree Leaf             Workspace     rootname=BR_MappViewConfiguration    editorname=e    filename=MAPPVIEW    filetree=Startup User    version=${VIEW_VERSION}    shortcut=0
    Select From TreeComboBox               item_number=2
    Close Editor
    Build Project
    Log    MappView configured with protocol and startup user

Insert and Configure Localization
    [Documentation]    Scenario: Insert and configure localization resource
    ...                Traceability ID: FW-MVIEW-D2
    ...                Component: mappView
    ...                Source Manual: agents/TM611-GER_Working_with_mapp_View_V6001_mV61.md
    ...                Source Section: 11 Localization
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappView component is available in project
    [Tags]             mappview    configuration    localization    trace:fw-mview-d2    trace:tm611    trace:sec-11
    Initialize Automation Studio
    Navigate To mapp View                  logical view=True
    Insert From ToolBox                    Logical View    Project Languages
    FlaUILib.Click Toolbar Button          Save All
    Expand and Click Tree Leaf             Logical View    BR_mappView|BR_Visualization|BR_Resources|BR_Texts
    Insert From ToolBox                    Logical View    Localizable Texts
    FlaUILib.Click Toolbar Button          Save All
    Expand and Click Tree Leaf             Logical View    BR_mappView|BR_Visualization|BR_Resources|BR_Texts|BR_LocalizableTexts.tmx    shortcut=0
    FlaUILib.Type Into Dialog Field        textNamespace   IAT
    FlaUILib.Click Toolbar Button          Save All
    @{test_enabled_widgets}=               Read Widget Test Configuration    @{toTestWidgetGroups}
    ${xpath_tmx}=                          Get ConfigTree Xpath
    Fill TMX Entries                       @{test_enabled_widgets}    xpath=${xpath_tmx}
    FlaUILib.Click Toolbar Button          Save All
    Close Editor
    Expand and Click Tree Leaf             Configuration View    BR_${CPU_TYPE}|BR_TextSystem
    Insert From ToolBox                    Configuration View    Textsystem Configuration
    Expand and Click Tree Leaf             Configuration View    BR_${CPU_TYPE}|BR_TextSystem|BR_TC.textconfig
    Click Into IDE
    Expand and Click Tree Leaf             Workspace     rootname=BR_TextConfig    editorname=e    filename=TEXTSYSTEM    filetree=System language    shortcut=0
    Select From TreeComboBox               item_label=en
    Expand and Click Tree Leaf             Workspace     rootname=BR_TextConfig    editorname=e    filename=TEXTSYSTEM    filetree=Fallback language    shortcut=0
    Select From TreeComboBox               item_label=de
    Expand and Click Tree Leaf             Workspace     rootname=BR_TextConfig    editorname=e    filename=TEXTSYSTEM    filetree=Target languages|BR_Target language 1    shortcut=0
    Select From TreeComboBox               item_label=en
    Expand and Click Tree Leaf             Workspace     rootname=BR_TextConfig    editorname=e    filename=TEXTSYSTEM    filetree=Target languages|BR_Target language 2    shortcut=0
    Select From TreeComboBox               item_label=de
    Expand and Click Tree Leaf             Workspace     rootname=BR_TextConfig    editorname=e    filename=TEXTSYSTEM    filetree=Target languages|BR_Target language 3    shortcut=0
    Select From TreeComboBox               item_label=fr
    Expand and Click Tree Leaf             Workspace     rootname=BR_TextConfig    editorname=e    filename=TEXTSYSTEM    filetree=Tmx files for target|BR_Tmx file 1    shortcut=0
    Select From TreeComboBox               item_number=0
    Click Into IDE
    FlaUILib.Click Toolbar Button          Save All
    Close Editor
    Build Project
    Log    Localization resource inserted and configured

Prepare Layout for Widget Pages
    [Documentation]    Scenario: Prepare mappView layout for widget page tests
    ...                Traceability ID: FW-MVIEW-C2
    ...                Component: mappView
    ...                Source Manual: agents/TM611-GER_Working_with_mapp_View_V6001_mV61.md
    ...                Source Section: 5.4 Content and Widgets
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: mappView component available and configured
    [Tags]             mappview    configuration    trace:fw-mview-c2    trace:tm611    trace:sec-5.4
    Initialize Automation Studio
    Navigate To mapp View                  logical view=True
    Expand and Click Tree Leaf             Logical View    BR_mappView|BR_Visualization|BR_Pages|BR_page_0|BR_content_0.content    shortcut=0
    #Sleep                                  4s
    ${xpath}                               FlaUILib.Get IAT Editor XPath
    ${IAT_editor}    Find One Element      ${xpath}
    @{children}      Find All Elements     ${xpath}/Group/*
    ${label1}        Set Variable          ${children[0]}
    Click                                  ${label1.Xpath}
    Press Key                              s'DEL'
    FlaUILib.Click Toolbar Button          Save All
    Edit Widget Size                       width=700    height=500    is_content=True
    Close Editor
    Expand and Click Tree Leaf             Logical View    BR_mappView|BR_Visualization|BR_Pages|BR_AreaContents    shortcut=0
    Insert From ToolBox                    Logical View    Page content
    Expand and Click Tree Leaf             Logical View    BR_mappView|BR_Visualization|BR_Pages|BR_AreaContents|BR_content_1.content    shortcut=0
    Wait Until Element Exist               ${xpath}
    Edit Widget Name                       Navigation
    Edit Widget Size                       width=100       height=500    is_content=True
    Insert From ToolBox                    Logical View    Navigation
    Edit Widget Position                   top=0           left=0
    Edit Widget Size                       width=100       height=500
    FlaUILib.Click Toolbar Button          Save All
    Close Editor
    Expand and Click Tree Leaf             Logical View    BR_mappView|BR_Visualization|BR_Pages|BR_AreaContents    shortcut=0
    Insert From ToolBox                    Logical View    Page content
    Expand and Click Tree Leaf             Logical View    BR_mappView|BR_Visualization|BR_Pages|BR_AreaContents|BR_content_1.content    shortcut=0
    Wait Until Element Exist               ${xpath}
    Edit Widget Name                       Info_Pane
    Edit Widget Size                       height=100      is_content=True
    Click Into IDE
    Insert From ToolBox                    Logical View    Label
    Edit Widget Position                   top=5           left=50
    Edit Widget Size                       width=200       height=30
    Insert From ToolBox                    Logical View    LanguageSelector
    Edit Widget Position                   top=35          left=680
    FlaUILib.Click Toolbar Button          Save All
    Close Editor


Insert All Widget Types
    [Documentation]    Scenario: Insert complete widget catalog into visualization
    ...                Traceability ID: FW-MVIEW-D3
    ...                Component: mappView
    ...                Source Manual: agents/TM642-ENG_Diagnostics__charts__customized_widgets_V6000_mV65.md
    ...                Source Section: 4 Customized widgets
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Widget catalog file is loaded and editor is open
    [Tags]              mappview    widgets    comprehensive    regression    trace:fw-mview-d3    trace:tm642    trace:sec-4
    ${project_name}=    Set Variable    MappView_AllWidgets
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    AllWidgetsVisu
    Open Visualization For Editing    AllWidgetsVisu
    Insert All Widgets From Config
    Build Project
    Log    All widget types inserted and project built


Insert Button Widget And Configure Properties
    [Documentation]    Scenario: Insert button widget and set visual properties
    ...                Traceability ID: FW-MVIEW-D4
    ...                Component: mappView
    ...                Source Manual: agents/TM611-GER_Working_with_mapp_View_V6001_mV61.md
    ...                Source Section: 5.4 Content and Widgets
    ...                Evidence Type: Manual procedure
    ...                Determinism: Deterministic UI path
    ...                Preconditions: Visualization content editor is open
    [Tags]              mappview    widgets    properties    trace:fw-mview-d4    trace:tm611    trace:sec-5.4
    ${project_name}=    Set Variable    MappView_Props
    ${project_path}=    Set Variable    ${PROJECT_TEMP_PATH}${project_name}
    Create New Project In Automation Studio    ${project_name}    ${project_path}
    Initialize MappView Component
    Create Visualization Project    PropertyTestVisu
    Open Visualization For Editing    PropertyTestVisu
    Insert MappView Widget    Button    TestButton    BTN_001
    Configure Widget Property    Text    Click Me
    Configure Widget Property    BackgroundColor    0xFF0000
    Log    Widget properties configured successfully
