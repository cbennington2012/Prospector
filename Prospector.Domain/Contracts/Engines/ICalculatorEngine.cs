using System;

namespace Prospector.Domain.Contracts.Engines
{
    public interface ICalculatorEngine
    {
        Decimal CalculateCost(Decimal shares, Decimal price, Decimal commission, Decimal tax);
        Decimal CalculateBreakEvenPrice(Decimal shares, Decimal price, Decimal commission, Decimal tax);
        Decimal CalculateProfitPrice(Decimal shares, Decimal price, Decimal commission, Decimal tax, Decimal profitPercentage);
        Decimal CalculateEarnings(Decimal shares, Decimal profitPrice, Decimal commission, Decimal cost);
    }
}
