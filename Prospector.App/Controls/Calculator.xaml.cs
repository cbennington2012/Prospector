using System;
using System.Windows;
using System.Windows.Controls;
using Prospector.Domain.Contracts.Engines;

namespace Prospector.App.Controls
{
    public partial class Calculator : UserControl
    {
        private readonly ICalculatorEngine _calculatorEngine;

        public Calculator(ICalculatorEngine calculatorEngine)
        {
            _calculatorEngine = calculatorEngine;

            InitializeComponent();
        }

        private void CalculateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var investment = Decimal.Parse(InvestmentTextbox.Text);
            var price = Decimal.Parse(PriceTextBox.Text);
            var commission = Decimal.Parse(CommissionTextBox.Text);
            var tax = Decimal.Parse(TaxTextBox.Text);
            var levy = Decimal.Parse(LevyTextBox.Text);
            var profitPercentage = 1 + Decimal.Parse(ProfitPercentageTextBox.Text) / 100;

            var shares = Math.Floor(((investment - commission - tax - levy) * 100) / price);
            var cost = _calculatorEngine.CalculateCost(shares, price, commission, tax, levy);
            var breakEvenPrice = _calculatorEngine.CalculateBreakEvenPrice(shares, price, commission, tax, levy);
            var profitPrice = _calculatorEngine.CalculateProfitPrice(shares, price, commission, tax, levy, profitPercentage);
            var earnings = _calculatorEngine.CalculateEarnings(shares, profitPrice, commission, cost, levy);

            SharesLabel.Content = shares;
            CostLabel.Content = cost;
            BreakEvenLabel.Content = breakEvenPrice;
            ProfitPriceLabel.Content = $"{profitPrice} ({profitPrice - price})";
            EarningsLabel.Content = earnings;
        }
    }
}
