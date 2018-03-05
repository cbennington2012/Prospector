using System.Collections;
using System.Collections.Generic;
using Prospector.Presentation.ViewModels;

namespace Prospector.Presentation.Contracts
{
    public interface IDashboardViewModelBuilder
    {
        IList<DashboardViewModel> BuildViewModels();
    }
}
