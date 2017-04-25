using System.Collections.Generic;
using Prospector.Domain.Entities;

namespace Prospector.Domain.Contracts.Wrappers
{
    public interface ITransactionFileWrapper
    {
        IList<TransactionData> GetCurrentHoldings();
        void WriteCurrentHoldings(IList<TransactionData> currentHoldings);
    }
}
