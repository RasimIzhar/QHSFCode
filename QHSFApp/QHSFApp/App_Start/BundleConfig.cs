using System.Web;
using System.Web.Optimization;

namespace QHSFApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.3.1.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/jquery.form.min.js"));
            

            // default CSS
            //bundles.Add(new StyleBundle("~/Content/css").Include(
            //          "~/Content/bootstrap.css",
            //          "~/Content/site.css"));


            // custom CSS
            bundles.Add(new StyleBundle("~/Content/_theme_assets/css").Include(
                      "~/Content/_theme_assets/css/bootstrap.min.css",
                      "~/Content/_theme_assets/css/font-awesome.min.css",
                      "~/Content/_theme_assets/css/adminpro-custon-icon.css",
                      "~/Content/_theme_assets/css/meanmenu.min.css",
                      "~/Content/_theme_assets/css/jquery.mCustomScrollbar.min.css",
                      "~/Content/_theme_assets/css/animate.css",
                      "~/Content/_theme_assets/css/modals.css",
                      "~/Content/_theme_assets/css/normalize.css",
                      "~/Content/_theme_assets/css/form.css",
                      "~/Content/_theme_assets/css/tabs.css",
                      "~/Content/_theme_assets/css/style.css",
                      "~/Content/_theme_assets/css/responsive.css"));


            
        }
    }
}
