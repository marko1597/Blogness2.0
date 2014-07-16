using System;
using System.Configuration;
using Blog.Web.Site;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;
using ServiceStack.Redis;

[assembly: OwinStartup(typeof(Startup))]
namespace Blog.Web.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.UseCors(CorsOptions.AllowAll);
            app.Map("/signalr", map =>
            {
                if (IsRedisServerAvailable())
                {
                    // Sets up Redis persistence on all instances of Blog web application for SignalR
                    // This allows SignalR to push data thru websockets to all instances of Blog web application
                    GlobalHost.DependencyResolver.UseRedis(
                        ConfigurationManager.AppSettings.Get("RedisServer"),
                        Convert.ToInt32(ConfigurationManager.AppSettings.Get("RedisPort")),
                        string.Empty, "bloggity");
                }
                
                // Setup the CORS middleware to run before SignalR.
                // By default this will allow all origins. You can 
                // configure the set of origins and/or http verbs by
                // providing a cors options with a different policy.
                map.UseCors(CorsOptions.AllowAll);

                var hubConfiguration = new HubConfiguration
                {
                    // You can enable JSONP by uncommenting line below.
                    // JSONP requests are insecure but some older browsers (and some
                    // versions of IE) require JSONP to work cross domain
                    EnableJSONP = true
                };
                // Run the SignalR pipeline. We're not using MapSignalR
                // since this branch already runs under the "/signalr"
                // path.
                map.RunSignalR(hubConfiguration);
            });

            app.UseCors(CorsOptions.AllowAll);
        }

        /// <summary>
        /// Checks if Redis service is running
        /// </summary>
        /// <returns>Boolean return that tells if Redis service is running</returns>
        private static bool IsRedisServerAvailable()
        {
            try
            {
                using (var redisClient = new RedisClient(
                    ConfigurationManager.AppSettings.Get("RedisServer"), 
                    Convert.ToInt32(ConfigurationManager.AppSettings.Get("RedisPort"))))
                {
                    redisClient.Echo(string.Empty);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
