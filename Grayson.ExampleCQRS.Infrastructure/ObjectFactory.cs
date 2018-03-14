using System;
using System.Collections.Generic;
using System.Text;
using Grayson.Utils.DDD.Infrastructure;
using SimpleInjector;

namespace Grayson.ExampleCQRS.Infrastructure
{
    public class ObjectFactory : IObjectFactory
    {
        private readonly Container _container;

        public ObjectFactory(Container container)
        {
            _container = container;
        }

        T IObjectFactory.GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }

        public object GetInstance(Type instanceType)
        {
            return _container.GetInstance(instanceType);
        }

        public IEnumerable<object> GetAllInstances(Type instanceType)
        {
            return _container.GetAllInstances(instanceType);
        }
    }
}
