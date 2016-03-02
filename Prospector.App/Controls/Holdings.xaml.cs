using System.Linq;
using System.Windows.Controls;
using Prospector.Domain.Contracts.Providers;
using Prospector.Presentation.Contracts;

namespace Prospector.App.Controls
{
    public partial class Holdings : UserControl
    {
        private readonly IHoldingViewModelBuilder _holdingViewModelBuilder;

        public Holdings(IHoldingViewModelBuilder holdingViewModelBuilder, IHoldingDataProvider holdingDataProvider)
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
    }
}
