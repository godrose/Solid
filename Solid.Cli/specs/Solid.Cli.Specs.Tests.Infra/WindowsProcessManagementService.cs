using System;
using System.IO;
using System.Threading;
using Common.Infra;
using JetBrains.Annotations;
using Solid.Cli.Specs.Tests.Contracts;

namespace Solid.Cli.Specs.Tests.Infra
{
    [UsedImplicitly]
    internal sealed class WindowsProcessManagementService : IProcessManagementService
    {
        //TODO: get from config.json. Use same source across all usages
        public ExecutionInfo Start(string tool, string args, int? pause = 2000)
        {
            var currentDir = Directory.GetCurrentDirectory();

            try
            {
                var fileName = Path.GetFileName(tool);
                EnsureCurrentDirectory(tool, fileName);

                var exitInfo = ProcessExtensions.LaunchApp(fileName, args);

                var result = new ExecutionInfo
                {
                    ProcessId = exitInfo.ProcessId,
                    OutputStrings = exitInfo.Output,
                    ErrorStrings = exitInfo.Errors.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries),
                    ExitCode = exitInfo.ExitCode
                };

                if (pause.HasValue && !exitInfo.IsError)
                {
                    Thread.Sleep(pause.Value);
                }

                return result;
            }
            finally
            {
                Directory.SetCurrentDirectory(currentDir);
            }
        }

        private static void EnsureCurrentDirectory(string tool, string fileName)
        {
            if (string.Compare(fileName, tool, StringComparison.OrdinalIgnoreCase) != 0)
            {
                var path = Path.GetDirectoryName(tool);
                // ReSharper disable once AssignNullToNotNullAttribute
                path = Path.GetFullPath(path);
                Directory.SetCurrentDirectory(path);
            }
        }

        public void Stop(int processId)
        {
            Action killAction = () => processId.KillProcessAndChildren();
            killAction.Execute();
        }
    }
}
