using System.Data;

namespace Prospector.Domain.Contracts.Parsers
{
    public interface IDataObjectParser
    {
        T GetObject<T>(DataRow item);
    }
}
