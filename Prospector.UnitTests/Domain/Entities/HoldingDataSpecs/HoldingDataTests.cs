using System;
using NUnit.Framework;
using Prospector.Domain.Entities;

namespace Prospector.UnitTests.Domain.Entities.HoldingDataSpecs
{
    public class WhenIGetTheCode : TestBase<HoldingData>
    {
        protected override void When()
        {
            base.When();

            Target = new HoldingData
            {
                Code = "TST",
                Date = DateTime.UtcNow,
                Shares = 10000,
                Price = 1000.5M,
                Tax = 100.5M,
                Commission = 10.5M,
                Levy = 1.5M
            };
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
            Assert.That(Target.Shares, Is.EqualTo(10000));
        }

        [Then]
        public void ThePricePropertyIsCorrect()
        {
            Assert.That(Target.Price, Is.EqualTo(1000.5M));
        }

        [Then]
        public void TheTaxPropertyyIsCorrect()
        {
            Assert.That(Target.Tax, Is.EqualTo(100.5M));
        }

        [Then]
        public void TheCommissionPropertyIsCorrect()
        {
            Assert.That(Target.Commission, Is.EqualTo(10.5M));
        }

        [Then]
        public void TheLevyPropertyIsCorrect()
        {
            Assert.That(Target.Levy, Is.EqualTo(1.5M));
        }
    }
}
