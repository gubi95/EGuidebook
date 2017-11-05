using System.Web;
using System.Web.Optimization;

namespace EGuidebook
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Content/assets/js/jQuery/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Content/assets/js/jQuery/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include("~/Content/assets/js/modernizr-*"));
            bundles.Add(new ScriptBundle("~/bundles/moment").Include("~/Content/assets/js/moment-with-locales.js"));
            bundles.Add(new ScriptBundle("~/bundles/datetime-picker").Include("~/Content/assets/bootstrap-datetimepicker/bootstrap-datetimepicker.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/iCheck").Include("~/Content/assets/js/icheck-1.x/icheck.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/bs-select").Include("~/Content/assets/js/bootstrap-select-1.12.4/dist/js/bootstrap-select.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jQueryDataTables").Include("~/Content/assets/js/jQuery-DataTable-1.10.15/DataTable.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/touchspin").Include("~/Content/assets/js/bootstrap-touchspin-master/dist/jquery.bootstrap-touchspin.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Content/assets/bootstrap/js/bootstrap.js", "~/Scripts/respond.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include("~/Content/assets/js/jQuery/jquery-ui-1.12.1/jquery-ui.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include("~/Content/assets/js/scripts.js"));
            bundles.Add(new StyleBundle("~/Content/css")
                .Include(
                "~/Content/assets/bootstrap/css/bootstrap.css",
                "~/Content/assets/css/site.css",
                "~/Content/assets/font-awesome-4.7.0/css/font-awesome.min.css",
                "~/Content/assets/js/icheck-1.x/skins/flat/blue.css",
                "~/Content/assets/js/jQuery-DataTable-1.10.15/styles.css",
                "~/Content/assets/js/bootstrap-select-1.12.4/dist/css/bootstrap-select.min.css",
                "~/Content/assets/bootstrap-datetimepicker/bootstrap-datetimepicker.min.css",
                "~/Content/assets/js/bootstrap-touchspin-master/dist/jquery.bootstrap-touchspin.min.css",
                "~/Content/assets/js/jQuery/jquery-ui-1.12.1/jquery-ui.min.css"
            ));
        }
    }
}
