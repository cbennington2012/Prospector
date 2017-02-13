using System;
using System.Collections.Generic;
using System.Linq;
using Prospector.Domain.Contracts.Engines;
using Prospector.Domain.Contracts.Factories;
using Prospector.Domain.Entities;
using Prospector.Domain.Enumerations;

namespace Prospector.Domain.Factories
{
    public class TransactionFactory : ITransactionFactory
    {
        private readonly ICalculatorEngine _calculatorEngine;

        public TransactionFactory(ICalculatorEngine calculatorEngine)
        {
            _calculatorEngine = calculatorEngine;
        }

        public Decimal GetTransactionPeriodValue(IList<TransactionData> data)
        {
            return (from item in data where item.TransactionType == TransactionType.Sell
                    let buyTransaction = data.FirstOrDefault(x => x.Id == item.SellTransactionId)
                        where buyTransaction != null
                    let buyCost = _calculatorEngine.CalculateCost(buyTransaction.Shares, buyTransaction.Price, buyTransaction.Commission, buyTransaction.Tax, buyTransaction.Levy)
                    let sellCost = _calculatorEngine.CalculateCost(item.Shares, item.Price, item.Commission, item.Tax, item.Levy)
                    select sellCost - buyCost).Sum();
        }
    }
}
