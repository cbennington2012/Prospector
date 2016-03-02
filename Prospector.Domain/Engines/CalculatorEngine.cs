using System;
using Prospector.Domain.Contracts.Engines;

namespace Prospector.Domain.Engines
{
    public class CalculatorEngine : ICalculatorEngine
    {
        public Decimal CalculateCost(Decimal shares, Decimal price, Decimal commission, Decimal tax)
        {
            return Math.Round(((shares * price) / 100) + commission + tax, 2);
        }

        public Decimal CalculateBreakEvenPrice(Decimal shares, Decimal price, Decimal commission, Decimal tax)
        {
            return Math.Round(((((shares * price) / 100) + commission + tax) / shares) * 100, 2);
        }

        public Decimal CalculateProfitPrice(Decimal shares, Decimal price, Decimal commission, Decimal tax, Decimal profitPercentage)
        {
            return Math.Round((((((shares * price) / 100) * profitPercentage) + commission + tax) / shares) * 100, 2);
        }

        public Decimal CalculateEarnings(Decimal shares, Decimal profitPrice, Decimal commission, Decimal cost)
        {
            return Math.Round(((shares * profitPrice) / 100) - commission, 2) - cost;
        }
    }
}
