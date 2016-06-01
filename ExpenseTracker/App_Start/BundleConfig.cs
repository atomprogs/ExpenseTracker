using System.Web;
using System.Web.Optimization;

namespace ExpenseTracker
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
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
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                    "~/Scripts/angular.js",
                    "~/Scripts/ui-bootstrap-tpls-1.3.2.js", "~/Scripts/angular-animate.js", "~/Scripts/angular-route.js",
                    "~/Scripts/angular-touch.js", "~/Scripts/loading-bar.js", "~/Scripts/angular-messages.js",
                    "~/Scripts/Expense/app/main.js", "~/Scripts/Expense/app/login.js", "~/Scripts/Expense/app/dashboard.js", "~/Scripts/Expense/service/modalService.js"));

            bundles.Add(new StyleBundle("~/Content/style").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.css", "~/Content/AngularAnimation.css", "~/Content/loading-bar.css").Include("~/Content/css/font-awesome.css", new CssRewriteUrlTransform()));
            // BundleTable.EnableOptimizations = true;
        }
    }
}