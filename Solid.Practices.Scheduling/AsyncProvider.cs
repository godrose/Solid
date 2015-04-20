using System;
using System.Threading;
using System.Threading.Tasks;

namespace Solid.Practices.Scheduling
{
    class AsyncProvider
    {
        public static Task RunAsync(Action action)
        {
            return Task.Factory.StartNew(action, new CancellationToken(), new TaskCreationOptions(),
                TaskScheduler.Current);
        }

        public static Task RunAsync<T>(Func<T> method)
        {
            return Task.Factory.StartNew(method, new CancellationToken(), new TaskCreationOptions(),
                TaskScheduler.Current);
        }
    }
}
