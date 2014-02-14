using System;
using System.Web.Optimization;

namespace Blog.Frontend.Web
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            #region JS Files

            bundles.Add(new ScriptBundle("~/scripts/jquery").Include(
                        "~/Scripts/plugins/jquery-1.11.0.js"));

            bundles.Add(new ScriptBundle("~/scripts/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui-1.10.4.custom.js"));

            bundles.Add(new ScriptBundle("~/scripts/modernizr").Include(
                        "~/Scripts/plugins/modernizr-*"));

            bundles.Add(new ScriptBundle("~/scripts/jqueryplugins").Include(
                        "~/Scripts/plugins/jquery.ui.touch-punch.js",
                        "~/Scripts/plugins/classie.js"));

            bundles.Add(new ScriptBundle("~/scripts/jsplugins").Include(
                        "~/Scripts/plugins/underscore.js",
                        "~/Scripts/plugins/video-js/video.js"));

            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
                      "~/Scripts/plugins/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/scripts/angular").Include(
                      "~/Scripts/plugins/angular.js",
                      "~/Scripts/plugins/angular-route.js",
                      "~/Scripts/plugins/angular-cookies.js",
                      "~/Scripts/plugins/angular-animate.js",
                      "~/Scripts/plugins/angular-deckgrid.js",
                      "~/Scripts/plugins/angular-dragdrop.js",
                      "~/Scripts/plugins/angular-local-storage.js",
                      "~/Scripts/plugins/angular-loading-bar.js",
                      "~/Scripts/plugins/angular-strap.js",
                      "~/Scripts/plugins/angular-strap.tpl.js",
                      "~/Scripts/plugins/angular-ui-bootstrap-tpls-0.10.0.js"));

            bundles.Add(new ScriptBundle("~/scripts/blog").Include(
                        "~/Scripts/modules/app.js"));

            bundles.Add(new ScriptBundle("~/scripts/shareddirectives").Include(
                        "~/Scripts/modules/shared/directives/keypress.js"));

            bundles.Add(new ScriptBundle("~/scripts/header").Include(
                      "~/Scripts/modules/header/headerModule.js",
                      "~/Scripts/modules/header/directives/headerMenu.js"));

            bundles.Add(new ScriptBundle("~/scripts/config").Include(
                      "~/Scripts/modules/config/configModule.js",
                      "~/Scripts/modules/config/provider/configProvider.js"));

            bundles.Add(new ScriptBundle("~/scripts/config").Include(
                      "~/Scripts/modules/config/configModule.js",
                      "~/Scripts/modules/config/provider/configProvider.js"));

            bundles.Add(new ScriptBundle("~/scripts/login").Include(
                      "~/Scripts/modules/login/loginModule.js",
                      "~/Scripts/modules/login/directives/loginForm.js",
                      "~/Scripts/modules/login/services/loginService.js"));

            bundles.Add(new ScriptBundle("~/scripts/login").Include(
                      "~/Scripts/modules/login/loginModule.js",
                      "~/Scripts/modules/login/directives/loginForm.js",
                      "~/Scripts/modules/login/services/loginService.js"));

            bundles.Add(new ScriptBundle("~/scripts/navigation").Include(
                      "~/Scripts/modules/navigation/navigationModule.js",
                      "~/Scripts/modules/navigation/directives/navigationMenu.js"));

            bundles.Add(new ScriptBundle("~/scripts/posts").Include(
                      "~/Scripts/modules/posts/postsModule.js",
                      "~/Scripts/modules/posts/directives/postsMain.js",
                      "~/Scripts/modules/posts/services/postsService.js"));

            #endregion

            #region CSS Files

            bundles.Add(new StyleBundle("~/content/cssplugins").Include(
                      "~/Content/plugins/angular-loading-bar.css",
                      "~/Content/plugins/angular-motion.css",
                      "~/Content/plugins/angular-scrollbar.css",
                      "~/Content/plugins/bootstrap.css",
                      "~/Content/plugins/bootstrap-theme.css",
                      "~/Content/plugins/bootstrap-additions.css",
                      "~/Content/plugins/jquery-ui-1.10.4.custom.css"));

            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/content/login").Include(
                      "~/Content/modules/loginform/loginform.css"));

            bundles.Add(new StyleBundle("~/content/navigation").Include(
                      "~/Content/modules/navigation/navigationmenu.css"));

            bundles.Add(new StyleBundle("~/content/posts").Include(
                      "~/Content/modules/posts/postsmain.css"));

            #endregion

            BundleTable.EnableOptimizations = true;
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
