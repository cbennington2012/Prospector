using System;
using System.Windows;
using System.Windows.Controls;
using Prospector.Domain.Contracts.Engines;

namespace Prospector.App.Controls
{
    public partial class CalculatorControl : UserControl
    {
        private readonly ICalculatorEngine _calculatorEngine;

        public CalculatorControl(ICalculatorEngine calculatorEngine)
        {
            _calculatorEngine = calculatorEngine;
            InitializeComponent();
        }

        private void CalculateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var investment = Decimal.Parse(InvestmentTextBox.Text);
            var commission = Decimal.Parse(CommissionTextBox.Text);
            var tax = Decimal.Parse(TaxTextBox.Text);
            var levy = Decimal.Parse(LevyTextBox.Text);
            var price = Decimal.Parse(PriceTextBox.Text);
            var percentage = 1 + (Decimal.Parse(ProfitTextBox.Text)/100M);

            var shares = _calculatorEngine.CalculateShares(investment, commission, tax, levy, price);
            var cost = _calculatorEngine.CalculateCost(shares, price, commission, tax, levy);
            var profitPrice = _calculatorEngine.CalculateProfitPrice(shares, price, commission, tax, levy, percentage);

            SharesLabel.Content = shares;
            CostLabel.Content = cost;
            BreakEvenPriceLabel.Content = _calculatorEngine.CalculateBreakEvenPrice(shares, price, commission, tax, levy);
            ProfitPriceLabel.Content = profitPrice;
            EarningsLabel.Content = _calculatorEngine.CalculateEarnings(shares, profitPrice, commission, cost, levy);
        }
    }
}
