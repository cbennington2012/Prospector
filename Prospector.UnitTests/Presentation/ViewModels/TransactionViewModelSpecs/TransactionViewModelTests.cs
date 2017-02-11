using System;
using NUnit.Framework;
using Prospector.Domain.Enumerations;
using Prospector.Presentation.ViewModels;

namespace Prospector.UnitTests.Presentation.ViewModels.TransactionViewModelSpecs
{
    public class WhenIGetATransactionViewModel : TestBase<TransactionViewModel>
    {
        private readonly DateTime _date = DateTime.UtcNow;
        private readonly Guid _id = Guid.NewGuid();
        private readonly Guid _sellTransactionId = Guid.NewGuid();

        protected override void When()
        {
            base.When();

            Target = new TransactionViewModel
            {
                Id = _id.ToString(),
                TransactionType = TransactionType.Sell,
                Code = "TST",
                Date = _date,
                Shares = 1000,
                Price = 249.75M,
                Tax = 50,
                Commission = 5.95M,
                Levy = 1,
                Percentage = 2.5M,
                SellTransactionId = _sellTransactionId.ToString()
            };
        }

        [Then]
        public void TheIdPropertyIsCorrect()
        {
            Assert.That(Target.Id, Is.EqualTo(_id.ToString()));
        }

        [Then]
        public void TheTransactionTypeIsCorrect()
        {
            Assert.That(Target.TransactionType, Is.EqualTo(TransactionType.Sell));
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
        public void ThePercentagePropertyIsCorrect()
        {
            Assert.That(Target.Percentage, Is.EqualTo(2.5));
        }

        [Then]
        public void TheSellTransactionIdPropertyIsCorrect()
        {
            Assert.That(Target.SellTransactionId, Is.EqualTo(_sellTransactionId.ToString()));
        }
    }
}
