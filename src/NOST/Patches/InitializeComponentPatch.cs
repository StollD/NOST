using System;
using System.Reflection;
using System.Windows.Forms;
using Harmony;
using MainForms;

namespace NOST.Patches
{
    [HarmonyPatch(typeof(Form1))]
    [HarmonyPatch("InitializeComponent")]
    public class InitializeComponentPatch
    {
        public static Boolean Prefix(ref Form1 __instance)
        {
            // Capture the form
            Program.OSTForm = __instance;
            return true;
        }

        public static void Postfix(ref Form1 __instance)
        {
            FieldInfo tabPageSelectedFw =
                typeof(Form1).GetField("tabPageSelectedFw", BindingFlags.Instance | BindingFlags.NonPublic);
            FieldInfo btnPhoneKeyInfo =
                typeof(Form1).GetField("btnPhoneKeyInfo", BindingFlags.Instance | BindingFlags.NonPublic);
            TabPage page = (TabPage) tabPageSelectedFw.GetValue(__instance);
            page.Controls.Remove((Control) btnPhoneKeyInfo.GetValue(__instance));
            tabPageSelectedFw.SetValue(__instance, page);
        }
    }
}