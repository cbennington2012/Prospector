using System;
using Prospector.Domain.Contracts.Wrappers;
using StructureMap;

namespace Prospector.Infrastructure.Wrappers
{
    public class StructureMapWrapper : IStructureMapWrapper
    {
        private readonly IContainer _container;

        public StructureMapWrapper(IContainer container)
        {
            _container = container;
        }

        public object GetInstance(Type type)
        {
            return _container.GetInstance(type);
        }
    }
}