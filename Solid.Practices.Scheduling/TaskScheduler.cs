namespace Solid.Practices.Scheduling
{
    /// <summary>
    /// Ambient context for <see cref="System.Threading.Tasks.TaskScheduler"/>
    /// </summary>
    public static class TaskScheduler
    {
        private static System.Threading.Tasks.TaskScheduler _taskScheduler = System.Threading.Tasks.TaskScheduler.Default;
        /// <summary>
        /// Current value of <see cref="System.Threading.Tasks.TaskScheduler"/>.
        /// The value is settable to support the tests and advanced system setup.
        /// </summary>
        public static System.Threading.Tasks.TaskScheduler Current
        {
            get { return _taskScheduler; }
            set { _taskScheduler = value; }
        }
    }
}
