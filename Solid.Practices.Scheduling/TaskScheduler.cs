namespace Solid.Practices.Scheduling
{
    public static class TaskScheduler
    {
        private static System.Threading.Tasks.TaskScheduler _taskScheduler = System.Threading.Tasks.TaskScheduler.Default;
        public static System.Threading.Tasks.TaskScheduler Current
        {
            get { return _taskScheduler; }
            set { _taskScheduler = value; }
        }
    }
}
