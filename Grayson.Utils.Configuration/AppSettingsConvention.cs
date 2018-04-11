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
    // new AppSettingsConvention(key => ConfigurationManager.AppSettings[key]);
    public class AppSettingsConvention : IParameterConvention
    {
        private const string AppSettingsPostFix = "AppSetting";

        private readonly Func<string, string> appSettingRetriever;

        public AppSettingsConvention(Func<string, string> appSettingRetriever)
        {
            this.appSettingRetriever = appSettingRetriever;
        }

        [DebuggerStepThrough]
        public bool CanResolve(InjectionTargetInfo target)
        {
            Type type = target.TargetType;

            bool resolvable =
                (type.IsValueType || type == typeof(string)) &&
                target.Name.EndsWith(AppSettingsPostFix) &&
                target.Name.LastIndexOf(AppSettingsPostFix) > 0;

            if (resolvable)
            {
                this.VerifyConfigurationFile(target);
            }

            return resolvable;
        }

        [DebuggerStepThrough]
        public Expression BuildExpression(InjectionConsumerInfo consumer)
        {
            object valueToInject = this.GetAppSettingValue(consumer.Target);

            return Expression.Constant(valueToInject, consumer.Target.TargetType);
        }

        [DebuggerStepThrough]
        private void VerifyConfigurationFile(InjectionTargetInfo target)
        {
            this.GetAppSettingValue(target);
        }

        [DebuggerStepThrough]
        private object GetAppSettingValue(InjectionTargetInfo target)
        {
            string key = target.Name.Substring(0,
                target.Name.LastIndexOf(AppSettingsPostFix));

            string configurationValue = this.appSettingRetriever(key); // ConfigurationManager.AppSettings[key];

            if (configurationValue != null)
            {
                System.ComponentModel.TypeConverter converter =
                    System.ComponentModel.TypeDescriptor.GetConverter(target.TargetType);

                return converter.ConvertFromString(null,
                    CultureInfo.InvariantCulture, configurationValue);
            }

            throw new ActivationException(
                "No application setting with key '" + key + "' could be found in the " +
                "application's configuration file.");
        }
    }
}
