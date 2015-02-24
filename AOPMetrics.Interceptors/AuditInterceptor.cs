using System.Text;

using Castle.DynamicProxy;
using Common.Logging;

using AOPMetrics.Core.Interfaces;

namespace AOPMetrics.Interceptors
{
    public class AuditInterceptor : IInterceptor
    {
        #region Private Members

        private readonly ILog _logger;

        #endregion

        #region Ctor's

        public AuditInterceptor(ILog logger)
        {
            _logger = logger;
        }

        #endregion

        public void Intercept(IInvocation invocation)
        {
            var sb = new StringBuilder(string.Format("{0}.{1}(", invocation.TargetType.FullName, invocation.Method));

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                if (i > 0)
                {
                    sb.Append(", ");
                }

                sb.Append(invocation.Arguments[i]);
            }

            sb.Append(")");
            _logger.Debug(_ => _(sb.ToString()));

            invocation.Proceed();
            _logger.Debug(_ => _("An exception has been thrown: {0}", "Exception Name"));
        }
    }
}