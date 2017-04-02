using System;
using NUnit.Framework;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Entities;
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

    public class WhenIGetTheTotalNumberOfMonths : GivenA<DateTimeProvider>
    {
        private readonly DateTime _startDate = DateTime.Parse("2017-01-01 00:00:00");
        private readonly DateTime _endDate = DateTime.Parse("2017-05-12 23:59:59");

        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(Target.GetTotalNumberOfMonths(_startDate, _endDate), Is.EqualTo(5M));
        }
    }

    public abstract class WhenIGetTheTaxYearStartDate : GivenA<DateTimeProvider, DateTime>
    {
        private const String TaxYearStart = "04-05";

        private SettingData _settingData;

        protected DateTime StartDate;

        protected override void Given()
        {
            base.Given();

            _settingData = new SettingData
            {
                SettingsValue = TaxYearStart
            };

            GetMock<ISettingRepository>()
                .Setup(m => m.GetSettingByKey("TaxYearStart"))
                .Returns(_settingData);
        }

        protected override void When()
        {
            base.When();

            Result = Target.GetTaxYearStartDate(StartDate);
        }
    }

    public class WhenITheStartDateIsLessThanThisYear : WhenIGetTheTaxYearStartDate
    {
        protected override void Given()
        {
            base.Given();

            StartDate = DateTime.Parse("2016-01-01 00:00:00");
        }

        [Then]
        public void TheSettingRepositoryGetsTheTaxYearStartDateParameter()
        {
            Verify<ISettingRepository>(m => m.GetSettingByKey("TaxYearStart"));
        }

        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(Result, Is.EqualTo(DateTime.Parse("2016-04-05 00:00:00")));
        }
    }

    public class WhenIGetTheStartDateGreaterThanThisYear : WhenIGetTheTaxYearStartDate
    {
        protected override void Given()
        {
            base.Given();

            StartDate = DateTime.Parse($"{DateTime.Today.Year}-06-01 00:00:00");
        }

        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(Result, Is.EqualTo(DateTime.Parse($"{DateTime.Today.Year}-04-05 00:00:00")));
        }
    }
}
