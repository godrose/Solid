using System;
using FluentAssertions;
using Solid.Cli.Specs.Tests.Contracts;

namespace Solid.Cli.Specs.Steps
{
    public static class ExecutionInfoExtensions
    {
        public static void ShouldBeSuccessful(this ExecutionInfo executionInfo)
        {
            executionInfo.Should().NotBeNull();
            executionInfo.ExitCode.Should().Be(0, string.Join(Environment.NewLine, executionInfo.ErrorStrings));
            //executionInfo.ErrorStrings.Should().BeEmpty();
        }
    }
}
