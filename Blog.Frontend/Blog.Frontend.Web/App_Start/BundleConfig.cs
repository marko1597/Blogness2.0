using System;
using System.Web;
using System.Web.Optimization;

namespace Blog.Frontend.Web.App_Start
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            #region JS Files

            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                        "~/Scripts/plugins/jquery-1.11.0.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui-1.10.4.custom.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/modernizr").Include(
                        "~/Scripts/plugins/modernizr-*"));

            bundles.Add(new ScriptBundle("~/scripts/jqueryplugins").Include(
                        "~/Scripts/plugins/jquery.mCustomScrollbar.min.js",
                        "~/Scripts/plugins/jquery.mousewheel.min.js",
                        "~/Scripts/plugins/jquery.ui.touch-punch.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/jsplugins").Include(
                        "~/Scripts/plugins/underscore-min.js",
                        "~/Scripts/plugins/snap.min.js",
                        "~/Scripts/plugins/video-js/video.js"));

            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
                      "~/Scripts/plugins/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/angular").Include(
                      "~/Scripts/plugins/angular.min.js",
                      "~/Scripts/plugins/angular-route.min.js",
                      "~/Scripts/plugins/angular-cookies.min.js",
                      "~/Scripts/plugins/angular-animate.min.js",
                      "~/Scripts/plugins/angular-deckgrid.min.js",
                      "~/Scripts/plugins/angular-dragdrop.min.js",
                      "~/Scripts/plugins/angular-local-storage.js",
                      "~/Scripts/plugins/angular-loading-bar.min.js",
                      "~/Scripts/plugins/angular-strap.min.js",
                      "~/Scripts/plugins/angular-snap.min.js",
                      "~/Scripts/plugins/angular-strap.tpl.min.js",
                      "~/Scripts/plugins/angular-ui-bootstrap-tpls-0.10.0.min.js"));

            bundles.Add(new ScriptBundle("~/scripts/blog").Include(
                        "~/Scripts/modules/app.js"));

            bundles.Add(new ScriptBundle("~/scripts/header").Include(
                      "~/Scripts/modules/header/headerModule.js",
                      "~/Scripts/modules/header/directives/headerMenu.js"));

            #endregion

            #region CSS Files

            bundles.Add(new StyleBundle("~/content/cssplugins").Include(
                      "~/Content/plugins/bootstrap.min.css",
                      "~/Content/plugins/bootstrap-theme.min.css",
                      "~/Content/plugins/bootstrap-additions.min.css",
                      "~/Content/plugins/angular-motion.min.css",
                      "~/Content/plugins/angular-snap.min.css"));

            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/Content/site.css"));

            #endregion

            BundleTable.EnableOptimizations = false;
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }

            ignoreList.Ignore("*.intellisense.js", OptimizationMode.WhenEnabled);
            ignoreList.Ignore("*-vsdoc.js", OptimizationMode.WhenEnabled);
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            ignoreList.Ignore("*.min.css", OptimizationMode.WhenEnabled);
            ignoreList.Ignore("*.min.js", OptimizationMode.WhenEnabled);
        }
    }
}
