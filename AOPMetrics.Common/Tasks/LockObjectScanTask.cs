using System;
using System.Reflection;

using Castle.DynamicProxy;
using log4net;

using AOPMetrics.Core.Attributes;
using AOPMetrics.Core.Interfaces;

namespace AOPMetrics.Core.Tasks
{
    public class LockObjectScanTask : IAttributeScanTask
    {
        private readonly ILog _logger;
        private static readonly object Locker = new object();

        public LockObjectScanTask()
        {
        }

        public LockObjectScanTask(ILog logger)
        {
            _logger = logger;
        }

        public void Run(IInvocation invocation, bool isFirstCall)
        {
            if (AttributeRecognize(invocation.Method) && isFirstCall)
            {
                CatchExceptionHelper.TryCatchAction(
                    () => LockMethod(invocation.Proceed, invocation.Method.Name, isFirstCall),
                    _logger);
            }
        }

        public bool AttributeRecognize(ICustomAttributeProvider methodInfo)
        {
            var attribute = methodInfo.GetCustomAttributes(typeof(LockObjectAttribute), true);
            if (attribute == null)
            {
                return false;
            }

            return attribute is LockObjectAttribute[];
        }

        private static void LockMethod(Action method, string methodName, bool isFirstCall)
        {
            lock (Locker)
            {
                if (isFirstCall)
                {
                    method();
                }
            }
        }
    } 
}
