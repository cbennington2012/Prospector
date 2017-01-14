using AutoMapper;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Presentation.ViewModels;

namespace Prospector.Presentation.AutoMapping
{
    public class CalculatorTransactionViewModelMap : Profile, IAutoMap
    {
        public CalculatorTransactionViewModelMap()
        {
            CreateMap<CalculatorViewModel, TransactionViewModel>()
                .ForMember(m => m.Percentage, opt => opt.MapFrom(x => x.Profit));
        }
    }
}
