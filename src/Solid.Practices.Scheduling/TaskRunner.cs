using System;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Practices.Scheduling
{
    /// <summary>
    /// Used as a proxy for running tasks using <see cref="TaskScheduler.Current"/>
    /// </summary>
    public static class TaskRunner
    {
        private static readonly TaskFactory Factory = TaskFactoryFactory.CreateTaskFactory();

        /// <summary>
        /// Gets the associated task factory's cancellation token.
        /// </summary>
        public static CancellationToken CancellationToken => Factory.CancellationToken;

        /// <summary>
        /// Runs a task associated with method without return value using <see cref="TaskScheduler.Current"/>
        /// </summary>
        /// <param name="action">Method that's run within the task</param>
        /// <returns>Running task</returns>
        public static Task RunAsync(Action action) => Factory.StartNew(action);

        /// <summary>
        /// Runs a task associated with method with return value using <see cref="TaskScheduler.Current"/>
        /// </summary>
        /// <typeparam name="TResult">Type of return value</typeparam>
        /// <param name="func">Method that's run within the task</param>
        /// <returns>Running task</returns>
        public static Task<TResult> RunAsync<TResult>(Func<TResult> func) => Factory.StartNew(func);
    }
}
