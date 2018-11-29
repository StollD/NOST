using Harmony;
using MainForms;

namespace NOST.Patches
{
    [HarmonyPatch(typeof(Form1))]
    [HarmonyPatch("ReloadDefaultTexts")]
    public class WindowTitlePatch
    {
        public static void Postfix(ref Form1 __instance)
        {
            // Change the Window Title
            __instance.Text = Program.Version;
        }
    }
}