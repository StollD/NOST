using System;
using System.IO;
using Harmony;
using NOST.Utilities;

namespace NOST.Patches
{
    [HarmonyType("Utils.CLogs")]
    [HarmonyPatch("CreateInstance")]
    public class CLogsPatch
    {
        // Move the Logs to the same directory as the application and not somewhere on the system.
        public static Boolean Prefix(ref String appName, ref String logPath)
        {
            appName = Program.Version;
            String logDir = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "Logs");
            logPath = Path.Combine(logDir, Path.GetFileName(logPath));
            Directory.CreateDirectory(logDir);
            return true;
        }
    }
}