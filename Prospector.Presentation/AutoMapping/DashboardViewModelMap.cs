using AutoMapper;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Entities;
using Prospector.Presentation.ViewModels;

namespace Prospector.Presentation.AutoMapping
{
    public class DashboardViewModelMap : Profile, IAutoMap
    {
        public DashboardViewModelMap()
        {
            CreateMap<TransactionData, DashboardViewModel>()
                .ForMember(m => m.Name, opt => opt.Ignore())
                .ForMember(m => m.BreakEvenPrice, opt => opt.Ignore())
                .ForMember(m => m.ProfitPrice, opt => opt.Ignore())
                .ForMember(m => m.CurrentPrice, opt => opt.Ignore())
                .ForMember(m => m.PercentageDifference, opt => opt.Ignore())
                .ForMember(m => m.Earnings, opt => opt.Ignore());
        }
    }
}
