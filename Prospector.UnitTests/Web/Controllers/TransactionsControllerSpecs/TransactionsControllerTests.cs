using System;
using System.Web.Mvc;
using NUnit.Framework;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Entities;
using Prospector.Domain.Enumerations;
using Prospector.Presentation.ViewModels;
using Prospector.Web.Controllers;

namespace Prospector.UnitTests.Web.Controllers.TransactionsControllerSpecs
{
    public class WhenIGetTheIndexView : GivenA<TransactionsController, ActionResult>
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
            Verify<ITransactionRepository>(m => m.Add(_transactionData));
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
