using StructureMap.Interceptors;
using StructureMap.Pipeline;

namespace AOPMetrics.Core.Extensions
{
    public static class StructureMapExtensions
    {
        public static DecoratorHelper<T> Decorate<T>(this SmartInstance<T> instance)
        {
            return new DecoratorHelper<T>(instance);
        }
    }

    public class DecoratorHelper<T>
    {
        private readonly SmartInstance<T> _instance;

        public DecoratorHelper(SmartInstance<T> instance)
        {
            _instance = instance;
        }

        public DecoratedInstance<T> With<TDecorator>()
        {
            ContextEnrichmentHandler<T> decorator = (ctx, t) =>
                {
                    var pluginType = ctx.BuildStack.Current.RequestedType;
                    ctx.RegisterDefault(pluginType, t);

                    return ctx.GetInstance<TDecorator>();
                };

            _instance.EnrichWith(decorator);

            return new DecoratedInstance<T>(_instance, decorator);
        }

        public class DecoratedInstance<T>
        {
            private readonly SmartInstance<T> _instance;
            private ContextEnrichmentHandler<T> _decorator;

            public DecoratedInstance(SmartInstance<T> instance, ContextEnrichmentHandler<T> decorator)
            {
                _instance = instance;
                _decorator = decorator;
            }

            public DecoratedInstance<T> AndThen<TDecorator>()
            {
                var previous = _decorator;
                ContextEnrichmentHandler<T> newDecorator = (ctx, t) =>
                    {
                        var pluginType = ctx.BuildStack.Current.RequestedType;
                        var innerInstance = previous(ctx, t);
                        ctx.RegisterDefault(pluginType, innerInstance);

                        return ctx.GetInstance<TDecorator>();
                    };

                _instance.EnrichWith(newDecorator);
                _decorator = newDecorator;

                return this;
            }
        }
    }
}
