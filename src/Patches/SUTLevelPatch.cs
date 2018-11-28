using System;
using Harmony;
using NOST.Utilities;

namespace NOST.Patches
{
    [HarmonyType("MyResources.Properties.Settings")]
    [HarmonyPatch("SutLevel", MethodType.Getter)]
    public class SUTLevelPatch
    {
        public static Boolean Prefix(ref Int32 __result)
        {
            // Always return access level 99 to bypass the login prompt
            __result = 99;
            return false;
        }
    }
}