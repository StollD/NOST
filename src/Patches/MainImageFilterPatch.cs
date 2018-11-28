using System;
using Harmony;
using NOST.Utilities;

namespace NOST.Patches
{
    [HarmonyType("Params.ToolParam")]
    [HarmonyPatch("MainImageFilter", MethodType.Getter)]
    public class MainImageFilterPatch
    {
        public static Boolean Prefix(ref String __result)
        {
            // Always return true to show the options menu
            __result =  "image files (*.mlf; *.nb0; *.zip; *.qlz)|*.mlf;*.nb0;*.zip;*.qlz";
            return false;
        }
    }
}