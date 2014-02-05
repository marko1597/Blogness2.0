using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface IMediaGroup
    {
        List<UserMediaGroup> GetByUser(int userId);
        MediaGroup Add(MediaGroup mediaGroup);
        MediaGroup Update(MediaGroup mediaGroup);
        void Delete(MediaGroup mediaGroup);
    }
}
