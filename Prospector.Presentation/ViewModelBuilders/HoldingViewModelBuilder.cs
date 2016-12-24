using System;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Engines;
using Prospector.Domain.Entities;
using Prospector.Presentation.Contracts;
using Prospector.Presentation.ViewModels;

namespace Prospector.Presentation.ViewModelBuilders
{
    public class HoldingViewModelBuilder : IHoldingViewModelBuilder
    {
        private readonly IAutoMapper _autoMapper;
        private readonly ICalculatorEngine _calculatorEngine;

        public HoldingViewModelBuilder(IAutoMapper autoMapper, ICalculatorEngine calculatorEngine)
        {
            _autoMapper = autoMapper;
            _calculatorEngine = calculatorEngine;
        }

        public HoldingViewModel BuildViewModel(HoldingData data)
        {
            var viewModel = _autoMapper.Map<HoldingData, HoldingViewModel>(data);

            var cost = _calculatorEngine.CalculateCost(viewModel.Shares, viewModel.Price, viewModel.Commission, viewModel.Tax, viewModel.Levy);
            viewModel.Cost = cost;
            viewModel.BreakEvenPrice = _calculatorEngine.CalculateBreakEvenPrice(viewModel.Shares, viewModel.Price, viewModel.Commission, viewModel.Tax, viewModel.Levy);

            var profitPrice = _calculatorEngine.CalculateProfitPrice(viewModel.Shares, viewModel.Price, viewModel.Commission, viewModel.Tax, viewModel.Levy, 1 + (viewModel.Percentage / 100));

            viewModel.ProfitPrice = profitPrice;
            viewModel.Earnings = _calculatorEngine.CalculateEarnings(viewModel.Shares, profitPrice, viewModel.Commission, cost, viewModel.Levy);

            return viewModel;
        }
    }
}
