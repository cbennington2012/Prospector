using System;
using Prospector.Domain.Contracts.Providers;

namespace Prospector.Domain.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetTransactionStartDate(DateTime date)
        {
            return DateTime.Parse(date.ToString("yyyy-MM-01 00:00:00"));
        }

        public DateTime GetTransactionEndDate(DateTime date)
        {
            return DateTime.Parse(DateTime.Parse(date.ToString("yyyy-MM-01 00:00:00")).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd 23:59:59"));
        }

        public Decimal GetTotalNumberOfMonths(DateTime startDate, DateTime endDate)
        {
            return Math.Ceiling((endDate.Subtract(startDate).Days/(365.25M/12)));
        }
    }
}
