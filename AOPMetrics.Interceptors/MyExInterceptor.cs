using System;

using Castle.DynamicProxy;
using log4net;

using AOPMetrics.Core.Interfaces;

namespace AOPMetrics.Interceptors
{
    public class MyExInterceptor : IInterceptor
    {
        private readonly ILog _logger;
        private readonly IAttributeScanEngine _attributeEngine;
        private static string _attrNamespace;

        public MyExInterceptor(string attrNamespace)
            : this(MyObjectFactory.GetInstance<ILog>(),
                MyObjectFactory.GetInstance<IAttributeScanEngine>(), attrNamespace)
        {
        }

        public MyExInterceptor(ILog logger, IAttributeScanEngine attributeEngine, string attrNamespace)
        {
            _logger = logger;
            _attributeEngine = attributeEngine;
            _attrNamespace = attrNamespace;
        }

        public void Intercept(IInvocation invocation)
        {
            var attributes = _attributeEngine.AutoFindAttribute("");

            foreach (var attribute in attributes)
            {
                var nameTemp = GetAttributeName(GetScanTaskName(attribute.FullName));

                var type = Type.GetType(nameTemp);

                if (type != null)
                {
                    var existedMethod = Attribute.GetCustomAttribute(invocation.GetConcreteMethodInvocationTarget(), type, false);

                    if (existedMethod != null)
                    {
                        var localAttribute = attribute;
                        CatchExceptionHelper.TryCatchAction(() => _attributeEngine.Run(invocation, localAttribute), _logger);
                    }
                }
            }
        }

        private static string GetScanTaskName(string fullName)
        {
            var result = fullName.Substring(fullName.LastIndexOf(".") + 1, fullName.Length - fullName.LastIndexOf(".") - 1);
            result = result.Substring(0, result.IndexOf("ScanTask"));

            return result;
        }

        private static string GetAttributeName(string scanTaskName)
        {
            return _attrNamespace + "." + scanTaskName + "Attribute";
        }
    }
}