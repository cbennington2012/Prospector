using StructureMap;

namespace Prospector.Domain.IoC
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            Scan(scan => {
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                }
            });
        }
    }
}
