using SimpleInjector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Grayson.Utils.Configuration
{
    public class AppSettingsConvention : IParameterConvention
    {
        private const string AppSettingsPostFix = "AppSetting";

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
            object valueToInject = GetAppSettingValue(consumer.Target);

            return Expression.Constant(valueToInject, consumer.Target.TargetType);
        }

        [DebuggerStepThrough]
        private void VerifyConfigurationFile(InjectionTargetInfo target)
        {
            GetAppSettingValue(target);
        }

        [DebuggerStepThrough]
        private static object GetAppSettingValue(InjectionTargetInfo target)
        {
            string key = target.Name.Substring(0,
                target.Name.LastIndexOf(AppSettingsPostFix));

            string configurationValue = ConfigurationManager.AppSettings[key];

            if (configurationValue != null)
            {
                TypeConverter converter = TypeDescriptor.GetConverter(target.TargetType);

                return converter.ConvertFromString(null,
                    CultureInfo.InvariantCulture, configurationValue);
            }

            throw new ActivationException(
                "No application setting with key '" + key + "' could be found in the " +
                "application's configuration file.");
        }
    }
}
