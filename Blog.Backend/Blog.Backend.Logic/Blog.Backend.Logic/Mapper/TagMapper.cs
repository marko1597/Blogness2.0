using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class TagMapper
    {
        public static Tag ToDto(DataAccess.Entities.Objects.Tag tag)
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

        public static DataAccess.Entities.Objects.Tag ToEntity(Tag tag)
        {
            return tag == null ?
                new DataAccess.Entities.Objects.Tag() :
                new DataAccess.Entities.Objects.Tag
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
