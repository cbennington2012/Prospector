using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Prospector.Presentation.ViewModels;

namespace Prospector.UnitTests.Presentation.ViewModels.TransactionSearchViewModelSpecs
{
    public class WhenIGetATransactionSearchViewModel : TestBase<TransactionSearchViewModel>
    {
        private readonly DateTime _startDate = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _endDate = DateTime.UtcNow;
        private readonly IList<TransactionViewModel> _results = new List<TransactionViewModel>();

        protected override void When()
        {
            base.When();

            Target = new TransactionSearchViewModel
            {
                StartDate = _startDate,
                EndDate = _endDate,
                Results = _results,
                MonthlyTarget = 3000,
                TaxFreeAllowance = 11500,
                TransactionPeriod = 2000,
                SinceStartTaxYear = 5000
            };
        }

        [Then]
        public void TheStartDatePropertyIsCorrect()
        {
            Assert.That(Target.StartDate, Is.EqualTo(_startDate));
        }

        [Then]
        public void TheEndDatePropertyIsCorrect()
        {
            Assert.That(Target.EndDate, Is.EqualTo(_endDate));
        }

        [Then]
        public void TheResultsPropertyIsCorrect()
        {
            Assert.That(Target.Results, Is.EqualTo(_results));
        }

        [Then]
        public void TheMonthlyTargetPropertyIsCorrect()
        {
            Assert.That(Target.MonthlyTarget, Is.EqualTo(3000));
        }

        [Then]
        public void TheTaxFreeAllowancePropertyIsCorrect()
        {
            Assert.That(Target.TaxFreeAllowance, Is.EqualTo(11500));
        }

        [Then]
        public void TheTransactionPeriodPropertyIsCorrect()
        {
            Assert.That(Target.TransactionPeriod, Is.EqualTo(2000));
        }

        [Then]
        public void TheSinceTaxYearStartPropertyIsCorrect()
        {
            Assert.That(Target.SinceStartTaxYear, Is.EqualTo(5000));
        }
    }
}
