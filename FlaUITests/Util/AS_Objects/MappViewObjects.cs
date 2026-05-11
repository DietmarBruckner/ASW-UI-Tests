using System;
using System.Collections.Generic;

namespace FlaUITests.Util.AS_Objects {
    public class MappViewObjects {
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