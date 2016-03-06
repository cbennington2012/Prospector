using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prospector.Presentation.ViewModels;

namespace Prospector.UnitTests.Presentation.ViewModels.HoldingViewModelSpecs
{
    public class WhenICreateAHoldingViewModel : TestBase<HoldingViewModel>
    {
        protected override void When()
        {
            base.When();

            Target = new HoldingViewModel
            {
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
        public void TheCodePropertyIsCorrectt()
        {
            Assert.That(Target.Code, Is.EqualTo("TST"));
        }

        [Then]
        public void TheDatePropertyIsCorrectt()
        {
            Assert.That(Target.Date, Is.InRange(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5)));
        }

        [Then]
        public void TheSharesPropertyIsCorrectt()
        {
            Assert.That(Target.Shares, Is.EqualTo(1000));
        }

        [Then]
        public void ThePricePropertyIsCorrectt()
        {
            Assert.That(Target.Price, Is.EqualTo(50.5M));
        }

        [Then]
        public void TheTaxPropertyIsCorrectt()
        {
            Assert.That(Target.Tax, Is.EqualTo(25.5M));
        }

        [Then]
        public void TheCommissionPropertyIsCorrectt()
        {
            Assert.That(Target.Commission, Is.EqualTo(5.95));
        }

        [Then]
        public void TheLevyPropertyIsCorrectt()
        {
            Assert.That(Target.Levy, Is.EqualTo(0));
        }

        [Then]
        public void TheCostPropertyIsCorrectt()
        {
            Assert.That(Target.Cost, Is.EqualTo(1500M));
        }

        [Then]
        public void TheBreakEvenPricePropertyIsCorrectt()
        {
            Assert.That(Target.BreakEvenPrice, Is.EqualTo(55));
        }

        [Then]
        public void TheProfitPricePropertyIsCorrectt()
        {
            Assert.That(Target.ProfitPrice, Is.EqualTo(75.5));
        }

        [Then]
        public void TheEarningsPropertyIsCorrectt()
        {
            Assert.That(Target.Earnings, Is.EqualTo(125.5));
        }
    }
}
