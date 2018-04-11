using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grayson.Utils.Configuration
{
    public interface IParameterConvention
    {
        bool CanResolve(InjectionTargetInfo target);

        Expression BuildExpression(InjectionConsumerInfo consumer);
    }
}
