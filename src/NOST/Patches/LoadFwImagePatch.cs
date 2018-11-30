using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Harmony;
using MainForms;
using NOST.Utilities;

namespace NOST.Patches
{
    [HarmonyType("PageControl.FwPageSelected")]
    [HarmonyPatch("LoadFwImage")]
    public class LoadFwImagePatch
    {
        public static Boolean Prefix(ref String fwPath, ref Int64 __result)
        {
            // Check if the Image file is a .zip or .qlz file
            if (fwPath.EndsWith(".zip"))
            {
                // TODO: Add .zip file support
                return false;
            }

            if (fwPath.EndsWith(".qlz"))
            {
                String directory = Path.GetTempFileName().Replace(".tmp", "");

                // Extract the file using QuickLZ
                ProcessStartInfo info = new ProcessStartInfo();
                info.FileName =
                    Path.Combine(
                        Path.Combine(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), ".."),
                            "Tools"), "exdupe.exe");
                info.Arguments = "-R " + fwPath + " " + directory;

                // Extract the file
                Process decompressor = Process.Start(info);
                if (decompressor != null)
                {
                    decompressor.WaitForExit();
                }

                // Try to get the extracted .mlf file
                String mlfPath = Directory.GetFiles(directory, "*.mlf").FirstOrDefault();
                if (String.IsNullOrEmpty(mlfPath))
                {
                    __result = 50718;
                    return false;
                }

                // Update the selected firmware path
                fwPath = mlfPath;
            }

            return true;
        }
    }
}