using Prospector.Domain.Contracts.AutoMapping;
using StructureMap;
using StructureMap.Graph;

namespace Prospector.Presentation.IoC
{
    public class PresentationRegistry : Registry
    {
        public PresentationRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
                scan.AddAllTypesOf<IAutoMap>();
            });
        }
    }
}
