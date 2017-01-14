using System;
using System.Collections.Generic;
using Prospector.Domain.Entities;

namespace Prospector.Domain.Contracts.Repositories
{
    public interface ITransactionRepository
    {
        IList<TransactionData> GetCurrentHoldings();
        void AddTransaction(TransactionData data);
        IList<TransactionData> GetTransactions(DateTime startDate, DateTime endDate);
    }
}
