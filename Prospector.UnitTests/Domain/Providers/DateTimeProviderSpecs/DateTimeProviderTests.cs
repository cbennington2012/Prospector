using System;
using NUnit.Framework;
using Prospector.Domain.Providers;

namespace Prospector.UnitTests.Domain.Providers.DateTimeProviderSpecs
{
    public class WhenIGetTheTransactionStartDate : GivenA<DateTimeProvider, DateTime>
    {
        private readonly DateTime _date = DateTime.Parse("2017-12-08 00:00:00");

        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(Target.GetTransactionStartDate(_date), Is.EqualTo(DateTime.Parse("2017-12-01 00:00:00")));
        }
    }

    public class WhenIGetTheTransactionEndDate : GivenA<DateTimeProvider, DateTime>
    {
        private readonly DateTime _date = DateTime.Parse("2017-12-08 00:00:00");

        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(Target.GetTransactionEndDate(_date), Is.EqualTo(DateTime.Parse("2017-12-31 23:59:59")));
        }
    }
}
