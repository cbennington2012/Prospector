using System;
using System.Collections.Generic;
using Prospector.Domain.Entities;

namespace Prospector.Domain.Contracts.Factories
{
    public interface ITransactionFactory
    {
        Decimal GetTransactionPeriodValue(IList<TransactionData> data);
        Decimal GetTaxYearValue(IList<TransactionData> taxYearData);
    }
}
