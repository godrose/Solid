using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using FluentAssertions;
using JetBrains.Annotations;
using Solid.Cli.Specs.Tests.Contracts;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Solid.Cli.Specs.Steps
{
    [Binding]
    public sealed class TemplateGenerationSteps
    {
        private readonly IProcessManagementService _processManagementService;

        public TemplateGenerationSteps(IProcessManagementService processManagementService)
        {
            _processManagementService = processManagementService;
        }

        [When(@"I install the template pack '(.*)' from local package")]
        public void WhenIInstallTheTemplatePackFromLocalPackage(string templatePackName)
        {
            //TODO: Use template pack name
            var execInfo = _processManagementService.Start("../../devops/install-template-pack", string.Empty, 30000);
            execInfo.ShouldBeSuccessful();
        }

        [When(@"I create a folder named '(.*)'")]
        public void WhenICreateAFolderNamed(string folderName)
        {
            var tempPath = Path.GetTempPath();
            var path = Path.Combine(tempPath, folderName);

            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Directory.CreateDirectory(path);
        }

        [When(@"I generate the code in folder named '(.*)' using '(.*)' template with the default options")]
        public void WhenIGenerateTheCodeInFolderNamedUsingTemplateWithTheDefaultOptions(string folderName,
            string shortName)
        {
            var tempPath = Path.GetTempPath();
            var path = Path.Combine(tempPath, folderName);

            var execInfo = _processManagementService.Start(Path.Combine(path, "dotnet"), $"new {shortName}", 30000);
            execInfo.ShouldBeSuccessful();
        }

        [Then(@"The template for '(.*)' is installed with the following parameters")]
        public void ThenTheTemplateForIsInstalledWithTheFollowingParameters(string shortName, Table table)
        {
            var expectedResult = table.CreateSet<TemplateAssertionData>().Single();
            var execInfo = _processManagementService.Start("dotnet", $"new {shortName} -l");
            execInfo.ShouldBeSuccessful();
            var lines = execInfo.OutputStrings;
            var dashLine = lines[1];
            var infoLine = lines[2];
            const int initStart = -1;
            const int initLength = 0;
            int start = initStart;
            int length = initLength;
            var words = new List<string>();
            for (int i = 0; i < dashLine.Length; i++)
            {
                if (dashLine[i] == ' ')
                {
                    if (length == initLength)
                    {
                        continue;
                    }
                }

                if (dashLine[i] == '-')
                {
                    if (start == initStart)
                    {
                        start = i;
                    }

                    length++;
                    if (i != dashLine.Length - 1)
                        continue;
                }

                words.Add(infoLine[new Range(new Index(start), new Index(start + length))].Trim());
                start = initStart;
                length = initLength;
            }

            var actualDescription = words[0];
            var actualShortName = words[1];
            var actualLanguages = words[2];
            var actualTags = words[3];
            actualDescription.Should().Be(expectedResult.Description);
            actualShortName.Should().Be(expectedResult.ShortName);
            actualLanguages.Should().Be(expectedResult.Languages);
            actualTags.Should().Be(expectedResult.Tags);
        }

        [Then(@"The folder '(.*)' contains valid composition module")]
        public void ThenTheFolderContainsValidCompositionModule(string folderName)
        {
            var tempPath = Path.GetTempPath();
            var path = Path.Combine(tempPath, folderName);
            var moduleFile = Path.Combine(path, "Module.cs");
            File.Exists(moduleFile).Should().BeTrue();
            var contents = File.ReadAllLines(moduleFile);
            string.Join(Environment.NewLine, contents).Should().Be(
                @"using Solid.Practices.IoC;
using Solid.Practices.Modularity;

namespace Solid.Cli.Modularity
{
    internal sealed class Module : ICompositionModule<IDependencyRegistrator>
    {
        public void RegisterModule(IDependencyRegistrator dependencyRegistrator)
        {
			//Add your registrations here
            //dependencyRegistrator
                //.AddSingleton<IContract, Implementation>();
        }
    }
}");
        }
    }

    [UsedImplicitly]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    class TemplateAssertionData
    {
        public string Description { get; set; }
        public string ShortName { get; set; }
        public string Languages { get; set; }
        public string Tags { get; set; }
    }
}
