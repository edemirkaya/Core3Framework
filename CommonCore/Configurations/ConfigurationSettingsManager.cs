using System;

namespace CommonCore.Configurations
{
    public class ConfigurationSettingsManager
    {
        internal static readonly ConfigurationSettings configurationSettings;

        public static bool ApplicationSettingExists(string key)
        {
            //TODO
            //return CSBConfigurationSettings.ApplicationSettings[key] != null;
            return true;
        }

        public static bool TryGetApplicationSetting(string key, out string value)
        {
            value = GetApplicationSetting(key);
            if (value == null)
                return false;
            return true;
        }

        public static bool TryGetApplicationSetting<T>(string key, out T value)
        {
            var def = default(T);
            try
            {
                value = GetApplicationSetting(key, def);
                return !value.Equals(def);
            }
            catch (Exception)
            {
                value = def;
                return false;
            }
        }

        public static string GetApplicationSetting(string key)
        {
            //TODO
            //string value = null;
            //ApplicationSettingElement applicationSetting =
            //    CSBConfigurationSettings.ApplicationSettings[key];
            //if (applicationSetting != null)
            //{
            //    value = applicationSetting.Value;
            //}
            //else
            //{
            //    value = null;
            //}
            //return value;

            return "";
        }

        //public static ServiceEndPointSettingElement GetServiceEndPointSetting(string key)
        //{
        //    ServiceEndPointSettingElement setting = CSBConfigurationSettings.ServiceEndPointSettings[key];
        //    return setting;
        //}

        //TODO
        public static T GetApplicationSetting<T>(string key, T defaultvalue)
        {
            return (T)Convert.ChangeType("", typeof(T));
        }

        //public static ExceptionSettings GetExceptionSettings()
        //{
        //    return CSBConfigurationSettings.ExceptionSettings;
        //}

        //public static CustomMemoryCacheSection GetCacheSettings()
        //{
        //    return CSBConfigurationSettings.CacheSettings;
        //}
    }
}
