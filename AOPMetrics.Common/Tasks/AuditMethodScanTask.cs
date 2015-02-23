using System;
using System.Reflection;

using Castle.DynamicProxy;
using log4net;

using AOPMetrics.Core.Attributes;
using AOPMetrics.Core.Interfaces;

namespace AOPMetrics.Core.Tasks
{
    public class AuditMethodScanTask : IAttributeScanTask
    {
        private readonly ILog _logger;

        public AuditMethodScanTask(ILog logger)
        {
            _logger = logger;
        }

        public void Run(IInvocation invocation, bool isFirstCall)
        {
            if (AttributeRecognize(invocation.Method) && isFirstCall)
            {
                CatchExceptionHelper.TryCatchAction(
                    () => AuditMethod(invocation.Proceed, invocation.Method.Name, isFirstCall),
                    _logger);
            }
        }

        public bool AttributeRecognize(ICustomAttributeProvider methodInfo)
        {
            var attribute = methodInfo.GetCustomAttributes(typeof(AuditMethodAttribute), true);

            if (attribute == null)
                return false;

            return attribute is AuditMethodAttribute[];
        }

        private static void AuditMethod(Action method, string methodName, bool isFirstCall)
        {
            //connect to database for audit this method call
            if (isFirstCall)
                method();
        }
    }
}