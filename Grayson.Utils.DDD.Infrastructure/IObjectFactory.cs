using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayson.Utils.DDD.Infrastructure
{
    public interface IObjectFactory
    {
        T GetInstance<T>() where T : class;

        object GetInstance(Type instanceType);

        IEnumerable<Object> GetAllInstances(Type instanceType);
    }
}
