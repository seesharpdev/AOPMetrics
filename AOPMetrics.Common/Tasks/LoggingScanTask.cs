using System;
using System.Reflection;

using Castle.DynamicProxy;
using log4net;

using AOPMetrics.Core.Attributes;
using AOPMetrics.Core.Interfaces;

namespace AOPMetrics.Core.Tasks
{
    public class LoggingScanTask : IAttributeScanTask
    {
        private readonly ILog _logger;

        public LoggingScanTask()
        {
        }

        public LoggingScanTask(ILog logger)
        {
            _logger = logger;
        }

        public void Run(IInvocation invocation, bool isFirstCall)
        {
            if (AttributeRecognize(invocation.Method))
            {
                CatchExceptionHelper.TryCatchAction(
                    () => LogInformation(invocation.Proceed, invocation.Method.Name, isFirstCall),
                    _logger);
            }
        }

        public bool AttributeRecognize(ICustomAttributeProvider methodInfo)
        {
            var attribute = methodInfo.GetCustomAttributes(typeof(LoggingAttribute), true);

            if (attribute == null)
            {
                return false;
            }

            return attribute is LoggingAttribute[];
        }

        private void LogInformation(Action method, string methodName, bool isFirstCall)
        {
            var startTime = DateTime.Now;

            if (isFirstCall)
            {
                method();
            }

            var endTime = DateTime.Now;
            var duration = endTime.Subtract(startTime).TotalSeconds.ToString("N3");

            var msg = string.Format("Duration of raw {0}(): {1}s",
                methodName, duration);

            _logger.Info(msg);
        }
    }
}