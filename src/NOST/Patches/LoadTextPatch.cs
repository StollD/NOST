using System;
using Harmony;
using NOST.Utilities;

namespace NOST.Patches
{
    [HarmonyType("Locales.Locale")]
    [HarmonyPatch("LoadText")]
    [HarmonyPatch(new [] { typeof(String), typeof(String)})]
    public class LoadTextPatch
    {
        // Add a way to override locales
        public static Boolean Prefix(ref String localeId, ref String regionName, ref String __result)
        {
            String localized = LocaleOverride.GetLocale(regionName, localeId);
            if (localized == localeId)
            {
                return true;
            }

            __result = localized;
            return false;
        }
    }
}