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

    // Example usage:
    // new ConnectionStringsConvention(name => ConfigurationManager.ConnectionStrings[name]?.ConnectionString)
    public class ConnectionStringsConvention : IParameterConvention
    {
        private const string ConnectionStringPostFix = "ConnectionString";

        private readonly Func<string, string> connectionStringRetriever;

        public ConnectionStringsConvention(Func<string, string> connectionStringRetriever)
        {
            this.connectionStringRetriever = connectionStringRetriever;
        }

        [DebuggerStepThrough]
        public bool CanResolve(InjectionTargetInfo target)
        {
            bool resolvable =
                target.TargetType == typeof(string) &&
                target.Name.EndsWith(ConnectionStringPostFix) &&
                target.Name.LastIndexOf(ConnectionStringPostFix) > 0;

            if (resolvable)
            {
                this.VerifyConfigurationFile(target);
            }

            return resolvable;
        }

        [DebuggerStepThrough]
        public Expression BuildExpression(InjectionConsumerInfo consumer)
        {
            string connectionString = this.GetConnectionString(consumer.Target);

            return Expression.Constant(connectionString, typeof(string));
        }

        [DebuggerStepThrough]
        private void VerifyConfigurationFile(InjectionTargetInfo target)
        {
            this.GetConnectionString(target);
        }

        [DebuggerStepThrough]
        private string GetConnectionString(InjectionTargetInfo target)
        {
            string name = target.Name.Substring(0,
                target.Name.LastIndexOf(ConnectionStringPostFix));

            var connectionString = this.connectionStringRetriever(name);

            if (connectionString == null)
            {
                throw new ActivationException(
                    "No connection string with name '" + name + "' could be found in the " +
                    "application's configuration file.");
            }

            return connectionString;
        }
    }
}
