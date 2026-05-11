using System.Collections.Generic;

namespace FlaUITests.Util.AS_Objects {
    public class MappViewObjects {
        public List<MappViewPage> Pages;
        public bool [] ButtonValues = new bool [9];
        public string [][] ButtonValuesStrings;
    }
    public class MappViewPage {
        public string Name;
        public List<string> Widgets;
        public MappViewPage(string name, List<string> widgets) {
            Name = name;
            Widgets = widgets;
        }
    }
}