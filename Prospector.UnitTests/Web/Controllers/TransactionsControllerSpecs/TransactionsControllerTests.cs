using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Factories;
using Prospector.Domain.Contracts.Providers;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Entities;
using Prospector.Domain.Enumerations;
using Prospector.Presentation.ViewModels;
using Prospector.Web.Controllers;
using StructureMap.Graph.Scanning;

namespace Prospector.UnitTests.Web.Controllers.TransactionsControllerSpecs
{
    public class WhenIGetTheIndexView : GivenA<TransactionsController, ActionResult>
    {
        private readonly DateTime _startDate = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _endDate = DateTime.UtcNow;

        private readonly TransactionData _transactionData = new TransactionData();
        private readonly TransactionViewModel _transactionViewModel = new TransactionViewModel();

        protected override void Given()
        {
            base.Given();

            GetMock<IDateTimeProvider>()
                .Setup(m => m.GetTransactionStartDate(It.IsAny<DateTime>()))
                .Returns(_startDate);

            GetMock<IDateTimeProvider>()
                .Setup(m => m.GetTransactionEndDate(It.IsAny<DateTime>()))
                .Returns(_endDate);

            GetMock<IDateTimeProvider>()
                .Setup(m => m.GetTotalNumberOfMonths(_startDate, _endDate))
                .Returns(2);

            GetMock<ITransactionRepository>()
                .Setup(m => m.GetTransactions(_startDate, _endDate))
                .Returns(new List<TransactionData>
                {
                    _transactionData
                });

            GetMock<IAutoMapper>()
                .Setup(m => m.Map<TransactionData, TransactionViewModel>(_transactionData))
                .Returns(_transactionViewModel);

            GetMock<IAppSettingProvider>()
                .Setup(m => m.Get("MonthlyTarget"))
                .Returns("3000");

            GetMock<IAppSettingProvider>()
                .Setup(m => m.Get("TaxFreeAllowance"))
                .Returns("11500");
        }

        protected override void When()
        {
            base.When();

            Result = Target.Index();
        }

        [Then]
        public void TheDateTimeProviderGetsTheTransactionStartDate()
        {
            Verify<IDateTimeProvider>(m => m.GetTransactionStartDate(It.IsAny<DateTime>()));
        }

        [Then]
        public void TheDateTimeProviderGetsTheTransactionEndDate()
        {
            Verify<IDateTimeProvider>(m => m.GetTransactionEndDate(It.IsAny<DateTime>()));
        }

        [Then]
        public void TheDateTimeProviderGetsTheNumberOfMonths()
        {
            Verify<IDateTimeProvider>(m => m.GetTotalNumberOfMonths(_startDate, _endDate));
        }

        [Then]
        public void TheTransactionRepositoryGetsTheTransactions()
        {
            Verify<ITransactionRepository>(m => m.GetTransactions(_startDate, _endDate));
        }

        [Then]
        public void TheAutoMapperMapsTheDataToTheViewModel()
        {
            Verify<IAutoMapper>(m => m.Map<TransactionData, TransactionViewModel>(_transactionData));
        }

        [Then]
        public void TheAppSettingProviderGetsTheMonthlyTargetValue()
        {
            Verify<IAppSettingProvider>(m => m.Get("MonthlyTarget"));
        }

        [Then]
        public void TheAppSettingProviderGetsTheTaxFreeAllowanceValue()
        {
            Verify<IAppSettingProvider>(m => m.Get("TaxFreeAllowance"));
        }

        [Then]
        public void TheTransactionFactoryGetsTheTransactionPeriodValue()
        {
            Verify<ITransactionFactory>(m => m.GetTransactionPeriodValue(It.Is<IList<TransactionData>>(x => x.Contains(_transactionData))));
        }

        [Then]
        public void TheResultIsAViewResult()
        {
            Assert.That(Result, Is.AssignableTo<ViewResult>());
        }

        [Then]
        public void TheViewModelIsCorrect()
        {
            Assert.That((Result as ViewResult).ViewData, Is.Not.Null);
        }

        [Then]
        public void TheStartDatePropertyIsOnTheViewModel()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as TransactionSearchViewModel).StartDate, Is.EqualTo(_startDate));
        }

        [Then]
        public void TheEndDatePropertyIsOnTheViewModel()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as TransactionSearchViewModel).EndDate, Is.EqualTo(_endDate));
        }

        [Then]
        public void TheResultsPropertyIsOnTheViewModel()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as TransactionSearchViewModel).Results, Is.Not.Null);
        }

        [Then]
        public void TheMonthlyTargetPropertyIsCorrectOnTheViewModel()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as TransactionSearchViewModel).MonthlyTarget, Is.EqualTo(6000));
        }
    }

    public class WhenIPostToTheIndexView : GivenA<TransactionsController, ActionResult>
    {
        private readonly DateTime _startDate = DateTime.UtcNow.AddDays(-1);
        private readonly DateTime _endDate = DateTime.UtcNow;
        private readonly TransactionData _transactionData = new TransactionData();
        private readonly TransactionViewModel _transactionViewModel = new TransactionViewModel();

        private TransactionSearchViewModel _viewModel;

        protected override void Given()
        {
            base.Given();

            _viewModel = new TransactionSearchViewModel
            {
                StartDate = _startDate,
                EndDate = _endDate
            };

            GetMock<ITransactionRepository>()
                .Setup(m => m.GetTransactions(_startDate, _endDate))
                .Returns(new List<TransactionData>
                {
                    _transactionData
                });

            GetMock<IAutoMapper>()
                .Setup(m => m.Map<TransactionData, TransactionViewModel>(_transactionData))
                .Returns(_transactionViewModel);

            GetMock<IAppSettingProvider>()
                .Setup(m => m.Get("MonthlyTarget"))
                .Returns("3000");

            GetMock<IDateTimeProvider>()
                .Setup(m => m.GetTotalNumberOfMonths(_startDate, _endDate))
                .Returns(2);
        }

        protected override void When()
        {
            base.When();

            Result = Target.Index(_viewModel);
        }

        [Then]
        public void TheDateTimeProviderGetsTheTotalNumberOfMonths()
        {
            Verify<IDateTimeProvider>(m => m.GetTotalNumberOfMonths(_startDate, _endDate));
        }

        [Then]
        public void TheTransactionRepositoryGetsTheData()
        {
            Verify<ITransactionRepository>(m => m.GetTransactions(_startDate, _endDate));
        }

        [Then]
        public void TheAutoMapperMapsTheDataToTheViewModel()
        {
            Verify<IAutoMapper>(m => m.Map<TransactionData, TransactionViewModel>(_transactionData));
        }

        [Then]
        public void TheAppSettingProviderGetsTheMonthlyTargetValue()
        {
            Verify<IAppSettingProvider>(m => m.Get("MonthlyTarget"));
        }

        [Then]
        public void TheTransactionFactoryGetsTheTransactionPeriodValue()
        {
            Verify<ITransactionFactory>(m => m.GetTransactionPeriodValue(It.Is<IList<TransactionData>>(x => x.Contains(_transactionData))));
        }

        [Then]
        public void TheViewDataHasTheResults()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as TransactionSearchViewModel).Results.Contains(_transactionViewModel));
        }

        [Then]
        public void TheMonthlyTargetPropertyIsCorrectOnTheViewModel()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as TransactionSearchViewModel).MonthlyTarget, Is.EqualTo(6000));
        }
    }

    public class WhenIGetTheAddView : GivenA<TransactionsController, ActionResult>
    {
        private readonly TransactionViewModel _transactionViewModel = new TransactionViewModel();

        protected override void When()
        {
            base.When();

            Result = Target.Add(_transactionViewModel);
        }

        [Then]
        public void TheResultIsAViewResult()
        {
            Assert.That(Result, Is.AssignableTo<ViewResult>());
        }
    }

    public class WhenIPostToTheAddView : GivenA<TransactionsController, ActionResult>
    {
        private readonly TransactionViewModel _viewModel = new TransactionViewModel
        {
            TransactionType = TransactionType.Buy,
            Code = "TST"
        };

        private readonly TransactionData _transactionData = new TransactionData();

        protected override void Given()
        {
            base.Given();

            GetMock<IAutoMapper>()
                .Setup(m => m.Map<TransactionViewModel, TransactionData>(_viewModel))
                .Returns(_transactionData);
        }

        protected override void When()
        {
            base.When();

            Result = Target.Add(_viewModel, String.Empty);
        }

        [Then]
        public void TheAutoMapperMapsTheViewModelToTheData()
        {
            Verify<IAutoMapper>(m => m.Map<TransactionViewModel, TransactionData>(_viewModel));
        }

        [Then]
        public void TheTransactionRepositoryAddsTheData()
        {
            Verify<ITransactionRepository>(m => m.AddTransaction(_transactionData));
        }

        [Then]
        public void TheResultIsAViewResult()
        {
            Assert.That(Result, Is.AssignableTo<ViewResult>());
        }

        [Then]
        public void TheViewNameIsCorrect()
        {
            Assert.That((Result as ViewResult).ViewName, Is.EqualTo("Add"));
        }

        [Then]
        public void TheViewBagMessageIsCorrect()
        {
            Assert.That((Result as ViewResult).ViewBag.Message, Is.EqualTo("Buy transaction for TST completed"));
        }
    }

    public class WhenIGetTheSellView : GivenA<TransactionsController, ActionResult>
    {
        private const String Id = "Id";

        private readonly TransactionData _mockData = new TransactionData();
        private readonly TransactionViewModel _mockViewModel = new TransactionViewModel();

        protected override void Given()
        {
            base.Given();

            GetMock<ITransactionRepository>()
                .Setup(m => m.GetTransactionById(Id))
                .Returns(_mockData);

            GetMock<IAutoMapper>()
                .Setup(m => m.Map<TransactionData, TransactionViewModel>(_mockData))
                .Returns(_mockViewModel);
        }

        protected override void When()
        {
            base.When();

            Result = Target.Sell(Id);
        }

        [Then]
        public void TheTransactionRepositoryGetsTheTransactionById()
        {
            Verify<ITransactionRepository>(m => m.GetTransactionById(Id));
        }

        [Then]
        public void TheAutoMapperMapsTheDataToTheViewModel()
        {
            Verify<IAutoMapper>(m => m.Map<TransactionData, TransactionViewModel>(_mockData));
        }

        [Then]
        public void TheResultIsAViewResult()
        {
            Assert.That(Result, Is.AssignableTo<ViewResult>());
        }
    }

    public class WhenIPostToTheSellView : GivenA<TransactionsController, ActionResult>
    {
        private readonly TransactionViewModel _viewModel = new TransactionViewModel
        {
            TransactionType = TransactionType.Sell,
            Code = "TST"
        };

        private readonly TransactionData _transactionData = new TransactionData();

        protected override void Given()
        {
            base.Given();

            GetMock<IAutoMapper>()
                .Setup(m => m.Map<TransactionViewModel, TransactionData>(_viewModel))
                .Returns(_transactionData);
        }

        protected override void When()
        {
            base.When();

            Result = Target.Sell(_viewModel);
        }

        [Then]
        public void TheAutoMapperMapsTheViewModelToTheData()
        {
            Verify<IAutoMapper>(m => m.Map<TransactionViewModel, TransactionData>(_viewModel));
        }

        [Then]
        public void TheTransactionRepositoryAddsTheData()
        {
            Verify<ITransactionRepository>(m => m.AddTransaction(_transactionData));
        }

        [Then]
        public void TheResultIsAViewResult()
        {
            Assert.That(Result, Is.AssignableTo<ViewResult>());
        }

        [Then]
        public void TheViewNameIsCorrect()
        {
            Assert.That((Result as ViewResult).ViewName, Is.EqualTo("Sell"));
        }

        [Then]
        public void TheViewBagMessageIsCorrect()
        {
            Assert.That((Result as ViewResult).ViewBag.Message, Is.EqualTo("Sell transaction for TST completed"));
        }
    }
}