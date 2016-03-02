using System;

namespace Prospector.Domain.Contracts.Providers
{
    public interface IJsonProvider
    {
        String Serialize(Object value);
        T Deserialize<T>(String json);
    }
}
