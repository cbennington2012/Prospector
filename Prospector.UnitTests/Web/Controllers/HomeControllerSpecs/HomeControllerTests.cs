using System.Web.Mvc;
using NUnit.Framework;
using Prospector.Web.Controllers;
using StructureMap.Graph.Scanning;

namespace Prospector.UnitTests.Web.Controllers.HomeControllerSpecs
{
    public class WhenIGetTheHomeController : GivenA<HomeController, ActionResult>
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
}
