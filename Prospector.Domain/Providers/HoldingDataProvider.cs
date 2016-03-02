using System;
using System.Collections.Generic;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Contracts.Wrappers;
using Prospector.Domain.Entities;

namespace Prospector.Domain.Providers
{
    public class HoldingDataProvider : IHoldingDataProvider
    {
        private readonly IJsonProvider _jsonProvider;
        private readonly IAppSettingProvider _appSettingProvider;
        private readonly IIoWrapper _ioWrapper;

        public HoldingDataProvider(IJsonProvider jsonProvider, IAppSettingProvider appSettingProvider, IIoWrapper ioWrapper)
        {
            _jsonProvider = jsonProvider;
            _appSettingProvider = appSettingProvider;
            _ioWrapper = ioWrapper;
        }

        public IList<HoldingData> Get()
        {
            var json = _ioWrapper.Read(GetFileName());
            return _jsonProvider.Deserialize<IList<HoldingData>>(json);
        }

        public void Save(IList<HoldingData> holdingsData)
        {
            var json = _jsonProvider.Serialize(holdingsData);
            _ioWrapper.Write(GetFileName(), json);
        }

        private String GetFileName()
        {
            return String.Concat(_appSettingProvider.Get("HoldingsJsonFilePath"), "Holdings.json");
        }
    }
}
