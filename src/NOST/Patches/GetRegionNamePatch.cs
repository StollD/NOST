using System;
using System.Globalization;
using Harmony;
using Microsoft.Win32;
using NOST.Utilities;

namespace NOST.Patches
{
    [HarmonyType("Locales.Locale")]
    [HarmonyPatch("GetRegionName")]
    public class GetRegionNamePatch
    {
        public static Boolean Prefix(ref String __result)
        {
            RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\International");
            if (registryKey != null)
            {
                // OST crashes when the locale value is invalid, so lets default to english
                String value = registryKey.GetValue("Locale", "0409") as String;
                if (!string.IsNullOrEmpty(value))
                {
                    int cultureId = int.Parse(value, NumberStyles.AllowHexSpecifier);
                    CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
                    CultureInfo cultureInfo = Array.Find(cultures, culture => culture.LCID == cultureId);
                    if (cultureInfo != null)
                    {
                        String lower = cultureInfo.Name.ToLower();
                        if (lower.Equals("zh-cn"))
                        {
                            __result = "zh-cn";
                            return false;
                        }
                        String[] strArrays = lower.Split(new Char[] { '-' });
                        if ((Int32)strArrays.Length <= 0)
                        {
                            __result =  "en";
                            return false;
                        }
                        __result =  strArrays[0];
                        return false;
                    }
                }
                registryKey.Close();
            }
            __result =  "en";
            return false;
        }
    }
}