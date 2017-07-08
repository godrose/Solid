using System.Threading.Tasks;

namespace Solid.Practices.Scheduling
{
    /// <summary>
    /// Factory for creating instances of <see cref="TaskFactory"/> that uses <see cref="TaskScheduler.Current"/> as its scheduler
    /// </summary>
    public class TaskFactoryFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="TaskFactory"/> using <see cref="TaskScheduler.Current"/> as its scheduler
        /// </summary>
        /// <returns>Task Factory instance</returns>
        public static TaskFactory CreateTaskFactory()
        {
            return new TaskFactory(TaskScheduler.Current);
        }
    }
}
