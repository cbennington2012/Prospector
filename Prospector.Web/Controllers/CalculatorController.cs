using System;
using System.Web.Mvc;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Engines;
using Prospector.Presentation.ViewModels;

namespace Prospector.Web.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICalculatorEngine _calculatorEngine;
        private readonly IAutoMapper _autoMapper;

        public CalculatorController(ICalculatorEngine calculatorEngine, IAutoMapper autoMapper)
        {
            _calculatorEngine = calculatorEngine;
            _autoMapper = autoMapper;
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
        public ActionResult Index(CalculatorViewModel viewModel, String calculate)
        {
            if (String.IsNullOrEmpty(calculate))
            {
                var transactionViewModel = _autoMapper.Map<CalculatorViewModel, TransactionViewModel>(viewModel);

                transactionViewModel.Shares = _calculatorEngine.CalculateShares(viewModel.Investment, viewModel.Commission,
                    viewModel.Tax,
                    viewModel.Levy, viewModel.Price);

                return View("../Transactions/Add", transactionViewModel);
            }
            else
            {
                viewModel.Shares = _calculatorEngine.CalculateShares(viewModel.Investment, viewModel.Commission,
                    viewModel.Tax,
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
}