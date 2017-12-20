using System;
using System.Collections.Generic;
using System.Linq;
using Prospector.Domain.Contracts.Engines;
using Prospector.Domain.Contracts.Factories;
using Prospector.Domain.Contracts.Repositories;
using Prospector.Domain.Entities;
using Prospector.Domain.Enumerations;

namespace Prospector.Domain.Factories
{
    public class TransactionFactory : ITransactionFactory
    {
        private readonly ICalculatorEngine _calculatorEngine;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionFactory(ICalculatorEngine calculatorEngine, ITransactionRepository transactionRepository)
        {
            _calculatorEngine = calculatorEngine;
            _transactionRepository = transactionRepository;
        }

        public Decimal GetTransactionPeriodValue(IList<TransactionData> data)
        {
            return (from item in data.Where(x => x.TransactionType == TransactionType.Sell)
                    let buyTransaction = (TransactionData)(data.FirstOrDefault(x => x.Id == item.SellTransactionId) ?? _transactionRepository.GetTransactionById(item.SellTransactionId))
                    let buyCost = _calculatorEngine.CalculateCost(buyTransaction.Shares, buyTransaction.Price, buyTransaction.Commission, buyTransaction.Tax, buyTransaction.Levy)
                    let sellCost = _calculatorEngine.CalculateCost(item.Shares, item.Price, item.Commission, item.Tax, item.Levy)
                    select sellCost - buyCost).Sum();
        }
    }
}
