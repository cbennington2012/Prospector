using System.Windows.Controls;
using Prospector.Presentation.Contracts;

namespace Prospector.App.Controls
{
    public partial class HoldingsControl : UserControl
    {
        private readonly IHoldingViewModelBuilder _holdingViewModelBuilder;

        public HoldingsControl(IHoldingViewModelBuilder holdingViewModelBuilder)
        {
            _holdingViewModelBuilder = holdingViewModelBuilder;

            InitializeComponent();

            PopulateTable();
        }

        private void PopulateTable()
        {
            var holdingViewModels = _holdingViewModelBuilder.BuildViewModels();

            HoldingsDataGrid.ItemsSource = holdingViewModels;
        }
    }
}
