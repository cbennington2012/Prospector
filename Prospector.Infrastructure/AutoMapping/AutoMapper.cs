using System;
using AutoMapper;
using Prospector.Domain.Contracts.AutoMapping;

namespace Prospector.Infrastructure.AutoMapping
{
    public class AutoMapper : IAutoMapper
    {
        private readonly IAutoMap[] _autoMaps;

        public AutoMapper(IAutoMap[] autoMaps)
        {
            _autoMaps = autoMaps;
        }

        public TD Map<TS, TD>(TS source)
        {
            return Mapper.Map<TS, TD>(source);
        }

        public TD Map<TS, TD>(TS source, TD destination)
        {
            return Mapper.Map(source, destination);
        }

        public void InitializeMaps()
        {
            Array.ForEach(_autoMaps, map => map.CreateMap());
            Mapper.AssertConfigurationIsValid();
        }
    }
}
