using System;
using System.Reflection;

namespace Solid.Practices.Composition.Container
{
    /// <summary>
    /// Represents an exception that is thrown during modules composition.
    /// </summary>
    public class CompositionException : Exception
    {
        /// <summary>
        /// Creates a new instance of <see cref="CompositionException"/>
        /// </summary>
        /// <param name="message">The message.</param>
        public CompositionException(string message)
            :base(message)
        {
            
        }
    }

    /// <summary>
    /// Represents an exception that is thrown during assemblies' inspection.
    /// </summary>
    public class AggregateAssemblyInspectionException : Exception
    {
        /// <summary>
        /// The collection modules' inner exceptions.
        /// </summary>
        public Exception[] InnerExceptions { get; }

        /// <summary>
        /// Creates an instance of <see cref="AggregateAssemblyInspectionException"/>
        /// </summary>
        /// <param name="innerExceptions">The inner exceptions.</param>
        public AggregateAssemblyInspectionException(Exception[] innerExceptions)
            : base("Unable to load assemblies")
        {
            InnerExceptions = innerExceptions;
        }
    }

    /// <summary>
    /// Represents an exception that is thrown during assembly inspection
    /// </summary>
    public class AssemblyInspectionException : Exception
    {
        /// <summary>
        /// The assembly whose inspection resulted in the exception.
        /// </summary>
        public string AssemblyName { get; }

        /// <summary>
        /// Creates an instance of <see cref="AssemblyInspectionException"/>
        /// </summary>
        /// <param name="assemblyName">The assembly.</param>
        /// <param name="innerException">The inner exception.</param>
        public AssemblyInspectionException(string assemblyName, Exception innerException)
            : base("Unable to load defined types", innerException)
        {
            AssemblyName = assemblyName;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Message} for {AssemblyName}; Inner exception is: {InnerException}";
        }
    }

    /// <summary>
    /// Represents an exception that is thrown during modules' creation.
    /// </summary>
    public class AggregateModuleCreationException : Exception
    {
        /// <summary>
        /// The collection modules' inner exceptions.
        /// </summary>
        public ModuleCreationException[] InnerExceptions { get; }

        /// <summary>
        /// Creates an instance of <see cref="AggregateModuleCreationException"/>
        /// </summary>
        /// <param name="innerExceptions">The inner exceptions.</param>
        public AggregateModuleCreationException(ModuleCreationException[] innerExceptions)
            : base("Unable to create composition modules")
        {
            InnerExceptions = innerExceptions;
        }
    }

    /// <summary>
    /// Represents an exception that is thrown during module creation.
    /// </summary>
    public class ModuleCreationException : Exception
    {
        /// <summary>
        /// The module type.
        /// </summary>
        public TypeInfo Type { get; }

        /// <summary>
        /// Creates an instance of <see cref="ModuleCreationException"/>
        /// </summary>
        /// <param name="type">The module type.</param>
        /// <param name="innerException">The inner exception.</param>
        public ModuleCreationException(TypeInfo type, Exception innerException)
            : base("Unable to create module for the specified type", innerException)
        {
            Type = type;
        }
    }
}