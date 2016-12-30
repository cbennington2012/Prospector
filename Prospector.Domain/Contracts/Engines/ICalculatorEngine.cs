using System;

namespace Prospector.Domain.Contracts.Engines
{
    public interface ICalculatorEngine
    {
        Decimal CalculatePercentage(Decimal profitPercentage);
        int CalculateShares(Decimal investment, Decimal commission, Decimal tax, Decimal levy, Decimal price);
        Decimal CalculateCost(Decimal shares, Decimal price, Decimal commission, Decimal tax, Decimal levy);
        Decimal CalculateBreakEvenPrice(Decimal shares, Decimal price, Decimal commission, Decimal tax, Decimal levy);
        Decimal CalculateProfitPrice(Decimal shares, Decimal price, Decimal commission, Decimal tax, Decimal levy, Decimal profitPercentage);
        Decimal CalculateEarnings(Decimal shares, Decimal profitPrice, Decimal commission, Decimal cost, Decimal levy);
    }
}
