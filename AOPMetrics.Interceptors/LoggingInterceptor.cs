using System.Text;

using Castle.DynamicProxy;
using Common.Logging;

namespace AOPMetrics.Interceptors
{
    public class LoggingInterceptor : IInterceptor
    {
        #region Private Members

        private readonly ILog _logger;

        #endregion

        #region Ctor's

        public LoggingInterceptor(ILog logger)
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
            _logger.Debug(_ => _("Result of invocation is {0}.", invocation.ReturnValue));
        }
    }
}