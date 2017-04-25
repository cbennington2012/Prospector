using System;
using System.Collections.Generic;
using NUnit.Framework;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Engines;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Contracts.Wrappers;
using Prospector.Domain.Entities;
using Prospector.Presentation.ViewModelBuilders;
using Prospector.Presentation.ViewModels;
using StructureMap.Graph.Scanning;

namespace Prospector.UnitTests.Presentation.ViewModelBuilders.HoldingViewModelBuilderSpecs
{
    public class WhenIBuildTheViewModel : GivenA<HoldingViewModelBuilder, HoldingViewModel>
    {
        private readonly HoldingData _mockHoldingData = new HoldingData();

        private readonly HoldingViewModel _mockHoldingViewModel = new HoldingViewModel
        {
            Shares = 1000,
            Price = 100,
            Commission = 10,
            Tax = 1,
            Levy = 1,
            Percentage = 5
        };

        protected override void Given()
        {
            base.Given();

            GetMock<IAutoMapper>()
                .Setup(m => m.Map<HoldingData, HoldingViewModel>(_mockHoldingData))
                .Returns(_mockHoldingViewModel);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateCost(1000, 100, 10, 1, 1))
                .Returns(2000);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateBreakEvenPrice(1000, 100, 10, 1, 1))
                .Returns(200);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateProfitPrice(1000, 100, 10, 1, 1, 1.05M))
                .Returns(300);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateEarnings(1000, 300, 10, 2000, 1))
                .Returns(4000);
        }

        protected override void When()
        {
            base.When();

            Result = Target.BuildViewModel(_mockHoldingData);
        }

        [Then]
        public void TheAutoMapperMapsTheDataToTheViewModel()
        {
            Verify<IAutoMapper>(m => m.Map<HoldingData, HoldingViewModel>(_mockHoldingData));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheCost()
        {
            Verify<ICalculatorEngine>(m => m.CalculateCost(1000, 100, 10, 1, 1));
        }

        [Then]
        public void TheCostPropertyHasBeenSet()
        {
            Assert.That(Result.Cost, Is.EqualTo(2000));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheBreakEvenPrice()
        {
            Verify<ICalculatorEngine>(m => m.CalculateBreakEvenPrice(1000, 100, 10, 1, 1));
        }

        [Then]
        public void TheBreakEvenPropertyHasBeenSet()
        {
            Assert.That(Result.BreakEvenPrice, Is.EqualTo(200));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheProfitPrice()
        {
            Verify<ICalculatorEngine>(m => m.CalculateProfitPrice(1000, 100, 10, 1, 1, 1.05M));
        }

        [Then]
        public void TheProfitPricePropertyHasBeenSet()
        {
            Assert.That(Result.ProfitPrice, Is.EqualTo(300));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheEarnings()
        {
            Verify<ICalculatorEngine>(m => m.CalculateEarnings(1000, 300, 10, 2000, 1));
        }

        [Then]
        public void TheEarningsPropertyHasBeenSet()
        {
            Assert.That(Result.Earnings, Is.EqualTo(4000));
        }
    }

    public abstract class WhenIBuildTheViewModels : GivenA<HoldingViewModelBuilder, IList<HoldingViewModel>>
    {
        protected readonly TransactionData TransactionData = new TransactionData
        {
            Percentage = 2M,
            Shares = 1000,
            Price = 249.75M,
            Commission = 5.95M,
            Tax = 50,
            Levy = 1
        };

        private readonly HoldingViewModel _holdingViewModel = new HoldingViewModel();

        protected override void Given()
        {
            base.Given();
            
            GetMock<IAutoMapper>()
                .Setup(m => m.Map<TransactionData, HoldingViewModel>(TransactionData))
                .Returns(_holdingViewModel);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculatePercentage(2M))
                .Returns(1.002M);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateCost(1000, 249.75M, 5.95M, 50, 1))
                .Returns(950.50M);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateProfitPrice(1000, 249.75M, 5.95M, 50, 1, 1.002M))
                .Returns(255.5M);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateBreakEvenPrice(1000, 249.75M, 5.95M, 50, 1))
                .Returns(250.25M);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateEarnings(1000, 255.5M, 5.95M, 950.5M, 1))
                .Returns(150.75M);
        }

        protected override void When()
        {
            base.When();

            Result = Target.BuildViewModels();
        }
        
        [Then]
        public void TheAutomapperMapsTheDataToTheViewModel()
        {
            Verify<IAutoMapper>(m => m.Map<TransactionData, HoldingViewModel>(TransactionData));
        }

        [Then]
        public void TheCalculatorEngineCalculatesThePercentage()
        {
            Verify<ICalculatorEngine>(m => m.CalculatePercentage(2M));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheCost()
        {
            Verify<ICalculatorEngine>(m => m.CalculateCost(1000, 249.75M, 5.95M, 50, 1));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheProfitPrice()
        {
            Verify<ICalculatorEngine>(m => m.CalculateProfitPrice(1000, 249.75M, 5.95M, 50, 1, 1.002M));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheBreakEvenPrice()
        {
            Verify<ICalculatorEngine>(m => m.CalculateBreakEvenPrice(1000, 249.75M, 5.95M, 50, 1));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheEarnings()
        {
            Verify<ICalculatorEngine>(m => m.CalculateEarnings(1000, 255.5M, 5.95M, 950.5M, 1));
        }

        [Then]
        public void TheViewModelContainsTheCost()
        {
            Assert.That(_holdingViewModel.Cost, Is.EqualTo(950.5));
        }

        [Then]
        public void TheViewModelContainsTheBreakEvenPrice()
        {
            Assert.That(_holdingViewModel.BreakEvenPrice, Is.EqualTo(250.25));
        }

        [Then]
        public void TheViewModelContainsTheProfitPrice()
        {
            Assert.That(_holdingViewModel.ProfitPrice, Is.EqualTo(255.5));
        }

        [Then]
        public void TheViewModelContainsTheEarnings()
        {
            Assert.That(_holdingViewModel.Earnings, Is.EqualTo(150.75));
        }

        [Then]
        public void TheResultContainsTheViewModel()
        {
            Assert.That(Result.Contains(_holdingViewModel));
        }
    }

    public class WhenICanAccessTheDatabase : WhenIBuildTheViewModels
    {
        protected override void Given()
        {
            GetMock<ITransactionRepository>()
                .Setup(m => m.GetCurrentHoldings())
                .Returns(new List<TransactionData>
                {
                    TransactionData
                });

            base.Given();
        }

        [Then]
        public void TheTransactionRepositoryGetsTheCurrentHoldings()
        {
            Verify<ITransactionRepository>(m => m.GetCurrentHoldings());
        }
    }

    public class WhenICannotAccessTheDatabase : WhenIBuildTheViewModels
    {
        protected override void Given()
        {
            GetMock<ITransactionRepository>()
                .Setup(m => m.GetCurrentHoldings())
                .Throws(new Exception());

            GetMock<ITransactionFileWrapper>()
                .Setup(m => m.GetCurrentHoldings())
                .Returns(new List<TransactionData>
                {
                    TransactionData
                });

            base.Given();
        }

        [Then]
        public void TheTransactionFileWrapperGetsTheCurrentHoldings()
        {
            Verify<ITransactionFileWrapper>(m => m.GetCurrentHoldings());
        }
    }
}
