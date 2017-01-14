using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using Prospector.Domain.Contracts.Providers;

namespace Prospector.Infrastructure.Providers
{
    public class MySqlProvider : IMySqlProvider
    {
        public DataTable GetData(string connectionString, string storedProcedure, IDictionary<string, object> parameters)
        {
            using (
                var connection =
                    new MySqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
            {
                connection.Open();

                var command = new MySqlCommand
                {
                    Connection = connection,
                    CommandText = storedProcedure,
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 240
                };

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                        command.Parameters.Add(new MySqlParameter(parameter.Key, parameter.Value));
                }

                var datatSet = new DataSet();

                using (var dataAdapter = new MySqlDataAdapter(command))
                {
                    dataAdapter.Fill(datatSet);
                    return datatSet.Tables[0];
                }
            }
        }

        public void ExecuteProcedure(string connectionString, string storedProcedure, IDictionary<string, object> parameters)
        {
            using (
                var sqlConnection =
                    new MySqlConnection(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString))
            {
                sqlConnection.Open();

                using (var sqlCommand = new MySqlCommand
                {
                    CommandText = storedProcedure,
                    Connection = sqlConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 240
                })
                {
                    foreach (var item in parameters)
                    {
                        sqlCommand.Parameters.Add(new MySqlParameter(item.Key, item.Value));
                    }

                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}