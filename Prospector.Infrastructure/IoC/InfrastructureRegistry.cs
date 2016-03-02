using Prospector.Domain.Contracts.AutoMapping;
using StructureMap;
using StructureMap.Graph;

namespace Prospector.Infrastructure.IoC
{
    public class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });

            For<IAutoMapper>().Singleton().Use<AutoMapping.AutoMapper>().OnCreation(m => m.InitializeMaps());
        }
    }
}
