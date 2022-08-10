using Microsoft.Extensions.Logging;

using SimpleInjector;
using SimpleInjector.Advanced;

using System;

namespace Grayson.Utils.Logging
{
    /// <summary>
    /// Test 01
    /// </summary>
    public class MsContextualLoggerInjectionBehavior : IDependencyInjectionBehavior
    {
        private readonly Container container;
        private readonly ILoggerFactory factory;
        private readonly IDependencyInjectionBehavior original;

        public MsContextualLoggerInjectionBehavior(
            ILoggerFactory factory, Container container)
        {
            this.factory = factory;
            this.original = container.Options.DependencyInjectionBehavior;
            this.container = container;
        }

        public InstanceProducer GetInstanceProducer(InjectionConsumerInfo i, bool t) =>
            i.Target.TargetType == typeof(ILogger)
                ? GetLoggerInstanceProducer(i.ImplementationType)
                : original.GetInstanceProducer(i, t);

        public void Verify(InjectionConsumerInfo consumer) => original.Verify(consumer);

        private InstanceProducer<ILogger> GetLoggerInstanceProducer(Type type) =>
            Lifestyle.Singleton.CreateProducer(() => factory.CreateLogger(type), container);
    }
}