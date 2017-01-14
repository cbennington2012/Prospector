using System;
using System.Collections.Generic;
using System.Data;

namespace Prospector.Domain.Contracts.Providers
{
    public interface IMySqlProvider
    {
        DataTable GetData(String connectionString, String storedProcedure, IDictionary<String, Object> parameters);
        void ExecuteProcedure(String connectionString, String storedProcedure, IDictionary<String, Object> parameters);
    }
}
