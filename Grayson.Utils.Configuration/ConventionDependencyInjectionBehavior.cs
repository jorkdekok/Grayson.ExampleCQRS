using SimpleInjector;
using SimpleInjector.Advanced;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grayson.Utils.Configuration
{
    public class ConventionDependencyInjectionBehavior
    {
        private readonly IDependencyInjectionBehavior decoratee;
        private readonly IParameterConvention convention;

        public ConventionDependencyInjectionBehavior(
            IDependencyInjectionBehavior decoratee, IParameterConvention convention)
        {
            this.decoratee = decoratee;
            this.convention = convention;
        }

        [DebuggerStepThrough]
        public Expression BuildExpression(InjectionConsumerInfo consumer)
        {
            return this.convention.CanResolve(consumer.Target)
                ? this.convention.BuildExpression(consumer)
                : this.decoratee.BuildExpression(consumer);
        }

        [DebuggerStepThrough]
        public void Verify(InjectionConsumerInfo consumer)
        {
            if (!this.convention.CanResolve(consumer.Target))
            {
                this.decoratee.Verify(consumer);
            }
        }
    }
}
