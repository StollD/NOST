using System;
using System.Collections.Generic;
using System.Reflection;
using Harmony;
using MainForms;

namespace NOST.Patches
{
    [HarmonyPatch(typeof(Form1))]
    [HarmonyPatch("GetOptionList")]
    public class GetOptionListPatch
    {
        public static void Postfix(ref Form1 __instance, ref List<String> __result)
        {
            FieldInfo product = typeof(Form1).GetField("product", BindingFlags.Instance | BindingFlags.NonPublic);
            Object productValue = product.GetValue(__instance);
            MethodInfo hasUserOption =
                product.FieldType.GetMethod("HasUserOption", BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo securityVersion =
                product.FieldType.GetProperty("SecurityVersion", BindingFlags.Instance | BindingFlags.Public);
            Boolean HasUserOption = (Boolean) hasUserOption.Invoke(productValue, new Object[] {12});
            String SecurityVersion = (String) securityVersion.GetValue(productValue, null);
            
            if (HasUserOption && SecurityVersion == "0x0001")
            {
                __result.RemoveAt(__result.Count - 1);
            }
        }
    }
}