using System;
using System.Collections.Generic;
using NUnit.Framework;
using Prospector.Domain.Contracts.Engines;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Entities;
using Prospector.Domain.Enumerations;
using Prospector.Domain.Factories;

namespace Prospector.UnitTests.Domain.Factories.TransactionFactorySpecs
{
    public class WhenIGetTheTransactionPeriodValue : GivenA<TransactionFactory, Decimal>
    {
        private readonly TransactionData _mockBuyTransaction = new TransactionData
        {
            TransactionType = TransactionType.Buy,
            Id = "123",
            Shares = 100,
            Price = 50,
            Commission = 8.95M,
            Tax = 10,
            Levy = 0
        };

        private readonly TransactionData _mockSellTransaction = new TransactionData
        {
            TransactionType = TransactionType.Sell,
            Id = "456",
            Shares = 100,
            Price = 75,
            Commission = 5.95M,
            Tax = 10,
            Levy = 0,
            SellTransactionId = "123"
        };

        private readonly TransactionData _mockOpenTransaction = new TransactionData
        {
            TransactionType = TransactionType.Buy,
            Id = "789",
            Shares = 100,
            Price = 50,
            Commission = 5.95M,
            Tax = 10,
            Levy = 0
        };

        private readonly TransactionData _mockBuyOutsidePeriodTransaction = new TransactionData
        {
            TransactionType = TransactionType.Buy,
            Id = "abc",
            Shares = 100,
            Price = 50,
            Commission = 8.95M,
            Tax = 10,
            Levy = 0
        };

        private readonly TransactionData _mockSellOutsidePeriodTransaction = new TransactionData
        {
            TransactionType = TransactionType.Sell,
            Id = "xyz",
            Shares = 100,
            Price = 75,
            Commission = 5.95M,
            Tax = 10,
            Levy = 0,
            SellTransactionId = "abc"
        };

        protected override void Given()
        {
            base.Given();

            GetMock<ITransactionRepository>()
                .Setup(m => m.GetTransactionById("abc"))
                .Returns(_mockBuyOutsidePeriodTransaction);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateCost(100, 50, 8.95M, 10, 0))
                .Returns(100);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateCost(100, 75, 5.95M, 10, 0))
                .Returns(175);
        }

        protected override void When()
        {
            base.When();

            Result = Target.GetTransactionPeriodValue(new List<TransactionData>
            {
                _mockBuyTransaction,
                _mockSellTransaction,
                _mockOpenTransaction,
                _mockSellOutsidePeriodTransaction
            });
        }

        [Then]
        public void TheTransactionRepositoryGetsTheMissingBuyTransaction()
        {
            Verify<ITransactionRepository>(m => m.GetTransactionById("abc"));
        }

        [Then]
        public void TheCalculatorEngineGetsTheBuyTransactionCost()
        {
            Verify<ICalculatorEngine>(m => m.CalculateCost(100, 50, 8.95M, 10, 0));
        }

        [Then]
        public void TheCalculatorEngineGetsTheSellTransactionCost()
        {
            Verify<ICalculatorEngine>(m => m.CalculateCost(100, 75, 5.95M, 10, 0));
        }

        [Then]
        public void TheResultIsCorrect()
        {
            Assert.That(Result, Is.EqualTo(150));
        }
    }
}
