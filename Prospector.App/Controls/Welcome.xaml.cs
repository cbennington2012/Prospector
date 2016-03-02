using System;
using System.Windows.Controls;

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
    }
}
