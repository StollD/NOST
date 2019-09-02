using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Harmony;
using MainForms;

namespace NOST.Patches
{
    [HarmonyPatch(typeof(Form1))]
    [HarmonyPatch("btnNext_Click")]
    public class BtnPhoneKeyInfoPatch
    {
        public static Boolean Prefix(ref Form1 __instance)
        {
            PropertyInfo deviceConnected =
                typeof(Form1).GetProperty("DeviceConnected", BindingFlags.Instance | BindingFlags.NonPublic);
            if ((Boolean) deviceConnected.GetValue(__instance, null))
            {
                Process p = new Process();
                p.StartInfo.FileName = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location),
                    "fastboot-android.exe");
                p.StartInfo.Arguments = "oem device-info";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardError = true;
                p.Start();
                p.WaitForExit();
                String output = p.StandardError.ReadToEnd();

                // Check if the bootloader is unlocked to critical
                if (output.Contains("Device critical unlocked: false"))
                {
                    MethodInfo showErrorMessage = typeof(Form1)
                        .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)
                        .FirstOrDefault(m =>
                            m.Name == "ShowErrorMessage" && m.GetParameters().Select(pa => pa.ParameterType)
                                .Contains(typeof(Int64)));
                    showErrorMessage.Invoke(__instance,
                        new Object[] {50723, null});

                    // Disable the next button, it does nothing
                    FieldInfo btnNext =
                        typeof(Form1).GetField("btnNext", BindingFlags.Instance | BindingFlags.NonPublic);
                    Button btn = (Button) btnNext.GetValue(__instance);
                    btn.Enabled = false;
                    btnNext.SetValue(__instance, btn);
                    return false;
                }
            }

            return true;
        }
    }
}