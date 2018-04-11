using SimpleInjector;
using SimpleInjector.Advanced;

using System.Diagnostics;

namespace Grayson.Utils.Configuration
{
    public static class DefaultParameterConventionExtensions
    {
        public static void RegisterParameterConvention(this ContainerOptions options,
            IParameterConvention convention)
        {
            options.DependencyInjectionBehavior = new ConventionDependencyInjectionBehavior(
                options.DependencyInjectionBehavior, convention, options.Container);
        }

        private class ConventionDependencyInjectionBehavior : IDependencyInjectionBehavior
        {
            private Container container;
            private IParameterConvention convention;
            private IDependencyInjectionBehavior decorated;

            public ConventionDependencyInjectionBehavior(
                IDependencyInjectionBehavior decorated, IParameterConvention convention,
                Container container)
            {
                this.decorated = decorated;
                this.convention = convention;
                this.container = container;
            }

            [DebuggerStepThrough]
            public InstanceProducer GetInstanceProducer(InjectionConsumerInfo consumer, bool throwOnFailure)
            {
                if (!this.convention.CanResolve(consumer.Target))
                {
                    return this.decorated.GetInstanceProducer(consumer, throwOnFailure);
                }

                return InstanceProducer.FromExpression(
                    serviceType: consumer.Target.TargetType,
                    expression: this.convention.BuildExpression(consumer),
                    container: this.container);
            }

            [DebuggerStepThrough]
            public void Verify(InjectionConsumerInfo consumer)
            {
                if (!this.convention.CanResolve(consumer.Target))
                {
                    this.decorated.Verify(consumer);
                }
            }
        }
    }
}