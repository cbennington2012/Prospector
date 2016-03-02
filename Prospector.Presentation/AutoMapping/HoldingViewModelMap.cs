using AutoMapper;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Entities;
using Prospector.Presentation.ViewModels;

namespace Prospector.Presentation.AutoMapping
{
    public class HoldingViewModelMap  : IAutoMap
    {
        public void CreateMap()
        {
            Mapper.CreateMap<HoldingData, HoldingViewModel>()
                .ForMember(m => m.Cost, opt => opt.Ignore())
                .ForMember(m => m.BreakEvenPrice, opt => opt.Ignore())
                .ForMember(m => m.ProfitPrice, opt => opt.Ignore())
                .ForMember(m => m.Earnings, opt =>  opt.Ignore());
        }
    }
}
