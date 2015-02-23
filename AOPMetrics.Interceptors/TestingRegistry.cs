using StructureMap.Configuration.DSL;

using AOPMetrics.Core.Interfaces.Persistence;
using AOPMetrics.Persistence;

namespace AOPMetrics.Interceptors
{
    public class TestingRegistry : Registry
    {
        public TestingRegistry()
        {
            //For<ITenantContext>().Use<SimpleTenantContext>();

            For<INewsRepository>().Use<NewsRepository>()
                .EnrichWith(ex => DynamicProxyHelper.CreateInterfaceProxyWithTargetInterface(typeof(INewsRepository), ex));
        }
    }
}
