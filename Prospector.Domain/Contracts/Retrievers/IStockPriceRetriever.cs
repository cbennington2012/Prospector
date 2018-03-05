using System.Collections.Generic;
using Prospector.Domain.Entities;

namespace Prospector.Domain.Contracts.Retrievers
{
    public interface IStockPriceRetriever
    {
        IList<StockPriceData> GetPrices(IList<TransactionData> transactions);
    }
}
