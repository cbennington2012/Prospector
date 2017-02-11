using AutoMapper;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Entities;
using Prospector.Presentation.ViewModels;

namespace Prospector.Presentation.AutoMapping
{
    public class HoldingViewModelMap  : Profile, IAutoMap
    {
        public HoldingViewModelMap()
        {
            CreateMap<TransactionData, HoldingViewModel>()
                .ForMember(m => m.Cost, opt => opt.Ignore())
                .ForMember(m => m.BreakEvenPrice, opt => opt.Ignore())
                .ForMember(m => m.ProfitPrice, opt => opt.Ignore())
                .ForMember(m => m.Earnings, opt =>  opt.Ignore());
        }
    }
}
