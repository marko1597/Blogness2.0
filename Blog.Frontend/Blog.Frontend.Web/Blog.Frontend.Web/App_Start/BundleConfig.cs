using System.Web.Optimization;

namespace Blog.Frontend.Web.App_Start
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.9.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-1.9.2.custom.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));
            
            bundles.Add(new ScriptBundle("~/bundles/jqueryplugins").Include(
                        "~/Plugins/jquery.blockUI.js",
                        "~/Plugins/jquery.autosize.js",
                        "~/Plugins/jquery.tmpl.min.js",
                        "~/Plugins/jquery.stickem.js",
                        "~/Plugins/video.js"));

            bundles.Add(new ScriptBundle("~/bundles/blog").Include(
                        "~/Scripts/Site/Namespace.js",
                        "~/Scripts/Site/Location.js",
                        "~/Scripts/Site/DateTime.js",
                        "~/Scripts/Site/Events.js",
                        "~/Scripts/Site/FileUpload.js",
                        "~/Scripts/Site/Util.js",
                        "~/Scripts/Site/Comments.js",
                        "~/Scripts/Site/Posts.js",
                        "~/Scripts/Site/PostContent.js",
                        "~/Scripts/Site/PostDetails.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/site.css",
                        "~/Content/styles/comments/commentsection.css",
                        "~/Content/styles/comments/commentnewitem.css",
                        "~/Content/styles/comments/commentlist.css",
                        "~/Content/styles/comments/commentitem.css",
                        "~/Content/styles/contentbody.css",
                        "~/Content/styles/errorpage.css",
                        "~/Content/styles/reporterror.css",
                        "~/Content/styles/blogpost.css",
                        "~/Content/styles/login.css",
                        "~/Content/styles/loginpartial.css",
                        "~/Content/styles/tabmenu.css",
                        "~/Content/styles/likes.css",
                        "~/Content/styles/postdetails.css",
                        "~/Content/styles/post.css",
                        "~/Content/styles/postmedia.css",
                        "~/Content/styles/postedit.css",
                        "~/Content/styles/siteheader.css",
                        "~/Content/styles/sitefooter.css",
                        "~/Content/styles/postfooterdetails.css",
                        "~/Content/styles/postmodificationarea.css",
                        "~/Content/styles/plugins/video-js.css",
                        "~/Content/styles/plugins/tubecss.css",
                        "~/Content/shared.css"));

            bundles.Add(new ScriptBundle("~/bundles/fileupload").Include(
                        "~/Scripts/FileUploadPlugin/jquery.fileinput.js"));
            
            bundles.Add(new StyleBundle("~/Content/fileupload").Include(
                        "~/Content/styles/fileupload/enhanced.css",
                        "~/Content/styles/fileupload/fileupload.css"));

            bundles.Add(new StyleBundle("~/Content/themes/smoothness").Include(
                         "~/Content/themes/pepper-grinder/jquery-ui-1.9.2.custom.css"));

            BundleTable.EnableOptimizations = false;
        }
    }
}