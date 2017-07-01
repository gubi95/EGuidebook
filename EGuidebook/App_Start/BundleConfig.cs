using System.Web;
using System.Web.Optimization;

namespace EGuidebook
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Content/assets/jQuery/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Content/assets/jQuery/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Content/assets/js/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Content/assets/bootstrap/js/bootstrap.js", "~/Scripts/respond.js"));
            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/assets/bootstrap/css/bootstrap.css", "~/Content/assets/css/site.css"));
        }
    }
}
