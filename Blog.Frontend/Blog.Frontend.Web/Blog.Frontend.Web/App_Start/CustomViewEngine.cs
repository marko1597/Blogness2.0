using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Blog.Frontend.Common;

namespace Blog.Frontend.Web.App_Start
{
    public class CustomViewEngine : RazorViewEngine
    {
        private static readonly string[] ViewLocations = GetSharedViewFolders(); 

        public CustomViewEngine()
        {
            ViewLocationFormats = ViewLocationFormats.Union(ViewLocations).ToArray();
            PartialViewLocationFormats = PartialViewLocationFormats.Union(ViewLocations).ToArray();
        }

        private static string[] GetSharedViewFolders()
        {
            var configValues = (NameValueCollection)ConfigurationManager.GetSection(Constants.RadixSharedViewFolders);
            return configValues.Cast<object>().Select((t, i) => configValues.Get(i)).ToArray();
        }
    }
}