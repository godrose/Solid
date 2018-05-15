using System;
using System.Linq;
using System.Reflection;
using FakeItEasy;
using FluentAssertions;
using Solid.Practices.Composition.Contracts;
using Solid.Practices.Modularity;
using Xunit;

namespace Solid.Practices.Composition.Container.NETStandard20.Tests
{
    public class SimpleCompositionContainerTests
    {
        [Fact]
        public void Compose_CantGetAssemblyDefinedTypes_AssemblyExceptionIsThrown()
        {
            var assemblies = new[] {Assembly.GetExecutingAssembly()};
            var stubTypeInfoExtractionService = A.Fake<ITypeInfoExtractionService>();
            A.CallTo(() => stubTypeInfoExtractionService.GetTypes(A<Assembly>._)).Throws<Exception>();

            var container = new SimpleCompositionContainer<ICompositionModule>(assemblies,
                stubTypeInfoExtractionService, null);
            var exception = Record.Exception(() => container.Compose());

            exception.Should().BeOfType<AggregateAssemblyInspectionException>().Which.InnerExceptions[0].Message.Should()
                .Be("Unable to load defined types");
        }

        [Fact]
        public void Compose_OneModuleCantBeLoaded_ModuleLoadingExceptionWithOneModuleInfoIsThrown()
        {
            var assemblies = new []{ Assembly.GetExecutingAssembly() };
            var stubTypeInfoExtractionService = A.Fake<ITypeInfoExtractionService>();
            var firstType = string.Empty.GetType().GetTypeInfo();
            var secondType = default(int).GetType().GetTypeInfo();
            var types = new[] {firstType, secondType};
            A.CallTo(() => stubTypeInfoExtractionService.GetTypes(A<Assembly>._)).Returns(types);
            A.CallTo(() => stubTypeInfoExtractionService.IsCompositionModule(firstType, typeof(ICompositionModule)))
                .Returns(true);
            A.CallTo(() => stubTypeInfoExtractionService.IsCompositionModule(secondType, typeof(ICompositionModule)))
                .Returns(true);           
            var stubCompositionModuleCreationStrategy = A.Fake<ICompositionModuleCreationStrategy>();
            A.CallTo(() => stubCompositionModuleCreationStrategy.CreateCompositionModule(firstType.AsType()))
                .Returns(A.Fake<ICompositionModule>());
            A.CallTo(() => stubCompositionModuleCreationStrategy.CreateCompositionModule(secondType.AsType()))
                .Throws<Exception>();

            var container = new SimpleCompositionContainer<ICompositionModule>(assemblies,
                stubTypeInfoExtractionService, stubCompositionModuleCreationStrategy);
            var exception = Record.Exception(() => container.Compose());

            exception.Should().BeOfType<AggregateAssemblyInspectionException>().Which.InnerExceptions[0].Message.Should()
                .Be("Unable to create composition modules");
            var moduleCreationException = exception.As<AggregateAssemblyInspectionException>().InnerExceptions[0]
                .As<AggregateModuleCreationException>().InnerExceptions[0];
            moduleCreationException.Type.Should().Be(secondType);
            moduleCreationException.Message.Should()
                .Be("Unable to create module for the specified type");
        }
    }
}
