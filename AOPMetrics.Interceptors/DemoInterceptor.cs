using System;

using Castle.DynamicProxy;
using log4net;

using AOPMetrics.Core;
using AOPMetrics.Core.Interfaces;

namespace AOPMetrics.Interceptors
{
    public class DemoInterceptor : IInterceptor
    {
        #region Private Members

        private readonly ILog _logger;
        private readonly IAttributeScanEngine _attributeScanEngine;
        private static string _attributeNamespace;

        #endregion

        #region Ctor's

        public DemoInterceptor(string attributeNamespace) // : this(Container.Resolve<ILog>(), Container.Resolve<IAttributeScanEngine>())
        {
        }

        public DemoInterceptor(ILog logger, IAttributeScanEngine attributeScanEngine, string attributeNamespace)
        {
            _logger = logger;
            _attributeScanEngine = attributeScanEngine;
            _attributeNamespace = attributeNamespace;
        }

        #endregion

        public void Intercept(IInvocation invocation)
        {
            var attributes = _attributeScanEngine.AutoFindAttribute("");
            foreach (var attribute in attributes)
            {
                var tempName = GetAttributeName(GetScanTaskName(attribute.FullName));
                var type = Type.GetType(tempName);

                if (type != null)
                {
                    var existedMethod = Attribute.GetCustomAttribute(invocation.GetConcreteMethodInvocationTarget(),
                        type, false);

                    if (existedMethod != null)
                    {
                        var localAttribute = attribute;
                        CatchExceptionHelper.TryCatchAction(() => _attributeScanEngine.Run(invocation, localAttribute), _logger);
                    }
                }
            }
        }

        private static string GetAttributeName(string scanTaskName)
        {
            return _attributeNamespace + "." + scanTaskName + "Attribute";
        }

        private static string GetScanTaskName(string fullName)
        {
            var result = fullName.Substring(fullName.LastIndexOf(".") + 1,
                fullName.Length - fullName.LastIndexOf(".") - 1);

            result = result.Substring(0, result.IndexOf("ScanTask"));

            return result;
        }
    }
}