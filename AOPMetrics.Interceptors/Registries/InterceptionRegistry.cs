using Castle.DynamicProxy;
using Common.Logging;
using Common.Logging.Simple;
using StructureMap.Configuration.DSL;

namespace AOPMetrics.Interceptors.Registries
{
    public class InterceptionRegistry : Registry
    {
        private readonly ProxyGenerator _proxyGenerator;

        public InterceptionRegistry()
        {
            #region Examples

            //// Perform an Action<T> upon the object of type T just created before it is returned to the caller
            //For<IClassThatNeedsSomeBootstrapping>()
            //    .Use<ClassThatNeedsSomeBootstrapping>()
            //    //.EnrichWith(_ => new ClassThatNeedsSomeBootstrappingDecorator(_));
            //    .OnCreation(x => x.Start());

            //ForRequestedType<IConnectionListener>()
            //    .TheDefault.Is
            //    .OfConcreteType<ClassThatNeedsSomeBootstrapping>()
            //    .EnrichWith(_ => new ClassThatNeedsSomeBootstrappingDecorator(_));

            //// You can also register an Action<IContext, T> to get access to all the services and capabilities of the BuildSession
            //For<IClassThatNeedsSomeBootstrapping>()
            //    .Use<ClassThatNeedsSomeBootstrapping>()
            //    .EnrichWith(_ => new ClassThatNeedsSomeBootstrappingDecorator(_))
            //    .OnCreation((context, x) =>
            //        {
            //            var connection = context.GetInstance<IConnectionPoint>();
            //            x.Connect(connection);
            //        });

            #endregion

            _proxyGenerator = new ProxyGenerator();

            //For<ProxyGenerator>()
            //    .Use<ProxyGenerator>();

            //For<ILog>()
            //    .Use<Logger>();

            var logger = new DebugOutLogger("DebugOutLogger", LogLevel.Info, true, true, true, "dd-mm-YYYY");

            //For<IConnectionListener>()
            For<IDocumentService>()
                .Use<DocumentService>()
                //.Decorate().With<DocumentServiceMetricsAdapter>()
                //.AndThen<DocumentServiceLogger>();

                //.EnrichWith(x => DynamicProxyHelper.CreateInterfaceProxyWithTargetInterface(typeof(IDocumentService), x));

                //.EnrichWith(x => new ProxyGenerator().CreateInterfaceProxyWithTargetInterface(typeof(IDocumentService), x, new LoggingInterceptor(debugOutLogger)));
                .DecorateWith((ctx, x) => _proxyGenerator.CreateInterfaceProxyWithTargetInterface(x, new LoggingInterceptor(logger)))
                //.DecorateWith((ctx, x) => ctx.GetInstance<ProxyGenerator>().CreateInterfaceProxyWithTargetInterface(x, new LoggingInterceptor(logger)))

                //.EnrichWith(x => new ProxyGenerator().CreateInterfaceProxyWithTargetInterface(typeof(IDocumentService), x, new AuditInterceptor(debugOutLogger)));
                .DecorateWith(x => _proxyGenerator.CreateInterfaceProxyWithTargetInterface(x, new AuditInterceptor(logger)));
                //.DecorateWith((ctx, x) => ctx.GetInstance<ProxyGenerator>().CreateInterfaceProxyWithTargetInterface(x, new AuditInterceptor(logger)));

            //.EnrichWith(x => new DocumentServiceMetricsAdapter(x))
            //.EnrichWith((context, target) => new DocumentServiceLogger(target, context.GetInstance<ILog>()));
            //.InterceptWith(new LogInterceptor());
        }
    }
}
