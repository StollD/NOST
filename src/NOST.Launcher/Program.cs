using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace NOST.Launcher
{
    public class Program
    {
        public static void Main(String[] args)
        {
            // Run as Admin
            if (!VistaSecurity.IsAdmin())
            {
                VistaSecurity.RestartElevated();
                return;
            }

            // Launch the OST Wrapper
            String ostDir = Path.Combine(Path.GetDirectoryName(typeof(Program).Assembly.Location), "OST");
            Process process = Process.Start(Path.Combine(ostDir, "NOST.exe"), String.Join(" ", args));

        }
    }
}
