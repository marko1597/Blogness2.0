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
                        "~/Scripts/plugins/jquery-*"));

            bundles.Add(new ScriptBundle("~/scripts/jqueryui").Include(
                        "~/Scripts/plugins/jquery-ui-*"));

            bundles.Add(new ScriptBundle("~/scripts/modernizr").Include(
                        "~/Scripts/plugins/modernizr-*"));

            bundles.Add(new ScriptBundle("~/scripts/jqueryplugins").Include(
                        "~/Scripts/plugins/jquery.validate.js",
                        "~/Scripts/plugins/jquery.validate.unobtrusive.js",
                        "~/Scripts/plugins/jquery.ui.touch-punch.js",
                        "~/Scripts/plugins/jquery.blockui.js",
                        "~/Scripts/plugins/jquery.isotope.js",
                        "~/Scripts/plugins/jquery.newsticker.js",
                        "~/Scripts/plugins/classie.js",
                        "~/Scripts/plugins/snap.js"));

            bundles.Add(new ScriptBundle("~/scripts/jsplugins").Include(
                        "~/Scripts/plugins/respond.js",
                        "~/Scripts/plugins/underscore.js",
                        "~/Scripts/plugins/video-js/video.js"));

            bundles.Add(new ScriptBundle("~/scripts/bootstrap").Include(
                      "~/Scripts/plugins/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/scripts/angular").Include(
                      "~/Scripts/plugins/angular.js",
                      "~/Scripts/plugins/angular-route.js",
                      "~/Scripts/plugins/angular-cookies.js",
                      "~/Scripts/plugins/angular-animate.js",
                      "~/Scripts/plugins/angular-isotope.js",
                      "~/Scripts/plugins/angular-local-storage.js",
                      "~/Scripts/plugins/angular-snap.js",
                      "~/Scripts/plugins/angular-infinite-scroll.js",
                      "~/Scripts/plugins/angular-strap.js",
                      "~/Scripts/plugins/angular-strap.tpl.js"));

            bundles.Add(new ScriptBundle("~/scripts/blog").Include(
                        "~/Scripts/modules/app.js"));

            bundles.Add(new ScriptBundle("~/scripts/shared").Include(
                        "~/Scripts/modules/shared/shared.js",
                        "~/Scripts/modules/shared/services/blockUi.js",
                        "~/Scripts/modules/shared/directives/ticker.js",
                        "~/Scripts/modules/shared/directives/keypress.js"));

            bundles.Add(new ScriptBundle("~/scripts/header").Include(
                      "~/Scripts/modules/header/header.js",
                      "~/Scripts/modules/header/directives/headerMenu.js"));

            bundles.Add(new ScriptBundle("~/scripts/config").Include(
                      "~/Scripts/modules/config/config.js",
                      "~/Scripts/modules/config/provider/configProvider.js"));

            bundles.Add(new ScriptBundle("~/scripts/user").Include(
                      "~/Scripts/modules/user/user.js",
                      "~/Scripts/modules/user/services/userService.js"));

            bundles.Add(new ScriptBundle("~/scripts/login").Include(
                      "~/Scripts/modules/login/login.js",
                      "~/Scripts/modules/login/directives/loginForm.js",
                      "~/Scripts/modules/login/services/loginService.js"));

            bundles.Add(new ScriptBundle("~/scripts/navigation").Include(
                      "~/Scripts/modules/navigation/navigation.js",
                      "~/Scripts/modules/navigation/directives/navigationMenu.js"));

            bundles.Add(new ScriptBundle("~/scripts/posts").Include(
                      "~/Scripts/modules/posts/posts.js",
                      "~/Scripts/modules/posts/controllers/postsController.js",
                      "~/Scripts/modules/posts/directives/postsMain.js",
                      "~/Scripts/modules/posts/directives/postItem.js",
                      "~/Scripts/modules/posts/directives/postLikes.js",
                      "~/Scripts/modules/posts/directives/postTags.js",
                      "~/Scripts/modules/posts/directives/postItemComment.js",
                      "~/Scripts/modules/posts/services/postsService.js"));

            #endregion

            #region CSS Files

            bundles.Add(new StyleBundle("~/content/cssplugins").Include(
                      "~/Content/plugins/angular-loading-bar.css",
                      "~/Content/plugins/angular-motion.css",
                      "~/Content/plugins/angular-snap.css",
                      "~/Content/plugins/angular-scrollbar.css",
                      "~/Content/plugins/bootstrap.css",
                      "~/Content/plugins/bootstrap.css",
                      "~/Content/plugins/bootstrap-theme.css",
                      "~/Content/plugins/bootstrap-additions.css",
                      "~/Content/plugins/isotope.css",
                      "~/Content/plugins/jquery-ui-*"));

            bundles.Add(new StyleBundle("~/content/css").Include(
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/content/header").Include(
                      "~/Content/modules/header/header.css"));

            bundles.Add(new StyleBundle("~/content/login").Include(
                      "~/Content/modules/loginform/loginform.css"));

            bundles.Add(new StyleBundle("~/content/navigation").Include(
                      "~/Content/modules/navigation/navigationmenu.css"));

            bundles.Add(new StyleBundle("~/content/posts").Include(
                      "~/Content/modules/posts/postsmain.css"));

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
