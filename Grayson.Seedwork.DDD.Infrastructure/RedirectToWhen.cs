using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Grayson.SeedWork.DDD.Infrastructure
{
    public static class RedirectToWhen
    {
        private static Dictionary<Type, IDictionary<Type, MethodInfo>> _cache = new Dictionary<Type, IDictionary<Type, MethodInfo>>();

        private static readonly MethodInfo InternalPreserveStackTraceMethod =
            typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);

        [DebuggerNonUserCode]
        public static void InvokeCommand<T>(T instance, object command)
        {
            MethodInfo info;
            var type = command.GetType();
            if (!Cache<T>.Dict.TryGetValue(type, out info))
            {
                var s = string.Format("Failed to locate {0}.When({1})", typeof(T).Name, type.Name);
                throw new InvalidOperationException(s);
            }
            try
            {
                info.Invoke(instance, new[] { command });
            }
            catch (TargetInvocationException ex)
            {
                if (null != InternalPreserveStackTraceMethod)
                    InternalPreserveStackTraceMethod.Invoke(ex.InnerException, new object[0]);
                throw ex.InnerException;
            }
        }

        //[DebuggerNonUserCode]
        public static void InvokeEventOptional(object instance, object @event)
        {
            MethodInfo info;
            var type = @event.GetType();

            if (_cache.ContainsKey(instance.GetType()))
            {
                if (!_cache[instance.GetType()].TryGetValue(type, out info))
                {
                     _cache[instance.GetType()] = instance.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .Where(m => m.Name == "When")
                        .Where(m => m.GetParameters().Length == 1)
                        .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
                    // we don't care if state does not consume events they are persisted anyway
                    return;
                }
                try
                {
                    info.Invoke(instance, new[] { @event });
                }
                catch (TargetInvocationException ex)
                {
                    if (null != InternalPreserveStackTraceMethod)
                        InternalPreserveStackTraceMethod.Invoke(ex.InnerException, new object[0]);
                    throw ex.InnerException;
                }
            }
        }

        private static class Cache<T>
        {
            // ReSharper disable StaticFieldInGenericType
            public static readonly IDictionary<Type, MethodInfo> Dict = typeof(T)
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name == "When")
                .Where(m => m.GetParameters().Length == 1)
                .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);

            // ReSharper restore StaticFieldInGenericType
        }
    }
}