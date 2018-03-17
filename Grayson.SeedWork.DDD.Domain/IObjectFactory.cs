using System;
using System.Collections.Generic;

namespace Grayson.SeedWork.DDD.Domain
{
    public interface IObjectFactory
    {
        IEnumerable<Object> GetAllInstances(Type instanceType);

        T GetInstance<T>() where T : class;

        object GetInstance(Type instanceType);
    }
}