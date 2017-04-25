using System;
using System.Collections.Generic;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Engines;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Contracts.Wrappers;
using Prospector.Domain.Entities;
using Prospector.Presentation.Contracts;
using Prospector.Presentation.ViewModels;

namespace Prospector.Presentation.ViewModelBuilders
{
    public class HoldingViewModelBuilder : IHoldingViewModelBuilder
    {
        private readonly IAutoMapper _autoMapper;
        private readonly ICalculatorEngine _calculatorEngine;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionFileWrapper _transactionFileWrapper;

        public HoldingViewModelBuilder(IAutoMapper autoMapper, ICalculatorEngine calculatorEngine, ITransactionRepository transactionRepository, ITransactionFileWrapper transactionFileWrapper)
        {
            _autoMapper = autoMapper;
            _calculatorEngine = calculatorEngine;
            _transactionRepository = transactionRepository;
            _transactionFileWrapper = transactionFileWrapper;
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

        public IList<HoldingViewModel> BuildViewModels()
        {
            IList<TransactionData> data;

            try
            {
                data = _transactionRepository.GetCurrentHoldings();
            }
            catch
            {
                // Get from latest Json file
                data = _transactionFileWrapper.GetCurrentHoldings();
            }

            var viewModels = new List<HoldingViewModel>();

            foreach (var item in data)
            {
                var viewModel = _autoMapper.Map<TransactionData, HoldingViewModel>(item);
                var profitPercentage = _calculatorEngine.CalculatePercentage(item.Percentage);
                var cost = _calculatorEngine.CalculateCost(item.Shares, item.Price, item.Commission, item.Tax, item.Levy);
                var profitPrice = _calculatorEngine.CalculateProfitPrice(item.Shares, item.Price, item.Commission, item.Tax, item.Levy, profitPercentage);

                viewModel.Cost = cost;
                viewModel.BreakEvenPrice = _calculatorEngine.CalculateBreakEvenPrice(item.Shares, item.Price, item.Commission, item.Tax, item.Levy);
                viewModel.ProfitPrice = profitPrice;
                viewModel.Earnings = _calculatorEngine.CalculateEarnings(item.Shares, profitPrice, item.Commission, cost, item.Levy);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
