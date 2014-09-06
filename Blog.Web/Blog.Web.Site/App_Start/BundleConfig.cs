using System;
using System.Configuration;
using System.Web.Optimization;

namespace Blog.Web.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            #region JS Files

            #endregion

            #region CSS Files

            #endregion

            BundleTable.EnableOptimizations = !(Convert.ToBoolean(ConfigurationManager.AppSettings.Get("IsDebug")));
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }

            ignoreList.Ignore("*.intellisense.js", OptimizationMode.Always);
            ignoreList.Ignore("*-vsdoc.js", OptimizationMode.Always);
            ignoreList.Ignore("*.debug.js", OptimizationMode.Always);
            ignoreList.Ignore("*.min.css", OptimizationMode.Always);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
        }
    }
}
