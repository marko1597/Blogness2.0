﻿using System.Web.Http;
using System.Web.Http.Cors;

namespace Blog.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            GlobalConfiguration.Configure(x => x.MapHttpAttributeRoutes());
        }
    }
}
