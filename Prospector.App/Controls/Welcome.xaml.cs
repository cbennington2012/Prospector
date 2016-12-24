using System;
using System.Windows.Controls;
using Prospector.App.Controls.HoldingControls;

namespace Prospector.App.Controls
{
    public partial class Welcome : UserControl
    {
        public Welcome()
        {
            InitializeComponent();
        }
        
        public void CalculatorButton_OnClick(Object sender, EventArgs e)
        {
            Container.Content = App.Container.GetInstance<Calculator>();
        }

        public void HoldingsButton_OnClick(Object sender, EventArgs e)
        {
            Container.Content = App.Container.GetInstance<Holdings>();
        }

        public void TransactionsButton_OnClick(Object sender, EventArgs e)
        {
            Container.Content = App.Container.GetInstance<Transactions>();
        }

        public void DashboardButton_OnClick(Object sender, EventArgs e)
        {
            Container.Content = App.Container.GetInstance<Dashboard>();
        }
    }
}
