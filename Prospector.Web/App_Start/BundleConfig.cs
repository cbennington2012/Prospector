using System.Web.Optimization;

namespace Prospector.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/prospectorstyles").Include(
                "~/Content/css/prospectorstyles.css",
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-ui-1.12.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-plugins").Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"));

            BundleTable.EnableOptimizations = false;
        }
    }
}