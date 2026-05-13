using System;
using System.Collections.Generic;

namespace FlaUITests.Util.AS_Objects {
    public class MappViewObjects {
        public static readonly List<string> buttonDenominators = new List<string> {"ToggleSwitch", "ToggleButton", "RadioButton", "PushButton", "NavigationButton", "MomentaryPushButton", "HoverButton", "Checkbox", "Button"};
        public static readonly List<string> chartDenominators = new List<string> {"BarChart", "DonutChart", "LinearGauge", "LineChart", "OnlineChart", "OnlineChartHDA", "PieChart", "ProfileGenerator", "RadialGauge", "StackedBarChart", "Timeline", "XYChart"};
        public static readonly List<string> containerDenominators = new List<string> {"ButtonBar", "FlexBox", "FlexLayoutPanel", "FlyOut", "GridLine", "GroupBox", "InfoBanner", /*"Navigation",*/ "NavigationBar", "RadialButtonBar", "RadioButtonGroup", "TabControl", };
        public static readonly List<string> dataDenominators = new List<string> {"AlarmHistory", "AlarmLine", "AlarmList", "AuditList", "FavoriteWatch", "Table", "UserList", "Database"};
        public static readonly List<string> dateTimeDenominators = new List<string> {"DateTimeInput", "DateTimeOutput"};
        public static readonly List<string> drawingDenominators = new List<string> {"Ellipse", "Line", "Rectangle", "Paper"};
        public static readonly List<string> imageDenominators = new List<string> {"Image", "ImageList"};
        public static readonly List<string> loginDenominators = new List<string> {"Login", "LoginButton", "LoginInfo", "LogoutButton", "Password"};
        public static readonly List<string> mediaDenominators = new List<string> {"PDFViewer", "QRViewer", "VideoPlayer", "VNCViewer", "WebViewer"};
        public static readonly List<string> motionDenominators = new List<string> {"MotionPad"};
        public static readonly List<string> numericDenominators = new List<string> {"BasicSlider", "Joystick", "NumericInput", "NumericOutput", "ProgressBar", "RadialSlider", "RangeSlider", "XYJoystick"};
        public static readonly List<string> selectorDenominators = new List<string> {"DropDownBox", "ListBox", "TextPicker"};
        public static readonly List<string> systemDenominators = new List<string> {"KeyBoard", "LanguageSelector", "MeasurementSystemSelector", "MotionKeyPad", "NumPad", "SystemNavButton", "SystemLogin", "TextKeyPad", "DateTimePicker", "ContentControl", "ContentCarousel"};
        public static readonly List<string> textDenominators = new List<string> {"Label", "TextInput", "TextOutput", "TextPad"};
        public static readonly List<string> processDenominators = new List<string> {"Sequencer", "LadderEditor", "Skyline"};
        public static readonly bool [] toTestWidgetGroups = new bool[] {true, false, false, false, true, false, false, false, false, false, true, false, false, false, false};
        public static readonly List<List<string>> AllWidgets = new List<List<string>> {buttonDenominators, chartDenominators, containerDenominators, dataDenominators, dateTimeDenominators, drawingDenominators, imageDenominators, loginDenominators, mediaDenominators, motionDenominators, numericDenominators, selectorDenominators, systemDenominators, textDenominators, processDenominators};
        public List<MappViewPage> Pages;
        public bool [] ButtonValues = new bool[9];
        public string [][] ButtonValuesStrings;
        public DateTime [] DateTimeValues = new DateTime[2];
        public string [][] DateTimeValuesStrings;
        public float [] NumericValues = new float[6];
        public string [][] NumericValuesStrings;
        public float [][] Numeric2DValues = new float[2][];
        public string [][] Numeric2DValuesStrings;

    }
    public class MappViewPage {
        public string Name;
        public List<string[]> Widgets;
        public MappViewPage(string name, List<string[]> widgets) {
            Name = name;
            Widgets = widgets;
        }
    }
}