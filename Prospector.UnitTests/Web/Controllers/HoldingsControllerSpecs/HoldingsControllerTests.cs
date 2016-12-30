using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NUnit.Framework;
using Prospector.Presentation.Contracts;
using Prospector.Presentation.ViewModels;
using Prospector.Web.Controllers;

namespace Prospector.UnitTests.Web.Controllers.HoldingsControllerSpecs
{
    public class WhenIGetTheIndexView : GivenA<HoldingsController, ActionResult>
    {
        private readonly IList<HoldingViewModel> _viewModels = new List<HoldingViewModel>();

        protected override void Given()
        {
            base.Given();

            GetMock<IHoldingViewModelBuilder>()
                .Setup(m => m.BuildViewModels())
                .Returns(_viewModels);
        }

        protected override void When()
        {
            base.When();

            Result = Target.Index();
        }

        [Then]
        public void TheHoldingViewModelBuilderBuildsTheViewModels()
        {
            Verify<IHoldingViewModelBuilder>(m => m.BuildViewModels());
        }

        [Then]
        public void TheResultIsAViewResult()
        {
            Assert.That(Result, Is.AssignableTo<ViewResult>());
        }

        [Then]
        public void TheViewResultNameIsCorrect()
        {
            Assert.That((Result as ViewResult).ViewName, Is.EqualTo("Index"));
        }

        [Then]
        public void TheViewDataIsCorrect()
        {
            Assert.That((Result as ViewResult).ViewData.Model, Is.EqualTo(_viewModels.ToArray()));
        }
    }
}
