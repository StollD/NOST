using System;
using Harmony;
using NOST.Utilities;

namespace NOST.Patches
{
    [HarmonyType("Params.ToolParam")]
    [HarmonyPatch("OptionVisible", MethodType.Getter)]
    public class OptionVisiblePatch
    {
        public static Boolean Prefix(ref Boolean __result)
        {
            // Always return true to show the options menu
            __result = true;
            return false;
        }
        
    }
}