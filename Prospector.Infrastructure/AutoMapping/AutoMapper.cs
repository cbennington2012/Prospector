using System;
using AutoMapper;
using Prospector.Domain.Contracts.AutoMapping;

namespace Prospector.Infrastructure.AutoMapping
{
    public class AutoMapper : IAutoMapper
    {
        private readonly IAutoMap[] _autoMaps;

        private IMapper _mapper;

        public AutoMapper(IAutoMap[] autoMaps)
        {
            _autoMaps = autoMaps;
        }

        public TD Map<TS, TD>(TS source)
        {
            return _mapper.Map<TS, TD>(source);
        }

        public TD Map<TS, TD>(TS source, TD destination)
        {
            return _mapper.Map(source, destination);
        }

        public void InitializeMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var item in _autoMaps)
                {
                    cfg.AddProfile((Profile)Activator.CreateInstance(item.GetType()));
                }
            });

            _mapper = config.CreateMapper();
        }
    }
}