using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using JetBrains.Annotations;
using Solid.Cli.Specs.Tests.Contracts;

namespace Solid.Cli.Specs.Tests.Infra
{
    [UsedImplicitly]
    internal sealed class WindowsProcessManagementService : IProcessManagementService
    {
        public ExecutionInfo Start(string tool, string args, int? waitTime = null)
        {
            var currentDir = Directory.GetCurrentDirectory();

            try
            {
                var fileName = Path.GetFileName(tool);
                if (string.Compare(fileName, tool, StringComparison.OrdinalIgnoreCase) != 0)
                {
                    var path = Path.GetDirectoryName(tool);
                    // ReSharper disable once AssignNullToNotNullAttribute
                    path = Path.GetFullPath(path);
                    Directory.SetCurrentDirectory(path);
                }

                var outputStrings = new List<string>();
                var errorStrings = new List<string>();

                var processInfo = new ProcessStartInfo("cmd.exe", $"/c {fileName} {args}")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true
                };

                var process = Process.Start(processInfo);

                // ReSharper disable once PossibleNullReferenceException
                process.OutputDataReceived += (sender, e) =>
                {
                    if (e.Data == null)
                    {
                        return;
                    }
                    outputStrings.Add(e.Data);
                    Debug.WriteLine("output>>" + e.Data);
                    Console.WriteLine(e.Data);
                };
                process.BeginOutputReadLine();

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (e.Data == null)
                    {
                        return;
                    }
                    errorStrings.Add(e.Data);
                    Debug.WriteLine("error>>" + e.Data);
                    Console.WriteLine(e.Data);
                };
                process.BeginErrorReadLine();

                if (waitTime.HasValue)
                {
                    process.WaitForExit(waitTime.Value);
                }
                else
                {
                    process.WaitForExit();
                }

                var result = new ExecutionInfo
                {
                    ProcessId = process.Id,
                    OutputStrings = outputStrings.ToArray(),
                    ErrorStrings = errorStrings.ToArray(),
                    ExitCode = process.ExitCode
                };

                process.Close();

                return result;
            }
            finally
            {
                Directory.SetCurrentDirectory(currentDir);
            }
        }

        public void Stop(int processId)
        {
            Action killAction = () => processId.KillProcessAndChildren();
            killAction.Execute();
        }
    }
}
