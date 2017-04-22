using System;
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
            CreateMap<TransactionViewModel, TransactionData>()
                .ForMember(m => m.Date, opt => opt.MapFrom(x => DateTime.Parse($"{x.Date.ToString("yyyy-MM-dd")} {x.Time.ToString("HH:mm:ss")}")));

            CreateMap<TransactionData, TransactionViewModel>()
                .ForMember(m => m.Date, opt => opt.MapFrom(x => x.Date))
                .ForMember(m => m.Time, opt => opt.MapFrom(x => x.Date));
        }
    }
}
