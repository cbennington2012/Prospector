using System;
using System.Collections.Generic;
using Prospector.Domain.Entities;

namespace Prospector.Domain.Contracts.Repositories
{
    public interface ISettingRepository
    {
        IList<SettingData> Get();
        SettingData GetSettingByKey(String key);
    }
}
