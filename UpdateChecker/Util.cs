using System;
using System.IO;

namespace UpdateChecker {
    public class Util {
        public static string getManagedPath() {
            return Path.Combine("RustDedicated_Data", "Managed");
        }
    }
}

