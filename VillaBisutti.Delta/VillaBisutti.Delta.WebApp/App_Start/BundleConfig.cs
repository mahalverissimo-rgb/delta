using System.Web;
using System.Web.Optimization;

namespace VillaBisutti.Delta.WebApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
			bundles.UseCdn = true;

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-2.0.3.min.js",
						"~/Scripts/ace-extra.min.js",
                        "~/Scripts/ace-elements.min.js",
						"~/Scripts/ace.min.js",
						"~/Scripts/jquery.gritter.min.js",
						"~/Scripts/jquery-ui.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/jquery-ui.min.css",
					  "~/Content/bootstrap.min.css",
                      "~/Content/bootstrap-glyphs.min.css",
                      "~/Content/ace.min.css",
					  "~/Content/jquery.gritter.css",
                      "~/Content/estilos.css"));

			bundles.Add(new StyleBundle("~/Content/default").Include(
					  "~/Content/bootstrap.min.css"));

			bundles.Add(new ScriptBundle("~/bundles/own").Include("~/Scripts/Functions.js"));

			bundles.IgnoreList.Clear();
        }
    }
}
