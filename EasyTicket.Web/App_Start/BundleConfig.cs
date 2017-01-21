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
                            "~/Content/bootstrap-datepicker.css",
                            "~/Content/site.css",
                            "~/Content/css/select2.css"));

            bundles.Add(new ScriptBundle("~/bundles/3rdParty").Include(
                            "~/Scripts/select2.js",
                            "~/Scripts/i18n/ru.js",
                            "~/Scripts/knockout-{version}.js",
                            "~/Scripts/knockout-select2.js",
                            "~/Scripts/moment.js",
                            "~/Scripts/bootstrap-datepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/3rdPartyExtensions").Include(
                            "~/Scripts/extensions/knockout.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/app/viewModelSettings.js",            
                "~/Scripts/app/viewModel.js",            
                "~/Scripts/app/index.js"));


        }
    }
}
