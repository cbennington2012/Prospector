using System.Collections.Generic;
using Prospector.Domain.Entities;

namespace Prospector.Domain.Contracts.Providers
{
    public interface IHoldingDataProvider
    {
        IList<HoldingData> Get();
        void Save(IList<HoldingData> holdingsData);
    }
}
