namespace Solid.Cli.Specs.Tests.Contracts
{
    public interface IProcessManagementService
    {
        ExecutionInfo Start(string tool, string args, int? waitTime = null);
        void Stop(int processId);
    }

    public sealed class ExecutionInfo
    {
        public int ProcessId { get; set; }

        public string[] OutputStrings { get; set; }

        public string[] ErrorStrings { get; set; }

        public int ExitCode { get; set; }
    }
}
