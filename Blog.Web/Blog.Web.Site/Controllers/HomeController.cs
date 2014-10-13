using System.Linq;
using System.Net;
using System.Web.Mvc;
using Blog.Common.Utils.Helpers.Interfaces;

namespace Blog.Web.Site.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientHelper _httpClientHelper;
        private readonly IConfigurationHelper _configurationHelper;

        public HomeController(IHttpClientHelper httpClientHelper, IConfigurationHelper configurationHelper)
        {
            _httpClientHelper = httpClientHelper;
            _configurationHelper = configurationHelper;
        }

        public ActionResult Index()
        {
            ViewBag.IsBlogSocketsAvailable = IsBlogSocketsAvailable();
            return IsBrowserUnsupported() ? View("UnsupportedBrowser") : View();
        }

        private bool IsBrowserUnsupported()
        {
            var unsupportedBrowsers = new[] { "internetexplorer", "opera" };
            var browser = Request.Browser;
            return unsupportedBrowsers.Contains(browser.Browser.ToLower());
        }

        private bool IsBlogSocketsAvailable()
        {
            try
            {
                var result = _httpClientHelper.HttpGet(_configurationHelper.GetAppSettings("BlogSockets"),
                    "favicon.ico");

                return result.StatusCode != HttpStatusCode.ServiceUnavailable;
            }
            catch
            {
                return false;
            }
        }
    }
}