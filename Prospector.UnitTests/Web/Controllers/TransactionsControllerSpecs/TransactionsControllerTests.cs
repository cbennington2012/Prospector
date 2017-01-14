﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Prospector.Domain.Contracts.AutoMapping;
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
        }

        protected override void When()
        {
            base.When();

            Result = Target.Index(_viewModel);
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
        public void TheViewDataHasTheResults()
        {
            Assert.That(((Result as ViewResult).ViewData.Model as TransactionSearchViewModel).Results.Contains(_transactionViewModel));
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
            Assert.That((Result as ViewResult).ViewName, Is.EqualTo("Index"));
        }

        [Then]
        public void TheViewBagMessageIsCorrect()
        {
            Assert.That((Result as ViewResult).ViewBag.Message, Is.EqualTo("Buy transaction for TST completed"));
        }
    }
}
