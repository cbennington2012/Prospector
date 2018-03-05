using System.Linq;
using System.Web.Mvc;
using Prospector.Presentation.Contracts;

namespace Prospector.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardViewModelBuilder _dashboardViewModelBuilder;

        public DashboardController(IDashboardViewModelBuilder dashboardViewModelBuilder)
        {
            _dashboardViewModelBuilder = dashboardViewModelBuilder;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewModels = _dashboardViewModelBuilder.BuildViewModels();
            return View(viewModels.ToArray());
        }
    }
}