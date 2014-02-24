using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class MediaGroupMapper
    {
        public static MediaGroup ToDto(DataAccess.Entities.Objects.MediaGroup mediaGroup)
        {
            if (mediaGroup != null)
            {
                var media = mediaGroup.Media.Select(MediaMapper.ToDto).ToList();

                return new MediaGroup
                {
                    MediaGroupId = mediaGroup.MediaGroupId,
                    MediaGroupName = mediaGroup.MediaGroupName,
                    Media = media,
                    User = UserMapper.ToDto(mediaGroup.User),
                    IsUserDefault = mediaGroup.IsUserDefault,
                    CreatedBy = mediaGroup.CreatedBy,
                    CreatedDate = mediaGroup.CreatedDate,
                    ModifiedBy = mediaGroup.ModifiedBy,
                    ModifiedDate = mediaGroup.ModifiedDate
                };
            }
            return new MediaGroup();
        }

        public static DataAccess.Entities.Objects.MediaGroup ToEntity(MediaGroup mediaGroup)
        {
            if (mediaGroup != null)
            {
                var media = mediaGroup.Media.Select(MediaMapper.ToEntity).ToList();

                return new DataAccess.Entities.Objects.MediaGroup
                {
                    MediaGroupId = mediaGroup.MediaGroupId,
                    MediaGroupName = mediaGroup.MediaGroupName,
                    Media = media,
                    User = UserMapper.ToEntity(mediaGroup.User),
                    IsUserDefault = mediaGroup.IsUserDefault,
                    CreatedBy = mediaGroup.CreatedBy,
                    CreatedDate = mediaGroup.CreatedDate,
                    ModifiedBy = mediaGroup.ModifiedBy,
                    ModifiedDate = mediaGroup.ModifiedDate
                };
            }
            return new DataAccess.Entities.Objects.MediaGroup();
        }
    }
}
