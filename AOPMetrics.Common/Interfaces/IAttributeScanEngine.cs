using System;
using System.Collections.Generic;

using Castle.DynamicProxy;

namespace AOPMetrics.Core.Interfaces
{
    public interface IAttributeScanEngine
    {
        void Run(IInvocation invocation, Type type);

        IEnumerable<Type> AutoFindAttribute(string path);
    }
}