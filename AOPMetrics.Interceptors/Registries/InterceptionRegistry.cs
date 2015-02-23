using AOPMetrics.Core.Interfaces.Logging;
using StructureMap.Configuration.DSL;

namespace AOPMetrics.Interceptors.Registries
{
    public class InterceptionRegistry : Registry
    {
        public InterceptionRegistry()
        {
            //// Perform an Action<T> upon the object of type T just created before it is returned to the caller
            //For<IClassThatNeedsSomeBootstrapping>()
            //    .Use<ClassThatNeedsSomeBootstrapping>()
            //    //.EnrichWith(_ => new ClassThatNeedsSomeBootstrappingDecorator(_));
            //    .OnCreation(x => x.Start());

            //ForRequestedType<IConnectionListener>()
            //    .TheDefault.Is
            //    .OfConcreteType<ClassThatNeedsSomeBootstrapping>()
            //    .EnrichWith(_ => new ClassThatNeedsSomeBootstrappingDecorator(_));

            For<ILog>()
                .Use<Logger>();

            //For<IConnectionListener>()
            For<IDocumentService>()
                .Use<DocumentService>()
                .EnrichWith(x => new DocumentServiceMetricsAdapter(x));
            //.EnrichWith((context, target) => new DocumentServiceLogger(target, context.GetInstance<ILog>()));
            //.InterceptWith(new DocumentServiceLogger());

            //// You can also register an Action<IContext, T> to get access to all the services and capabilities of the BuildSession
            //For<IClassThatNeedsSomeBootstrapping>()
            //    .Use<ClassThatNeedsSomeBootstrapping>()
            //    .EnrichWith(_ => new ClassThatNeedsSomeBootstrappingDecorator(_))
            //    .OnCreation((context, x) =>
            //        {
            //            var connection = context.GetInstance<IConnectionPoint>();
            //            x.Connect(connection);
            //        });
        }
    }
}
