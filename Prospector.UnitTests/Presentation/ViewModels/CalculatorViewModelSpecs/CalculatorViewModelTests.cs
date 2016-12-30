using NUnit.Framework;
using Prospector.Presentation.ViewModels;

namespace Prospector.UnitTests.Presentation.ViewModels.CalculatorViewModelSpecs
{
    public class WhenIGetACalculatorViewModel : TestBase<CalculatorViewModel>
    {
        protected override void When()
        {
            base.When();

            Target = new CalculatorViewModel
            {
                Investment = 10000,
                Price = 100.5M,
                Tax = 10.5M,
                Commission = 5.95M,
                Levy = 1,
                Profit = 2.5M,
                Cost = 150.25M,
                Shares = 1000,
                BreakEvenPrice = 101.5M,
                ProfitPrice = 105.75M,
                Earnings = 10250
            };
        }

        [Then]
        public void TheInvestmentPropertyIsCorrect()
        {
            Assert.That(Target.Investment, Is.EqualTo(10000));
        }

        [Then]
        public void ThePricePropertyIsCorrect()
        {
            Assert.That(Target.Price, Is.EqualTo(100.5));
        }

        [Then]
        public void TheTaxPropertyIsCorrect()
        {
            Assert.That(Target.Tax, Is.EqualTo(10.5));
        }

        [Then]
        public void TheCommissionPropertyIsCorrect()
        {
            Assert.That(Target.Commission, Is.EqualTo(5.95));
        }

        [Then]
        public void TheLevyPropertyIsCorrect()
        {
            Assert.That(Target.Levy, Is.EqualTo(1));
        }

        [Then]
        public void TheProfitPropertyIsCorrect()
        {
            Assert.That(Target.Profit, Is.EqualTo(2.5));
        }

        [Then]
        public void TheCostPropertyIsCorrect()
        {
            Assert.That(Target.Cost, Is.EqualTo(150.25));
        }

        [Then]
        public void TheSharesPropertyIsCorrect()
        {
            Assert.That(Target.Shares, Is.EqualTo(1000));
        }

        [Then]
        public void TheBreakEvenPricePropertyIsCorrect()
        {
            Assert.That(Target.BreakEvenPrice, Is.EqualTo(101.5));
        }

        [Then]
        public void TheProfitPricePropertyIsCorrect()
        {
            Assert.That(Target.ProfitPrice, Is.EqualTo(105.75));
        }

        [Then]
        public void TheEarningsPropertyIsCorrect()
        {
            Assert.That(Target.Earnings, Is.EqualTo(10250));
        }
    }
}
