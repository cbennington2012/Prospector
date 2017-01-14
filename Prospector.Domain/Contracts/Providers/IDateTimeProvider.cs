using System;

namespace Prospector.Domain.Contracts.Providers
{
    public interface IDateTimeProvider
    {
        DateTime GetTransactionStartDate(DateTime date);
        DateTime GetTransactionEndDate(DateTime date);
    }
}
