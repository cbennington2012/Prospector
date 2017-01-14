using System;

namespace Prospector.Domain.Contracts.Wrappers
{
    public interface IStructureMapWrapper
    {
        Object GetInstance(Type type);
    }
}
