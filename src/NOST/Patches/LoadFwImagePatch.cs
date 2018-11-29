using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Harmony;
using NOST.Utilities;

namespace NOST.Patches
{
    [HarmonyType("PageControl.FwPageSelected")]
    [HarmonyPatch("LoadFwImage")]
    public class LoadFwImagePatch
    {
        public static Boolean Prefix(ref String fwPath)
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
                info.FileName = Path.Combine(Path.Combine(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), ".."), "Tools"), "exdupe.exe");
                info.Arguments = "-R " + fwPath + " " + directory;
                
                // Ask for confirmation
                DialogResult result = MessageBox.Show(
                    "Extracting the firmware image. This could take some time and the program might look like it froze. Please don't terminate it.",
                    "Firmware Unpacker", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.Cancel)
                {
                    return false;
                }
                
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
                    MessageBox.Show("Extracting the firmware failed, or it is invalid!", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return false;
                }
                
                // Update the selected firmware path
                fwPath = mlfPath;
                
                // Announce success
                MessageBox.Show("Successfully extracted the firmware!", "Success", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return true;
            }

            return true;
        }
    }
}