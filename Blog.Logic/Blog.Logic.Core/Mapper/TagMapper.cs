using Blog.Common.Contracts;

namespace Blog.Logic.Core.Mapper
{
    public static class TagMapper
    {
        public static Tag ToDto(DataAccess.Database.Entities.Objects.Tag tag)
        {
            return tag == null ?
                new Tag() : 
                new Tag
                {
                    TagId = tag.TagId,
                    TagName = tag.TagName,
                    CreatedBy = tag.CreatedBy,
                    CreatedDate = tag.CreatedDate,
                    ModifiedBy = tag.ModifiedBy,
                    ModifiedDate = tag.ModifiedDate
                };
        }

        public static DataAccess.Database.Entities.Objects.Tag ToEntity(Tag tag)
        {
            return tag == null ?
                new DataAccess.Database.Entities.Objects.Tag() :
                new DataAccess.Database.Entities.Objects.Tag
                {
                    TagId = tag.TagId,
                    TagName = tag.TagName,
                    CreatedBy = tag.CreatedBy,
                    CreatedDate = tag.CreatedDate,
                    ModifiedBy = tag.ModifiedBy,
                    ModifiedDate = tag.ModifiedDate
                };
        }
    }
}
