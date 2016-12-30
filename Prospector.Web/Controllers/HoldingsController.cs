using System.Linq;
using System.Web.Mvc;
using Prospector.Presentation.Contracts;

namespace Prospector.Web.Controllers
{
    public class HoldingsController : Controller
    {
        private readonly IHoldingViewModelBuilder _holdingViewModelBuilder;

        public HoldingsController(IHoldingViewModelBuilder holdingViewModelBuilder)
        {
            _holdingViewModelBuilder = holdingViewModelBuilder;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var data = _holdingViewModelBuilder.BuildViewModels();

            var viewResult = new ViewResult
            {
                ViewName = "Index",
                ViewData = { Model = data.ToArray() }
            };

            return viewResult;
        }
    }
}