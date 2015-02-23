using System.Reflection;

using Castle.DynamicProxy;

namespace AOPMetrics.Core.Interfaces
{
    public interface IAttributeScanTask
    {
        void Run(IInvocation invocation, bool isFirstCall);

        bool AttributeRecognize(ICustomAttributeProvider methodInfo);
    }
}