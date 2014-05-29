using System.Configuration;
using Blog.Common.Utils.Helpers.Interfaces;

namespace Blog.Common.Utils.Helpers
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        public string GetAppSettings(string key)
        {
            return ConfigurationManager.AppSettings.Get(key);
        }
    }
}
