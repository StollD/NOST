using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Harmony;

namespace NOST
{
    internal class Program
    {
        /// <summary>
        /// The version of the Program
        /// </summary>
        public const String Version = "NOST v0.1";
        
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
                
                // Allow the program to load the OST dependency from a subfolder
                AssemblyResolver.Hook("OST");
                Assembly.LoadFrom("OST/OnlineUpdateTool.exe");
                Directory.SetCurrentDirectory("OST");

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