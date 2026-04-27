using System.Security.Policy;

namespace FlaUITests.Util {
    public static class Environment {
        public enum Verbose {NONE, LIGHT, STEPS, FULL}
        public static string InstallationPath;
        public static Verbose verbose;
    }
}
