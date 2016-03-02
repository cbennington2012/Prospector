using System;

namespace Prospector.Domain.Contracts.Providers
{
    public interface IAppSettingProvider
    {
        String Get(String key);
    }
}
