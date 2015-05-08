using System.Threading.Tasks;

namespace Solid.Practices.Scheduling
{
    public class TaskFactoryFactory
    {
        public static TaskFactory CreateTaskFactory()
        {
            return new TaskFactory(TaskScheduler.Current);
        }
    }
}
