using System.Web.Mvc;
using Prospector.Domain.Contracts.Engines;
using Prospector.Presentation.ViewModels;

namespace Prospector.Web.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICalculatorEngine _calculatorEngine;

        public CalculatorController(ICalculatorEngine calculatorEngine)
        {
            _calculatorEngine = calculatorEngine;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var viewResult = new ViewResult
            {
                ViewName = "Index",
                ViewData = {Model = new CalculatorViewModel
                {
                    Commission = 5.95M,
                    Levy = 0M,
                    Profit = 2M
                } }
            };

            return viewResult;
        }

        [HttpPost]
        public ActionResult Index(CalculatorViewModel viewModel)
        {
            viewModel.Shares = _calculatorEngine.CalculateShares(viewModel.Investment, viewModel.Commission, viewModel.Tax, 
                viewModel.Levy, viewModel.Price);

            viewModel.Cost = _calculatorEngine.CalculateCost(viewModel.Shares, viewModel.Price, viewModel.Commission,
                viewModel.Tax, viewModel.Levy);

            viewModel.BreakEvenPrice = _calculatorEngine.CalculateBreakEvenPrice(viewModel.Shares, viewModel.Price,
                viewModel.Commission, viewModel.Tax, viewModel.Levy);

            var profitPercentage = _calculatorEngine.CalculatePercentage(viewModel.Profit);

            viewModel.ProfitPrice = _calculatorEngine.CalculateProfitPrice(viewModel.Shares, viewModel.Price,
                viewModel.Commission, viewModel.Tax, viewModel.Levy, profitPercentage);

            viewModel.Earnings = _calculatorEngine.CalculateEarnings(viewModel.Shares, viewModel.ProfitPrice,
                viewModel.Commission, viewModel.Cost, viewModel.Levy);

            var viewResult = new ViewResult
            {
                ViewName = "Index",
                ViewData = {Model = viewModel}
            };
            
            viewResult.ViewBag.Message = "Investment Calculated";

            return viewResult;
        }
    }
}