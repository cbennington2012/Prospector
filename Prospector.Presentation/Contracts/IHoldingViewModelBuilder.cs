using Prospector.Domain.Entities;
using Prospector.Presentation.ViewModels;

namespace Prospector.Presentation.Contracts
{
    public interface IHoldingViewModelBuilder
    {
        HoldingViewModel BuildViewModel(HoldingData data);
    }
}
