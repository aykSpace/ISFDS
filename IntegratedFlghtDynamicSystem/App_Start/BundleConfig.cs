using System.Web.Optimization;

namespace IntegratedFlghtDynamicSystem
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.custom.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap*"));
            bundles.Add(new ScriptBundle("~/bundles/modern-business").Include("~/Scripts/common.js"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство построения на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap*").Include("~/Content/style.css"));
            bundles.Add(new StyleBundle("~/Content/spcr-oper").Include("~/Content/spcr-oper.css"));

            bundles.Add(new ScriptBundle("~/bundles/grid").Include(
                "~/Scripts/jquery.dd.js",
                "~/Scripts/jquery.ui.datepicker-en.js",
                "~/Scripts/jQuery.tmpl.min.js",
                "~/Scripts/grid.locale-en.js"));
            bundles.Add(new ScriptBundle("~/bundles/Layout").Include("~/Scripts/Layout.js"));
            bundles.Add(new StyleBundle("~/Content/grid").Include(
                "~/Content/dd.css",
                "~/Content/ui.jqgrid.css"));
            bundles.Add(new ScriptBundle("~/Scripts/nuGrid").Include("~/Scripts/NuGrid.js"));
            bundles.Add(new ScriptBundle("~/Scripts/gridStart").Include("~/Scripts/jquery.jqGrid.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jquerySession").Include("~/Scripts/jquery.session.js"));
            bundles.Add(new ScriptBundle("~/Scripts/predictScript").Include("~/Scripts/predictScript.js"));
            bundles.Add(new ScriptBundle("~/Scripts/jquery.unobtrusive-ajax").Include("~/Scripts/jquery.unobtrusive-ajax.min.js"));
            bundles.Add(new ScriptBundle("~/Scripts/morris-charts").Include(
                "~/Scripts/morris.js",
                "~/Scripts/chartsScript.js"));
            bundles.Add(new ScriptBundle("~/Scripts/chartJs").Include(
                "~/Scripts/globalize.js",
                "~/Scripts/dx.chartjs.js",
                "~/Scripts/chartsScript.js"));
            bundles.Add(new ScriptBundle("~/Scripts/spCrInitialData").Include("~/Scripts/spCrInitialData.js"));

            bundles.Add(new ScriptBundle("~/Scripts/addData").Include("~/Scripts/addData.js"));
            bundles.Add(new ScriptBundle("~/Scripts/ajaxPaging").Include("~/Scripts/ajaxPaging.js"));

        }
    }
}