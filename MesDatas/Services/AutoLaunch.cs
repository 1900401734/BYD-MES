using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace MesDatas.Services
{
    public static class AutoLaunch
    {
        private const string Key = "SCADA";

        public static void AutoStart(bool enable)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                try
                {
                    if (enable)
                    {
                        key?.SetValue(Key, Application.ExecutablePath);
                    }
                    else
                    {
                        key?.DeleteValue(Key, false); // Corrected to delete the value when disabling
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
