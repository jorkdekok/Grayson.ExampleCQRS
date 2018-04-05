using System;
using System.Linq;
using System.Linq.Expressions;

using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure.Extensions
{
    public static class SimpleInjectorExtensions
    {
        public static void AllowResolvingFuncFactories(this ContainerOptions options)
        {
            options.Container.ResolveUnregisteredType += (s, e) =>
            {
                var type = e.UnregisteredServiceType;

                if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Func<>))
                {
                    return;
                }

                Type serviceType = type.GetGenericArguments().First();
                InstanceProducer registration = null;
                try
                {
                    registration  = options.Container.GetRegistration(serviceType, true);
                }
                catch (ActivationException)
                {
                    serviceType = serviceType.GetInterfaces().FirstOrDefault();
                    registration = options.Container.GetRegistration(serviceType, true);
                }

                Type funcType = typeof(Func<>).MakeGenericType(serviceType);

                var factoryDelegate = Expression.Lambda(funcType, registration.BuildExpression()).Compile();

                e.Register(Expression.Constant(factoryDelegate));
            };
        }
    }
}