using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using SimpleInjector;
using SimpleInjector.Advanced;

namespace Grayson.Utils.Configuration
{

    // Using optional parameters in constructor arguments is highly discouraged. 
    // This code is merely an example.
    public class OptionalParameterConvention : IParameterConvention
    {
        private readonly IDependencyInjectionBehavior injectionBehavior;

        public OptionalParameterConvention(IDependencyInjectionBehavior injectionBehavior)
        {
            this.injectionBehavior = injectionBehavior;
        }

        [DebuggerStepThrough]
        public bool CanResolve(InjectionTargetInfo target) =>
            target.Parameter != null && target.GetCustomAttributes(typeof(OptionalAttribute), true).Any();

        [DebuggerStepThrough]
        public Expression BuildExpression(InjectionConsumerInfo consumer) =>
            this.GetProducer(consumer)?.BuildExpression() ?? GetDefault(consumer.Target.Parameter);

        private InstanceProducer GetProducer(InjectionConsumerInfo consumer) =>
            this.injectionBehavior.GetInstanceProducer(consumer, throwOnFailure: false);

        private static ConstantExpression GetDefault(ParameterInfo parameter) =>
            Expression.Constant(parameter.RawDefaultValue, parameter.ParameterType);
    }
}
