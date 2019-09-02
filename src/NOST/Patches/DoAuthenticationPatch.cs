using Harmony;
using NOST.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NOST.Patches
{
    [HarmonyType("UserForms.UserInterAction")]
    [HarmonyPatch("AcquireAuthentication")]
    public class DoAuthenticationPatch
    {
        public static Boolean Prefix(ref Boolean __result)
        {
            __result = true;
            return false;
        }
    }
}
