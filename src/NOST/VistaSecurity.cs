using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace NOST
{
	public static class VistaSecurity
	{
		internal static bool IsAdmin()
		{
			return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
		}

		internal static void RestartElevated()
		{
			ProcessStartInfo processStartInfo = new ProcessStartInfo()
			{
				UseShellExecute = true,
				WorkingDirectory = Environment.CurrentDirectory,
				FileName = Application.ExecutablePath,
				Verb = "runas"
			};
			try
			{
				Process.Start(processStartInfo);
				Application.Exit();
			}
			catch (Exception)
			{
			}
		}
	}
}