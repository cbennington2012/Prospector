using System;
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
    }
}
