using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Xunit;

namespace Solid.Practices.Composition.Tests
{
    public class RuntimeAssemblyLoaderTests : IDisposable
    {
        private readonly string _folder =
            Path.Combine(Path.GetTempPath(), "solid-loader-" + Guid.NewGuid().ToString("N"));
        private readonly string _suffix = Guid.NewGuid().ToString("N").Substring(0, 8);
        private readonly string _previousDirectory = Directory.GetCurrentDirectory();

        public RuntimeAssemblyLoaderTests()
        {
            Directory.CreateDirectory(_folder);
            Directory.SetCurrentDirectory(_folder);
        }

        public void Dispose()
        {
            Directory.SetCurrentDirectory(_previousDirectory);
            try { Directory.Delete(_folder, true); } catch (IOException) { }
        }

        [Fact]
        public void Dependency_next_to_the_module_is_resolved()
        {
            var module = BuildModuleWithDependency(dependencyFolder: _folder);

            Run(RuntimeAssemblyLoader.Get(new[] { module }).Single()).Should().Be("from-dependency");
        }

        [Fact]
        public void Assembly_that_cannot_be_found_is_still_reported_as_missing()
        {
            var module = BuildModuleWithDependency(dependencyFolder: _folder);
            File.Delete(Path.Combine(_folder, $"Dep{_suffix}.dll"));

            Action run = () => Run(RuntimeAssemblyLoader.Get(new[] { module }).Single());

            run.Should().Throw<TargetInvocationException>()
                .WithInnerException<FileNotFoundException>();
        }

        private string BuildModuleWithDependency(string dependencyFolder)
        {
            var depName = "Dep" + _suffix;
            var moduleName = "Module" + _suffix;
            var dependency = Compile(depName,
                "public static class Dep { public static string Value() => \"from-dependency\"; }");
            File.WriteAllBytes(Path.Combine(dependencyFolder, depName + ".dll"), dependency);
            var module = Compile(moduleName,
                "public static class Entry { public static string Run() => Dep.Value(); }",
                MetadataReference.CreateFromImage(dependency));
            File.WriteAllBytes(Path.Combine(_folder, moduleName + ".dll"), module);
            return moduleName + ".dll";
        }

        private static string Run(Assembly module) =>
            (string)module.GetType("Entry").GetMethod("Run").Invoke(null, null);

        private static byte[] Compile(string name, string code, params MetadataReference[] extra)
        {
            var trusted = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES"))
                .Split(Path.PathSeparator)
                .Select(p => (MetadataReference)MetadataReference.CreateFromFile(p));
            var compilation = CSharpCompilation.Create(name,
                new[] { CSharpSyntaxTree.ParseText(code) },
                trusted.Concat(extra),
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
            using var stream = new MemoryStream();
            var result = compilation.Emit(stream);
            result.Success.Should().BeTrue(string.Join(";", result.Diagnostics));
            return stream.ToArray();
        }
    }
}
