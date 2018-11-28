using System;
using System.Linq;

namespace NOST.Utilities
{
    public class HarmonyType : Harmony.HarmonyPatch
    {
        public HarmonyType(String typeName)
        {
            Type[] types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).ToArray();
            info.declaringType = types.FirstOrDefault(t =>  typeName == t.Namespace + "." + t.Name);
        }   
    }
}