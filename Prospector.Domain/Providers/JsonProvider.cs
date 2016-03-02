using System;
using Newtonsoft.Json;
using Prospector.Domain.Contracts.Providers;

namespace Prospector.Domain.Providers
{
    public class JsonProvider : IJsonProvider
    {
        public String Serialize(Object value)
        {
            return JsonConvert.SerializeObject(value);
        }

        public T Deserialize<T>(String json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
