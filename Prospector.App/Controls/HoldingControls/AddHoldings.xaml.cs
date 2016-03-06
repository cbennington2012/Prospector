﻿using System;
using System.Windows.Controls;
using Prospector.Domain.Entities;

namespace Prospector.App.Controls.HoldingControls
{
    public partial class AddHoldings : UserControl
    {
        public AddHoldings()
        {
            InitializeComponent();
        }

        public void SaveButton_OnClick(Object sender, EventArgs e)
        {
            App.Holdings.Add(new HoldingData
            {
                Code = CodeTextBox.Text,
                Date = String.IsNullOrEmpty(DateTextBox.Text) ? DateTime.Parse(DateTextBox.Text) : DateTime.Today,
                Shares = int.Parse(SharesTextBox.Text),
                Price = Decimal.Parse(PriceTextBox.Text),
                Commission = Decimal.Parse(CommissionTextBox.Text),
                Tax = Decimal.Parse(TaxTextBox.Text),
                Levy = Decimal.Parse(LevyTextBox.Text)
            });

            base.Content = App.Container.GetInstance<Holdings>();
        }
    }
}