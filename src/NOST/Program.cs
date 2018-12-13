using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Harmony;
using MainForms;
using NOST.Utilities;

namespace NOST
{
    internal class Program
    {
        /// <summary>
        /// The version of the Program
        /// </summary>
        public const String Version = "NOST v0.5";

        /// <summary>
        /// A reference to the main form of OST
        /// </summary>
        public static Form1 OSTForm;
        
        [STAThread]
        public static void Main(String[] args)
        {
            try
            {
                // Run as Admin
                if (!VistaSecurity.IsAdmin())
                {
                    VistaSecurity.RestartElevated();
                    return;
                }

                // Trigger OST Load by referencing one of its types
                OSTForm = null;
                
                // Load locales
                LocaleOverride.Load();

                // Patching
                HarmonyInstance instance = HarmonyInstance.Create("io.tmsp.nost");
                instance.PatchAll(typeof(Program).Assembly);

                // Enable all the options that are only available when being logged in
                Type[] types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).ToArray();
                Type options = types.FirstOrDefault(t => t.Name == "OstOption" && t.Namespace == "Utils");
                if (options == null)
                {
                    throw new Exception("Couldn't start OST!");
                }
                MethodInfo enableOptions =
                    options.GetMethod("EnableAllOptions", BindingFlags.Public | BindingFlags.Static);
                if (enableOptions == null)
                {
                    throw new Exception("Couldn't start OST!");
                }
                enableOptions.Invoke(null, null);

                // Start OST
                Type OST = types.FirstOrDefault(t => t.Name == "Program" && t.Namespace == "OnlineUpdateTool");
                if (OST == null)
                {
                    throw new Exception("Couldn't start OST!");
                }
                MethodInfo info = OST.GetMethod("Main", BindingFlags.NonPublic | BindingFlags.Static);
                if (info == null)
                {
                    throw new Exception("Couldn't start OST!");
                }
                info.Invoke(null, null);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString(), "Error!", MessageBoxButtons.OK);
            }
        }
    }
}