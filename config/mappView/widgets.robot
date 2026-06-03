*** Settings ***
Library            Collections

*** Variables ***
@{all_Widgets}
...    @{button_Widgets}
...    @{chart_Widgets}
...    @{container_Widgets}
...    @{data_Widgets}
...    @{dateTime_Widgets}
...    @{drawing_Widgets}
...    @{image_Widgets}
...    @{login_Widgets}
...    @{media_Widgets}
...    @{motion_Widgets}
...    @{numeric_Widgets}
...    @{selector_Widgets}
...    @{system_Widgets}
...    @{text_Widgets}
...    @{process_Widgets}
@{button_Widgets}
...    ToggleSwitch
...    ToggleButton
...    RadioButton
...    PushButton
...    NavigationButton
...    MomentaryPushButton
...    HoverButton
...    Checkbox
...    Button
@{chart_Widgets}
...    BarChart
...    DonutChart
...    LinearGauge
...    LineChart
...    OnlineChart
...    OnlineChartHDA
...    PieChart
...    ProfileGenerator
...    RadialGauge
...    StackedBarChart
...    Timeline
...    XYChart
@{container_Widgets}
...    ButtonBar
...    FlexBox
...    FlexLayoutPanel
...    FlyOut
...    GridLine
...    GroupBox
...    InfoBanner
...    NavigationBar
...    RadialButtonBar
...    RadioButtonGroup
...    TabControl
@{data_Widgets}
...    AlarmHistory
...    AlarmLine
...    AlarmList
...    AuditList
...    FavoriteWatch
...    Table
...    UserList
...    Database
@{dateTime_Widgets}
...    DateTimeInput
...    DateTimeOutput
@{drawing_Widgets}
...    Ellipse
...    Line
...    Rectangle
...    Paper
@{image_Widgets}
...    Image
...    ImageList
@{login_Widgets}
...    Login
...    LoginButton
...    LoginInfo
...    LogoutButton
...    Password
@{media_Widgets}
...    PDFViewer
...    QRViewer
...    VideoPlayer
...    VNCViewer
...    WebViewer
@{motion_Widgets}
...    MotionPad
@{numeric_Widgets}
...    BasicSlider
...    Joystick
...    NumericInput
...    NumericOutput
...    ProgressBar
...    RadialSlider
...    RangeSlider
...    XYJoystick
@{selector_Widgets}
...    DropDownBox
...    ListBox
...    TextPicker
@{system_Widgets}
...    KeyBoard
...    LanguageSelector
...    MeasurementSystemSelector
...    MotionKeyPad
...    NumPad
...    SystemNavButton
...    SystemLogin
...    TextKeyPad
...    DateTimePicker
...    ContentControl
...    ContentCarousel
@{text_Widgets}
...    Label
...    TextInput
...    TextOutput
...    TextPad
@{process_Widgets}
...    Sequencer
...    LadderEditor
...    Skyline