using System;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Practices.Scheduling
{
    /// <summary>
    /// Used as a proxy for running tasks using <see cref="TaskScheduler.Current"/>
    /// </summary>
    class AsyncProvider
    {
        /// <summary>
        /// Runs a task associated with method without return value using <see cref="TaskScheduler.Current"/>
        /// </summary>
        /// <param name="action">Method that's run within the task</param>
        /// <returns>Running task</returns>
        public static Task RunAsync(Action action)
        {
            return Task.Factory.StartNew(action, new CancellationToken(), new TaskCreationOptions(),
                TaskScheduler.Current);
        }

        /// <summary>
        /// Runs a task associated with method with return value using <see cref="TaskScheduler.Current"/>
        /// </summary>
        /// <typeparam name="T">Type of return value</typeparam>
        /// <param name="method">Method that's run within the task</param>
        /// <returns>Running task</returns>
        public static Task RunAsync<T>(Func<T> method)
        {
            return Task.Factory.StartNew(method, new CancellationToken(), new TaskCreationOptions(),
                TaskScheduler.Current);
        }
    }
}
