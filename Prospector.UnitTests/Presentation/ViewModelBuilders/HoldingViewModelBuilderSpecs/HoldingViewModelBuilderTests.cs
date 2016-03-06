using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Engines;
using Prospector.Domain.Entities;
using Prospector.Presentation.ViewModelBuilders;
using Prospector.Presentation.ViewModels;

namespace Prospector.UnitTests.Presentation.ViewModelBuilders.HoldingViewModelBuilderSpecs
{
    public class WhenIBuildTheViewModels : GivenA<HoldingViewModelBuilder, HoldingViewModel>
    {
        private readonly HoldingData _mockHoldingData = new HoldingData();

        private readonly HoldingViewModel _mockHoldingViewModel = new HoldingViewModel
        {
            Shares = 1000,
            Price = 100,
            Commission = 10,
            Tax = 1
        };

        protected override void Given()
        {
            base.Given();

            GetMock<IAutoMapper>()
                .Setup(m => m.Map<HoldingData, HoldingViewModel>(_mockHoldingData))
                .Returns(_mockHoldingViewModel);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateCost(1000, 100, 10, 1))
                .Returns(2000);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateBreakEvenPrice(1000, 100, 10, 1))
                .Returns(200);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateProfitPrice(1000, 100, 10, 1, 1.025M))
                .Returns(300);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateEarnings(1000, 300, 10, 2000))
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
            Verify<ICalculatorEngine>(m => m.CalculateCost(1000, 100, 10, 1));
        }

        [Then]
        public void TheCostPropertyHasBeenSet()
        {
            Assert.That(Result.Cost, Is.EqualTo(2000));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheBreakEvenPrice()
        {
            Verify<ICalculatorEngine>(m => m.CalculateBreakEvenPrice(1000, 100, 10, 1));
        }

        [Then]
        public void TheBreakEvenPropertyHasBeenSet()
        {
            Assert.That(Result.BreakEvenPrice, Is.EqualTo(200));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheProfitPrice()
        {
            Verify<ICalculatorEngine>(m => m.CalculateProfitPrice(1000, 100, 10, 1, 1.025M));
        }

        [Then]
        public void TheProfitPricePropertyHasBeenSet()
        {
            Assert.That(Result.ProfitPrice, Is.EqualTo(300));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheEarnings()
        {
            Verify<ICalculatorEngine>(m => m.CalculateEarnings(1000, 300, 10, 2000));
        }

        [Then]
        public void TheEarningsPropertyHasBeenSet()
        {
            Assert.That(Result.Earnings, Is.EqualTo(4000));
        }
    }
}
