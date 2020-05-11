using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Management;

namespace Solid.Cli.Specs.Tests.Infra
{
    public static class ProcessExtensions
    {
        public static void KillProcessAndChildren(this Process process)
        {
            KillProcessAndChildrenImpl(process?.Id ?? 0);
        }

        public static void KillProcessAndChildren(this int pid)
        {
            KillProcessAndChildrenImpl(pid);
        }

        private static void KillProcessAndChildrenImpl(int pid)
        {
            // Cannot close 'system idle process'.
            if (pid == 0)
            {
                return;
            }

            ManagementObjectSearcher searcher = new ManagementObjectSearcher
                ("Select * From Win32_Process Where ParentProcessID=" + pid);
            ManagementObjectCollection moc = searcher.Get();
            foreach (var mo in moc)
            {
                KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
            }

            try
            {
                Process proc = Process.GetProcessById(pid);
                proc.Kill();
            }
            catch (ArgumentException)
            {
                // Process already exited.
            }
            catch (Win32Exception)
            {
                // TODO: Handle Access is denied case
            }
        }
    }
}
