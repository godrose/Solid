using System;
using System.Linq;
using System.Threading.Tasks;

namespace Solid.Cli.Specs.Tests.Infra
{
    public static class DelegateExtensions
    {
        public static void Execute(this Action action) =>
            Execute<Exception>(action, 20, TimeSpan.FromMilliseconds(200));

        public static void Execute<TException>(this Action action)
            where TException : Exception => Execute<TException>(action, 20, TimeSpan.FromMilliseconds(200));

        public static TResult ExecuteWithResult<TException, TResult>(this Func<TResult> func,
            Func<TResult, string> valueExtractor = null, string[] unacceptedValues = null)
            where TException : Exception => ExecuteWithResult<TException, TResult>(func, 20,
            TimeSpan.FromMilliseconds(200), valueExtractor, unacceptedValues);

        public static TResult ExecuteWithResult<TResult>(this Func<TResult> func,
            Func<TResult, string> valueExtractor = null, string[] unacceptedValues = null) =>
            ExecuteWithResult<Exception, TResult>(func, 20, TimeSpan.FromMilliseconds(200), valueExtractor,
                unacceptedValues);

        public static TResult ExecuteWithResult<TResult>(this Func<TResult> func, int numberOfRetries,
            TimeSpan waitingInterval) => ExecuteWithResult<Exception, TResult>(func, numberOfRetries, waitingInterval);

        public static void Execute<TException>(this Action action, int numberOfRetries, TimeSpan waitingInterval)
            where TException : Exception
        {
            TException lastException = null;
            for (int i = 0; i < numberOfRetries; i++)
            {
                try
                {
                    action();
                    return;
                }
                catch (TException ex)
                {
                    lastException = ex;
                    Task.Delay(waitingInterval);
                }
            }
            if (lastException != null)
            {
                throw lastException;
            }
        }

        public static TResult ExecuteWithResult<TException, TResult>(
            this Func<TResult> func,
            int numberOfRetries,
            TimeSpan waitingInterval,
            Func<TResult, string> valueExtractor = null,
            string[] unacceptedValues = null)
            where TException : Exception
        {
            TException lastException = null;
            for (int i = 0; i < numberOfRetries; i++)
            {
                try
                {
                    var result = func();
                    if (valueExtractor != null && unacceptedValues != null)
                    {
                        var value = valueExtractor(result);
                        if (unacceptedValues.Contains(value))
                        {
                            throw new Exception($"Unaccepted value {value}");
                        }
                    }
                    return result;
                }
                catch (TException ex)
                {
                    lastException = ex;
                    Task.Delay(waitingInterval);
                }
            }
            if (lastException != null)
            {
                throw lastException;
            }
            return default(TResult);
        }
    }
}
