using System.Web.Mvc;
using NUnit.Framework;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Engines;
using Prospector.Presentation.ViewModels;
using Prospector.Web.Controllers;

namespace Prospector.UnitTests.Web.Controllers.CalculatorControllerSpecs
{
    public class WhenIGetTheIndexView : GivenA<CalculatorController, ActionResult>
    {
        protected override void When()
        {
            base.When();

            Result = Target.Index();
        }

        [Then]
        public void TheResultIsAViewResult()
        {
            Assert.That(Result, Is.AssignableTo<ViewResult>());
        }

        [Then]
        public void TheViewNameIsCorrect()
        {
            Assert.That((Result as ViewResult).ViewName, Is.EqualTo("Index"));
        }

        [Then]
        public void TheViewModelIsPresent()
        {
            Assert.IsNotNull((Result as ViewResult).ViewData);
        }

        [Then]
        public void TheModelIsACalculatorViewModel()
        {
            Assert.That((Result as ViewResult).ViewData.Model, Is.AssignableTo<CalculatorViewModel>());
        }

        [Then]
        public void TheCommissionValueIsCorrect()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as CalculatorViewModel).Commission, Is.EqualTo(5.95));
        }

        [Then]
        public void TheLevyValueIsCorrect()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as CalculatorViewModel).Levy, Is.EqualTo(0));
        }

        [Then]
        public void TheProfitValueIsCorrect()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as CalculatorViewModel).Profit, Is.EqualTo(2));
        }
    }

    public class WhenIPostToTheIndexForTheCalculateOption : GivenA<CalculatorController, ActionResult>
    {
        private readonly CalculatorViewModel _viewModel = new CalculatorViewModel
        {
            Investment = 10000,
            Commission = 5.95M,
            Tax = 50,
            Levy = 1,
            Price = 249.75M,
            Profit = 2M
        };

        protected override void Given()
        {
            base.Given();

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateShares(10000, 5.95M, 50, 1, 249.75M))
                .Returns(1000);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateCost(1000, 249.75M, 5.95M, 50, 1))
                .Returns(9500.50M);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateBreakEvenPrice(1000, 249.75M, 5.95M, 50, 1))
                .Returns(250.25M);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculatePercentage(2M))
                .Returns(1.002M);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateProfitPrice(1000, 249.75M, 5.95M, 50, 1, 1.002M))
                .Returns(255.5M);

            GetMock<ICalculatorEngine>()
                .Setup(m => m.CalculateEarnings(1000, 255.5M, 5.95M, 9500.50M, 1))
                .Returns(150.5M);
        }

        protected override void When()
        {
            base.When();

            Result = Target.Index(_viewModel, "calculate");
        }

        [Then]
        public void TheResultIsAViewResult()
        {
            Assert.That(Result, Is.AssignableTo<ViewResult>());
        }

        [Then]
        public void TheViewNameIsCorrect()
        {
            Assert.That((Result as ViewResult).ViewName, Is.EqualTo("Index"));
        }

        [Then]
        public void TheViewBagMessageIsCorrect()
        {
            Assert.That((Result as ViewResult).ViewBag.Message, Is.EqualTo("Investment Calculated"));
        }

        [Then]
        public void TheViewModelIsPresent()
        {
            Assert.IsNotNull((Result as ViewResult).ViewData);
        }

        [Then]
        public void TheModelIsACalculatorViewModel()
        {
            Assert.That((Result as ViewResult).ViewData.Model, Is.AssignableTo<CalculatorViewModel>());
        }

        [Then]
        public void TheCommissionValueIsCorrect()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as CalculatorViewModel).Commission, Is.EqualTo(5.95));
        }

        [Then]
        public void TheLevyValueIsCorrect()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as CalculatorViewModel).Levy, Is.EqualTo(1));
        }

        [Then]
        public void TheProfitValueIsCorrect()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as CalculatorViewModel).Profit, Is.EqualTo(2));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheSharesAmount()
        {
            Verify<ICalculatorEngine>(m => m.CalculateShares(10000, 5.95M, 50, 1, 249.75M));
        }

        [Then]
        public void TheSharesValueIsCorrect()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as CalculatorViewModel).Shares, Is.EqualTo(1000));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheCostAmount()
        {
            Verify<ICalculatorEngine>(m => m.CalculateCost(1000, 249.75M, 5.95M, 50, 1));
        }

        [Then]
        public void TheCostValueIsCorrect()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as CalculatorViewModel).Cost, Is.EqualTo(9500.50));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheBreakEvenPrice()
        {
            Verify<ICalculatorEngine>(m => m.CalculateBreakEvenPrice(1000, 249.75M, 5.95M, 50, 1));
        }

        [Then]
        public void TheBreakEvenPriceValueIsCorrect()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as CalculatorViewModel).BreakEvenPrice, Is.EqualTo(250.25));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheProfitPercentage()
        {
            Verify<ICalculatorEngine>(m => m.CalculatePercentage(2M));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheProfitPrice()
        {
            Verify<ICalculatorEngine>(m => m.CalculateProfitPrice(1000, 249.75M, 5.95M, 50, 1, 1.002M));
        }

        [Then]
        public void TheProfitPriceValueIsCorrect()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as CalculatorViewModel).ProfitPrice, Is.EqualTo(255.5));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheEarnings()
        {
            Verify<ICalculatorEngine>(m => m.CalculateEarnings(1000, 255.5M, 5.95M, 9500.50M, 1));
        }

        [Then]
        public void TheEarningsValueIsCorrect()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as CalculatorViewModel).Earnings, Is.EqualTo(150.5));
        }
    }

    public class WhenIPostToTheIndexViewForTheAddHoldingsOption : GivenA<CalculatorController, ActionResult>
    {
        private readonly CalculatorViewModel _viewModel = new CalculatorViewModel
        {
            Investment = 10000,
            Commission = 5.95M,
            Tax = 50,
            Levy = 1,
            Price = 249.75M,
            Profit = 2M
        };

        private readonly TransactionViewModel _transactionViewModel = new TransactionViewModel();

        protected override void Given()
        {
            base.Given();

            GetMock<IAutoMapper>()
                .Setup(m => m.Map<CalculatorViewModel, TransactionViewModel>(_viewModel))
                .Returns(_transactionViewModel);
        }

        protected override void When()
        {
            base.When();

            Result = Target.Index(_viewModel, "");
        }

        [Then]
        public void TheAutoMappperMapsTheViewModels()
        {
            Verify<IAutoMapper>(m => m.Map<CalculatorViewModel, TransactionViewModel>(_viewModel));
        }

        [Then]
        public void TheCalculatorEngineCalculatesTheShares()
        {
            Verify<ICalculatorEngine>(m => m.CalculateShares(10000, 5.95M, 50, 1, 249.75M));
        }

        [Then]
        public void TheResultIsAViewResult()
        {
            Assert.That(Result, Is.AssignableTo<ViewResult>());
        }
    }
}
