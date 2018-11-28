using System;
using Harmony;
using NOST.Utilities;

namespace NOST.Patches
{
    [HarmonyType("Params.ToolParam")]
    [HarmonyPatch("SearchBundledImage")]
    public class SearchBundledImagePatch
    {
        public static Boolean Prefix()
        {
            // Disable the function
            return false;
        }
    }
}