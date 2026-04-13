using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;

namespace FlaUITests.Util {
    /// <summary>
    /// Represents a single module from the Hardware.hwl configuration
    /// </summary>
    public class HardwareModule {
        public string Name { get; set; }
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString() {
            return $"Module: Name={Name}, Type={Type}, X={X}, Y={Y}";
        }
    }
    public class HardwareLink {
        public HardwareModule From { get; set; }
        public HardwareModule To { get; set; }  
        public List<System.Drawing.Point> Points { get; set; }
        public string FromPort { get; set; }
        public string ToPort { get; set; }
    }
    public class HardwareTopology {
            public List<HardwareModule> Modules { get; set; }
            public List<HardwareLink> Links { get; set; }
        }
    /// <summary>
    /// Reads and parses the Hardware.hwl XML file
    /// </summary>
    public class HardwareConfigReader
    {
        private string _hardwareFilePath;

        /// <summary>
        /// Initializes a new instance with the hardware config file path
        /// </summary>
        public HardwareConfigReader(string projectFolder, string configString)
        {
            if (string.IsNullOrEmpty(projectFolder))
                throw new ArgumentException("Project folder cannot be null or empty", nameof(projectFolder));

            _hardwareFilePath = System.IO.Path.Combine(projectFolder, "Physical", configString, "Hardware.hwl");
        }

        /// <summary>
        /// Reads and parses all modules from the Hardware.hwl file
        /// </summary>
        public List<HardwareModule> ReadModules()
        {
            List<HardwareModule> modules = new List<HardwareModule>();

            if (!System.IO.File.Exists(_hardwareFilePath))
            {
                Console.WriteLine($"Warning: Hardware.hwl file not found at path: {_hardwareFilePath}");
                return modules;
            }

            try
            {
                XDocument doc = XDocument.Load(_hardwareFilePath);
                XElement root = doc.Root;

                // Find BR.AS.HardwareTopology element
                XElement hardwareTopology = root;
                if (hardwareTopology == null)
                {
                    Console.WriteLine("Warning: BR.AS.HardwareTopology element not found in Hardware.hwl");
                    return modules;
                }

                // Find Modules subelement
                XElement modulesElement = hardwareTopology.Element("Modules");
                if (modulesElement == null)
                {
                    Console.WriteLine("Warning: Modules element not found in BR.AS.HardwareTopology");
                    return modules;
                }

                // Parse each Module element
                foreach (XElement moduleElement in modulesElement.Elements("Module"))
                {
                    HardwareModule module = new HardwareModule();

                    // Read attributes or child elements
                    XAttribute nameAttr = moduleElement.Attribute("Name");
                    if (nameAttr != null)
                        module.Name = nameAttr.Value;

                    XAttribute typeAttr = moduleElement.Attribute("Type");
                    if (typeAttr != null)
                        module.Type = typeAttr.Value;

                    XAttribute xAttr = moduleElement.Attribute("X");
                    if (xAttr != null && int.TryParse(xAttr.Value, out int x))
                        module.X = x;

                    XAttribute yAttr = moduleElement.Attribute("Y");
                    if (yAttr != null && int.TryParse(yAttr.Value, out int y))
                        module.Y = y;

                    modules.Add(module);
                }

                Console.WriteLine($"Successfully read {modules.Count} modules from Hardware.hwl");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading Hardware.hwl: {ex.Message}");
            }

            return modules;
        }
        /// <summary>
        /// Reads and parses all links from the Hardware.hwl file
        /// </summary>
        public List<HardwareLink> ReadLinks(List<HardwareModule> modules = null) {
            List<HardwareLink> links = new List<HardwareLink>();
            if (!System.IO.File.Exists(_hardwareFilePath)) {
                Console.WriteLine($"Warning: Hardware.hwl file not found at path: {_hardwareFilePath}");
                return links;
            }
            try {
                XDocument doc = XDocument.Load(_hardwareFilePath);
                XElement root = doc.Root;
                // Find BR.AS.HardwareTopology element
                XElement hardwareTopology = root;
                if (hardwareTopology == null) {
                    Console.WriteLine("Warning: BR.AS.HardwareTopology element not found in Hardware.hwl");
                    return links;
                }
                // Find Links subelement
                XElement linksElement = hardwareTopology.Element("Links");
                if (linksElement == null) {
                    Console.WriteLine("Warning: Links element not found in BR.AS.HardwareTopology");
                    return links;
                }

                // Parse each Link element
                foreach (XElement linkElement in linksElement.Elements("Link")) {
                    HardwareLink link = new HardwareLink();
                    // Read attributes or child elements
                    XAttribute fromAttr = linkElement.Attribute("From");
                    if (fromAttr != null) {
                        if (modules != null) {
                            link.From = modules.Find(m => m.Name == fromAttr.Value);
                            if (link.From == null)
                                Console.WriteLine($"Warning: Link From module '{fromAttr.Value}' not found in modules list");
                        }
                    }
                    XAttribute toAttr = linkElement.Attribute("To");
                    if (toAttr != null) {
                        if (modules != null) {
                            link.To = modules.Find(m => m.Name == toAttr.Value);
                            if (link.To == null)
                                Console.WriteLine($"Warning: Link To module '{toAttr.Value}' not found in modules list");
                        }
                    }
                    XAttribute fromportAttr = linkElement.Attribute("FromPort");
                    if (fromportAttr != null)
                        link.FromPort = fromportAttr.Value;
                    XAttribute toportAttr = linkElement.Attribute("ToPort");
                    if (toportAttr != null)
                        link.ToPort = toportAttr.Value;
                    List<System.Drawing.Point> Points = new List<System.Drawing.Point>();
                    foreach (XElement pointElement in linkElement.Elements("Point")) {
                        // Parse each Point element
                        XAttribute pointXAttr = pointElement.Attribute("X");
                        XAttribute pointYAttr = pointElement.Attribute("Y");
                        if (pointXAttr != null && pointYAttr != null && int.TryParse(pointXAttr.Value, out int pointX) && int.TryParse(pointYAttr.Value, out int pointY))
                            Points.Add(new System.Drawing.Point(pointX, pointY));
                    }
                    link.Points = Points;
                    links.Add(link);
                }
                Console.WriteLine($"Successfully read {links.Count} links from Hardware.hwl");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error reading Hardware.hwl: {ex.Message}");
            }
            return links;
        }
        /// <summary>
        /// return HW topology from Hardware.hwl file
        /// </summary>        
        public HardwareTopology ReadHardwareTopology() {
            List<HardwareModule> Modules = ReadModules();
            return new HardwareTopology {
                Modules = Modules,
                Links = ReadLinks(Modules)
            };
        }
        /// <summary>
        /// Gets the full path to the Hardware.hwl file
        /// </summary>
        public string GetHardwareFilePath()
        {
            return _hardwareFilePath;
        }
    }
}
