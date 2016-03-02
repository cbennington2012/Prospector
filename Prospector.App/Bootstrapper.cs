using Prospector.Domain.IoC;
using Prospector.Infrastructure.IoC;
using Prospector.Presentation.IoC;
using StructureMap;
using StructureMap.Graph;

namespace Prospector.App
{
    public static class Bootstrapper
    {
        public static IContainer Initialize()
        {
            var container = new Container(cfg =>
            {
                cfg.AddRegistry<DomainRegistry>();
                cfg.AddRegistry<PresentationRegistry>();
                cfg.AddRegistry<InfrastructureRegistry>();

                cfg.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            });
            
            return container;
        }
    }
}
