using System;
using System.Collections.Generic;
using System.Linq;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Contracts.Engines;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Contracts.Retrievers;
using Prospector.Domain.Contracts.Wrappers;
using Prospector.Domain.Entities;
using Prospector.Presentation.Contracts;
using Prospector.Presentation.ViewModels;

namespace Prospector.Presentation.ViewModelBuilders
{
    public class DashboardViewModelBuilder : IDashboardViewModelBuilder
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly ITransactionFileWrapper _transactionFileWrapper;
        private readonly IAutoMapper _autoMapper;
        private readonly ICalculatorEngine _calculatorEngine;
        private readonly IStockPriceRetriever _stockPriceRetriever;

        public DashboardViewModelBuilder(ITransactionRepository transactionRepository, ITransactionFileWrapper transactionFileWrapper, 
            IAutoMapper autoMapper, ICalculatorEngine calculatorEngine, IStockPriceRetriever stockPriceRetriever)
        {
            _transactionRepository = transactionRepository;
            _transactionFileWrapper = transactionFileWrapper;
            _autoMapper = autoMapper;
            _calculatorEngine = calculatorEngine;
            _stockPriceRetriever = stockPriceRetriever;
        }

        public IList<DashboardViewModel> BuildViewModels()
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

            var viewModels = new List<DashboardViewModel>();
            var currentPrices = _stockPriceRetriever.GetPrices(data);

            foreach (var item in data)
            {
                var viewModel = _autoMapper.Map<TransactionData, DashboardViewModel>(item);

                var profitPercentage = _calculatorEngine.CalculatePercentage(item.Percentage);
                var cost = _calculatorEngine.CalculateCost(item.Shares, item.Price, item.Commission, item.Tax, item.Levy);
                var currentPrice = currentPrices.FirstOrDefault(x => x.Code.Replace(".L", String.Empty) == item.Code);

                viewModel.Name = currentPrice.Name;

                viewModel.BreakEvenPrice = _calculatorEngine.CalculateBreakEvenPrice(item.Shares, item.Price,
                    item.Commission, item.Tax, item.Levy);
                viewModel.ProfitPrice = _calculatorEngine.CalculateProfitPrice(item.Shares, item.Price, item.Commission,
                    item.Tax, item.Levy, profitPercentage);

                viewModel.CurrentPrice = currentPrice.Ask;

                viewModel.PercentageDifference = (viewModel.CurrentPrice - viewModel.BreakEvenPrice)/
                                                 viewModel.BreakEvenPrice;

                viewModel.Earnings = _calculatorEngine.CalculateEarnings(item.Shares, currentPrice.Ask, item.Commission,
                    cost, item.Levy);

                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
