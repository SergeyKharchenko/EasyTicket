using System.Web.Optimization;

namespace EasyTicket.Web {
    public class BundleConfig {
        public static void RegisterBundles(BundleCollection bundles) {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                            "~/Scripts/bootstrap.js",
                            "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                            "~/Content/bootstrap.css",
                            "~/Content/site.css",
                            "~/Content/css/select2.css"));

            bundles.Add(new ScriptBundle("~/bundles/3rdParty").Include(
                            "~/Scripts/select2.js",
                            "~/Scripts/i18n/ru.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                            "~/Scripts/app/index.js"));


        }
    }
}
