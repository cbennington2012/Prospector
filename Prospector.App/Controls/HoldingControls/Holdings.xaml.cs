using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Controls;
using Prospector.Presentation.Contracts;

namespace Prospector.App.Controls.HoldingControls
{
    public partial class Holdings : UserControl
    {
        private readonly IHoldingViewModelBuilder _holdingViewModelBuilder;

        public Holdings(IHoldingViewModelBuilder holdingViewModelBuilder)
        {
            _holdingViewModelBuilder = holdingViewModelBuilder;

            InitializeComponent();
            Initialize();
        }

        public void Initialize()
        {
            var holdingViewModels = App.Holdings.Select(item => _holdingViewModelBuilder.BuildViewModel(item)).ToList();

            ResultsGrid.DataContext = holdingViewModels;
        }

        public void AddHoldingsButton_OnClick(Object sender, EventArgs e)
        {
            base.Content = App.Container.GetInstance<AddHoldings>();
        }

        public void DeleteHoldingsButton_OnClick(object sender, EventArgs e)
        {
            var button = sender as Button;
            var holding = App.Holdings.FirstOrDefault(x => x.Id == Guid.Parse(button.Tag.ToString()));

            App.Holdings.Remove(holding);

            base.Content = App.Container.GetInstance<Holdings>();
        }

        public void CalculatorButton_OnClick(Object sender, EventArgs e)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C:\\Windows\\System32\\calc.exe"
                }
            };

            process.Start();
        }
    }
}
