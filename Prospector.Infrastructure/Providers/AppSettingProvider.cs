using System;
using System.Configuration;
using Prospector.Domain.Contracts.Providers;

namespace Prospector.Infrastructure.Providers
{
    public class AppSettingProvider : IAppSettingProvider
    {
        public String Get(String key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
