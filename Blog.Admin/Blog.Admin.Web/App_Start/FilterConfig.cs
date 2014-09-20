using System.Web.Mvc;
using Blog.Admin.Web.Attributes;

namespace Blog.Admin.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ElmahHandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
        }
    }
}
