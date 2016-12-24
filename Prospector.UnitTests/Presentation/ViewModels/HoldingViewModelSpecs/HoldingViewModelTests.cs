using System;
using NUnit.Framework;
using Prospector.Presentation.ViewModels;

namespace Prospector.UnitTests.Presentation.ViewModels.HoldingViewModelSpecs
{
    public class WhenICreateAHoldingViewModel : TestBase<HoldingViewModel>
    {
        private readonly Guid _id = Guid.NewGuid();
        protected override void When()
        {
            base.When();

            Target = new HoldingViewModel
            {
                Id = _id,
                Code = "TST",
                Date = DateTime.UtcNow,
                Shares = 1000,
                Price = 50.5M,
                Tax = 25.5M,
                Commission = 5.95M,
                Levy = 0,
                Cost = 1500M,
                BreakEvenPrice = 55.0M,
                ProfitPrice = 75.5M,
                Earnings = 125.5M
            };
        }

        [Then]
        public void TheIdPropertyIsCorrect()
        {
            Assert.That(Target.Id, Is.EqualTo(_id));
        }

        [Then]
        public void TheCodePropertyIsCorrect()
        {
            Assert.That(Target.Code, Is.EqualTo("TST"));
        }

        [Then]
        public void TheDatePropertyIsCorrect()
        {
            Assert.That(Target.Date, Is.InRange(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5)));
        }

        [Then]
        public void TheSharesPropertyIsCorrect()
        {
            Assert.That(Target.Shares, Is.EqualTo(1000));
        }

        [Then]
        public void ThePricePropertyIsCorrect()
        {
            Assert.That(Target.Price, Is.EqualTo(50.5M));
        }

        [Then]
        public void TheTaxPropertyIsCorrect()
        {
            Assert.That(Target.Tax, Is.EqualTo(25.5M));
        }

        [Then]
        public void TheCommissionPropertyIsCorrect()
        {
            Assert.That(Target.Commission, Is.EqualTo(5.95));
        }

        [Then]
        public void TheLevyPropertyIsCorrect()
        {
            Assert.That(Target.Levy, Is.EqualTo(0));
        }

        [Then]
        public void TheCostPropertyIsCorrect()
        {
            Assert.That(Target.Cost, Is.EqualTo(1500M));
        }

        [Then]
        public void TheBreakEvenPricePropertyIsCorrect()
        {
            Assert.That(Target.BreakEvenPrice, Is.EqualTo(55));
        }

        [Then]
        public void TheProfitPricePropertyIsCorrect()
        {
            Assert.That(Target.ProfitPrice, Is.EqualTo(75.5));
        }

        [Then]
        public void TheEarningsPropertyIsCorrect()
        {
            Assert.That(Target.Earnings, Is.EqualTo(125.5));
        }
    }
}
