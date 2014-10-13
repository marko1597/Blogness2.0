using System;
using System.Collections.Generic;
using System.Linq;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Interfaces;
using Blog.Logic.ObjectMapper;
using Blog.Common.Contracts;
using Blog.Common.Utils.Extensions;

namespace Blog.Logic.Core
{
    public class ViewCountLogic : IViewCountLogic
    {
        private readonly IViewCountRepository _viewCountRepository;

        public ViewCountLogic(IViewCountRepository viewCountRepository)
        {
            _viewCountRepository = viewCountRepository;
        }

        public List<ViewCount> Get(int postId)
        {
            var viewCounts = new List<ViewCount>();
            try
            {
                var db = _viewCountRepository.Find(a => a.PostId == postId, true).ToList();
                db.ForEach(a => viewCounts.Add(ViewCountMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return viewCounts;
        }

        public  ViewCount Add(ViewCount viewCount)
        {
            try
            {
                return ViewCountMapper.ToDto(_viewCountRepository.Add(ViewCountMapper.ToEntity(viewCount)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
