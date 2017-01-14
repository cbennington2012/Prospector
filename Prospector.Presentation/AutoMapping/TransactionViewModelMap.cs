using AutoMapper;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Entities;
using Prospector.Presentation.ViewModels;

namespace Prospector.Presentation.AutoMapping
{
    public class TransactionViewModelMap : Profile, IAutoMap
    {
        public TransactionViewModelMap()
        {
            CreateMap<TransactionViewModel, TransactionData>();

            CreateMap<TransactionData, TransactionViewModel>();
        }
    }
}
