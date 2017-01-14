using System.Collections.Generic;
using Prospector.Domain.Entities;

namespace Prospector.Domain.Contracts.Repositories
{
    public interface ITransactionRepository
    {
        IList<TransactionData> GetCurrentHoldings();
        void Add(TransactionData data);
    }
}
