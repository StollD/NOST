using System;
using System.IO;
using Harmony;
using NOST.Utilities;

namespace NOST.Patches
{
    [HarmonyType("Utils.PathEx")]
    [HarmonyPatch("GetModulePath")]
    [HarmonyPatch(new Type[0])]
    public class GetModulePathPatch
    {
        public static void Postfix(ref String __result)
        {
            // Append the OST folder to the end of the module path
            __result = Path.Combine(__result, "OST");
        }
    }
}