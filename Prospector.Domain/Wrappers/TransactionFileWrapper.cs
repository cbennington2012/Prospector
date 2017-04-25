using System;
using System.Collections.Generic;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Contracts.Wrappers;
using Prospector.Domain.Entities;

namespace Prospector.Domain.Wrappers
{
    public class TransactionFileWrapper : ITransactionFileWrapper
    {
        private readonly IAppSettingProvider _appSettingProvider;
        private readonly IJsonProvider _jsonProvider;
        private readonly IIoWrapper _ioWrapper;

        public TransactionFileWrapper(IAppSettingProvider appSettingProvider, IJsonProvider jsonProvider, IIoWrapper ioWrapper)
        {
            _appSettingProvider = appSettingProvider;
            _jsonProvider = jsonProvider;
            _ioWrapper = ioWrapper;
        }

        public IList<TransactionData> GetCurrentHoldings()
        {
            var contents = _ioWrapper.Read(_appSettingProvider.Get(Constants.HoldingsJsonFilePath));
            return _jsonProvider.Deserialize<IList<TransactionData>>(contents);
        }

        public void WriteCurrentHoldings(IList<TransactionData> currentHoldings)
        {
            var json = _jsonProvider.Serialize(currentHoldings);
            _ioWrapper.Write(_appSettingProvider.Get(Constants.HoldingsJsonFilePath), json);
        }
    }
}
