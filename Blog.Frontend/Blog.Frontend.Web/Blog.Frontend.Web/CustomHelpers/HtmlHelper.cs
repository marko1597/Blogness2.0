using System.Web.Mvc;

namespace Blog.Frontend.Web.CustomHelpers
{
    public static class HtmlHelperExtension
    {
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src, string altText, string classStr)
        {
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src", src);
            builder.MergeAttribute("alt", altText);
            builder.MergeAttribute("class", classStr);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}