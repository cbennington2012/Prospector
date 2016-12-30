using System;
using NUnit.Framework;
using Prospector.Domain.Entities;

namespace Prospector.UnitTests.Domain.Entities.TransactionDataSpecs
{
    public class WhenIGetATransactionDataObject : TestBase<TransactionData>
    {
        private readonly DateTime _date = DateTime.UtcNow;

        protected override void When()
        {
            base.When();

            Target = new TransactionData
            {
                Code = "TST",
                Date = _date,
                Shares = 1000,
                Price = 249.75M,
                Tax = 50,
                Commission = 5.95M,
                Levy = 1,
                Percentage = 2.5M
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
            Assert.That(Target.Date, Is.EqualTo(_date));
        }

        [Then]
        public void TheSharesPropertyIsCorrect()
        {
            Assert.That(Target.Shares, Is.EqualTo(1000));
        }

        [Then]
        public void ThePricePropertyIsCorrect()
        {
            Assert.That(Target.Price, Is.EqualTo(249.75));
        }

        [Then]
        public void TheTaxPropertyIsCorrect()
        {
            Assert.That(Target.Tax, Is.EqualTo(50));
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
        public void ThePropertyIsCorrect()
        {
            Assert.That(Target.Percentage, Is.EqualTo(2.5));
        }
    }
}
