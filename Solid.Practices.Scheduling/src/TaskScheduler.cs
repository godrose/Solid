namespace Solid.Practices.Scheduling
{
    /// <summary>
    /// Ambient context for <see cref="System.Threading.Tasks.TaskScheduler"/>
    /// </summary>
    public static class TaskScheduler
    {
        /// <summary>
        /// Current value of <see cref="System.Threading.Tasks.TaskScheduler"/>.
        /// The value is settable to support the tests and advanced system setup.
        /// </summary>
        public static System.Threading.Tasks.TaskScheduler Current { get; set; } =
            System.Threading.Tasks.TaskScheduler.Default;
    }
}
