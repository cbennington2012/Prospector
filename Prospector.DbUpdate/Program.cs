using System;
using System.Configuration;
using System.Reflection;
using DbUp;

namespace Prospector.DbUpdater
{
    public class Program
    {
        public static int Main(String[] args)
        {
            Console.WriteLine("Updating database");

            var upgrader = DeployChanges.To.
                MySqlDatabase(ConfigurationManager.ConnectionStrings["Prospector"].ConnectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.WriteLine($"Update to Update Database: {result.Error}");

                return -1;
            }

            Console.WriteLine("");

            return 0;
        }
    }
}
