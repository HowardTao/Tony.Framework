using System.Web.Optimization;

namespace Tony.Application.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //jqgrid表格组件
            bundles.Add(
                new StyleBundle("~/Content/scripts/plugins/jqgrid/css").Include(
                    "~/Content/scripts/plugins/jqgrid/jqgrid.css"));
            bundles.Add(new ScriptBundle("~/Content/scripts/plugins/jqgrid/js").Include(
                "~/Content/scripts/plugins/jqgrid/grid.locale-cn.js",
                "~/Content/scripts/plugins/jqgrid/jqgrid.min.js"));
            //树形组件
            bundles.Add(new StyleBundle("~/Content/scripts/plugins/tree/css").Include(
                "~/Content/scripts/plugins/tree/tree.css"));
            bundles.Add(new ScriptBundle("~/Content/scripts/plugins/tree/js").Include(
                "~/Content/scripts/plugins/tree/tree.js"));
            //表单验证
            bundles.Add(new ScriptBundle("~/Content/scripts/plugins/validator/js").Include(
                "~/Content/scripts/plugins/validator/validator.js"));
            //日期控件
            bundles.Add(new StyleBundle("~/Content/scripts/plugins/datetime/css").Include(
                "~/Content/scripts/plugins/datetime/pikaday.css"));
            bundles.Add(new ScriptBundle("~/Content/scripts/plugins/datepicker/js").Include(
                "~/Content/scripts/plugins/datetime/pikaday.js"));
            //导向组件
            bundles.Add(new StyleBundle("~/Content/scripts/plugins/wizard/css").Include(
                "~/Content/scripts/plugins/wizard/wizard.css"));
            bundles.Add(new ScriptBundle("~/Content/scripts/plugins/wizard/js").Include(
                "~/Content/scripts/plugins/wizard/wizard.js"));
            //lr
            bundles.Add(new StyleBundle("~/Content/styles/learun-ui.css").Include(
                "~/Content/styles/learun-ui.css"));
            bundles.Add(new StyleBundle("~/Content/styles/custmerform").Include(
                "~/Content/styles/custmerform.css"));
            bundles.Add(new ScriptBundle("~/Content/scripts/utils/js").Include(
                "~/Content/scripts/utils/learun-ui.js",
                "~/Content/scripts/utils/learun-form.js"));
            //lr order

            //工作流
        }
    }
}