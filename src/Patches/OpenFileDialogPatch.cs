using System.Windows.Forms;
using Harmony;

namespace NOST.Patches
{
    [HarmonyPatch(typeof(OpenFileDialog))]
    [HarmonyPatch(MethodType.Constructor)]
    public class OpenFileDialogPatch
    {
        public static void Postfix(ref OpenFileDialog __instance)
        {
            // If this isn't set to true the Dialog crashes the whole app
            // Call the Ghostbusters
            __instance.ShowHelp = true;
        }
    }
}