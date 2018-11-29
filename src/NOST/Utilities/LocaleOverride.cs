using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;

namespace NOST.Utilities
{
    public class LocaleOverride
    {
        /// <summary>
        /// All overwritten texts that exist
        /// </summary>
        public static Dictionary<String, Dictionary<String, String>> Locales =
            new Dictionary<String, Dictionary<String, String>>();

        public static String GetLocale(String region, String id)
        {
            if (Locales.ContainsKey(region) && Locales[region].ContainsKey(id))
            {
                return Locales[region][id];
            }

            return id;
        }

        public static void Load()
        {
            // Find all locale files
            String localeDir =
                Path.Combine(Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), ".."), "Lang");
            foreach (String file in Directory.GetFiles(localeDir, "*.yml"))
            {
                String localeName = Path.GetFileNameWithoutExtension(file);
                Locales.Add(localeName,
                    new Deserializer().Deserialize<Dictionary<String, String>>(File.ReadAllText(file)));
            }
        }
    }
}