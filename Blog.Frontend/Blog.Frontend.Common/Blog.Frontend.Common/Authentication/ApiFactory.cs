using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Blog.Frontend.Common.Helper;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Frontend.Common.Authentication
{
    public class ApiFactory
    {
        private ApiFactory()
        {
        }

        private static ApiFactory _instance;

        public static ApiFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ApiFactory();
                return _instance;
            }
            return _instance;
        }

        public Api CreateApi()
        {
            return new Api();
        }
    }
}
