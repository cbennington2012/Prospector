using System;
using System.Collections.Generic;
using System.Data;
using Moq;
using NUnit.Framework;
using Prospector.Domain.Contracts.Parsers;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Entities;
using Prospector.Domain.Enumerations;
using Prospector.Domain.Repositories;
using StructureMap.Graph.Scanning;

namespace Prospector.UnitTests.Domain.Repositories.TransactionRepositorySpecs
{
    public class WhenIGetTheCurrentHoldings : GivenA<TransactionRepository>
    {
        private DataTable _sqlData;

        protected override void Given()
        {
            base.Given();

            _sqlData = new DataTable();
            _sqlData.Columns.Add(new DataColumn("Testing"));
            _sqlData.Rows.Add(new[] {101});

            GetMock<IMySqlProvider>()
                .Setup(m => m.GetData("Prospector", "spGetCurrentHoldings", It.IsAny<IDictionary<String, Object>>()))
                .Returns(_sqlData);
        }

        protected override void When()
        {
            base.When();

            Target.GetCurrentHoldings();
        }

        [Then]
        public void TheMySqlDataProviderGetsTheData()
        {
            Verify<IMySqlProvider>(m => m.GetData("Prospector", "spGetCurrentHoldings", It.IsAny<IDictionary<String, Object>>()));
        }

        [Then]
        public void TheDataObjectParserGetsTheObjectFromTheDataRow()
        {
            Verify<IDataObjectParser>(m => m.GetObject<TransactionData>(It.IsAny<DataRow>()));
        }
    }

    public class WhenIAddATransactionData : GivenA<TransactionRepository>
    {
        private readonly TransactionData _transactionData = new TransactionData();

        protected override void When()
        {
            base.When();

            Target.Add(_transactionData);
        }

        [Then]
        public void TheMySqlProviderExecutesTheProcedure()
        {
            Verify<IMySqlProvider>(m => m.ExecuteProcedure("Prospector", "spInsertTransaction", It.IsAny<IDictionary<String, Object>>()));
        }
    }

    public class WhenIGetTheInsertParameters : GivenA<TransactionRepository, IDictionary<String, Object>>
    {
        private readonly Guid _id = Guid.NewGuid();
        private readonly DateTime _date = DateTime.UtcNow;

        private TransactionData _transactionData;

        protected override void Given()
        {
            base.Given();

            _transactionData = new TransactionData
            {
                Id = _id,
                TransactionType = TransactionType.Sell,
                Code = "Code",
                Date = _date,
                Shares = 5000,
                Price = 250.25M,
                Tax = 50.25M,
                Commission = 5.95M,
                Levy = 1,
                Percentage = 2.0M
            };
        }

        protected override void When()
        {
            base.When();

            Result = Target.GetInsertParameters(_transactionData);
        }

        [Then]
        public void TheIdParameterIsCorrect()
        {
            Assert.That(Result["@Id"], Is.EqualTo(_id));
        }

        [Then]
        public void TheTransactionTypeParameterIsCorrect()
        {
            Assert.That(Result["@TransactionType"], Is.EqualTo("Sell"));
        }

        [Then]
        public void TheCodeParameterIsCorrect()
        {
            Assert.That(Result["@Code"], Is.EqualTo("Code"));
        }

        [Then]
        public void TheDateParameterIsCorrect()
        {
            Assert.That(Result["@Date"], Is.EqualTo(_date.ToString("yyyy-MM-dd HH:mm:ss")));
        }

        [Then]
        public void TheSharesParameterIsCorrect()
        {
            Assert.That(Result["@Shares"], Is.EqualTo(5000));
        }

        [Then]
        public void ThePriceParameterIsCorrect()
        {
            Assert.That(Result["@Price"], Is.EqualTo(250.25));
        }

        [Then]
        public void TheTaxParameterIsCorrect()
        {
            Assert.That(Result["@Tax"], Is.EqualTo(50.25));
        }

        [Then]
        public void TheCommissionParameterIsCorrect()
        {
            Assert.That(Result["@Commission"], Is.EqualTo(5.95));
        }

        [Then]
        public void TheLevyParameterIsCorrect()
        {
            Assert.That(Result["@Levy"], Is.EqualTo(1));
        }

        [Then]
        public void ThePercentageParameterIsCorrect()
        {
            Assert.That(Result["@Percentage"], Is.EqualTo(2.0));
        }
    }
}
