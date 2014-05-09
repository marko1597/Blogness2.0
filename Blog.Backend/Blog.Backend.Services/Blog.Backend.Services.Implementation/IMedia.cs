﻿using System.Collections.Generic;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public interface IMedia
    {
        List<Media> GetByUser(int userId);
        List<Media> GetByGroup(int albumId);
        Media GetByName(string customName);
        Media Get(int mediaId);
        Media Add(Media media);
        Media Add(User user, string albumName, string filename, string path, string contentType);
        bool Delete(int mediaId);
    }
}
