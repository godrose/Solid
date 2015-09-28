using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solid.Practices.Scheduling
{
    /// <summary>
    /// Custom task scheduler that runs all tasks in one thread
    /// </summary>
    public class SameThreadTaskScheduler : System.Threading.Tasks.TaskScheduler
    {
        public override int MaximumConcurrencyLevel
        {
            get { return 1; }
        }

        protected override void QueueTask(Task task)
        {
            TryExecuteTask(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            return TryExecuteTask(task);
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return Enumerable.Empty<Task>();
        }
    }
}
