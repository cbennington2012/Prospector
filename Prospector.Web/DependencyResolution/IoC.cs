using Prospector.Domain.IoC;
using Prospector.Infrastructure.IoC;
using Prospector.Presentation.IoC;
using StructureMap;

namespace Prospector.Web.DependencyResolution
{
    public static class IoC
    {
        public static IContainer Initialize()
        {
            var container = new Container(cfg =>
            {
                cfg.AddRegistry<DomainRegistry>();
                cfg.AddRegistry<InfrastructureRegistry>();
                cfg.AddRegistry<PresentationRegistry>();

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