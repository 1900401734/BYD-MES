﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BulletinBoard
{
    static class Program
    {
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern int SetForegroundWindow(IntPtr hwnd);
        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x112;
        public const int SC_RESTORE = 0xF120;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process cur = Process.GetCurrentProcess();
            foreach (Process p in Process.GetProcesses())
            {
                if (p.Id == cur.Id) continue;
                if (p.ProcessName == cur.ProcessName)
                {
                    SetForegroundWindow(p.MainWindowHandle);
                    SendMessage(p.MainWindowHandle, WM_SYSCOMMAND, SC_RESTORE, 0);
                    return;
                }
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            /* Process current = Process.GetCurrentProcess();
             Process[] pp = Process.GetProcessesByName(current.ProcessName);
             foreach (Process p in pp)
             {
                 if (p.Id != current.Id)
                 {
                     if (p.MainModule.FileName == current.MainModule.FileName)
                     {
                         p.Kill();
                         break;
                     }
                 }
             }
             Application.EnableVisualStyles();
                 Application.SetCompatibleTextRenderingDefault(false);
                 Application.Run(new Form1());*/

        }
        
    }
}
