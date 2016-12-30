using System.Web;
using System.Web.Mvc;
using Prospector.Web;
using Prospector.Web.DependencyResolution;

[assembly: PreApplicationStartMethod(typeof(StructuremapMvc), "Start")]

namespace Prospector.Web
{
    public class StructuremapMvc
    {
        public static void Start()
        {
            var container = IoC.Initialize();
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}