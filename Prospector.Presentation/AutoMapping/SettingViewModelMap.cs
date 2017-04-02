using AutoMapper;
using Prospector.Domain.Contracts.AutoMapping;
using Prospector.Domain.Entities;
using Prospector.Presentation.ViewModels;

namespace Prospector.Presentation.AutoMapping
{
    public class SettingViewModelMap : Profile, IAutoMap
    {
        public SettingViewModelMap()
        {
            CreateMap<SettingData, SettingViewModel>()
                .ForMember(m => m.Key, opt => opt.MapFrom(x => x.SettingsKey))
                .ForMember(m => m.Value, opt => opt.MapFrom(x => x.SettingsValue));
        }
    }
}
