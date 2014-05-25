using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class TagMapper
    {
        public static Tag ToDto(Db.Tag tag)
        {
            return tag == null ? null :
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

        public static Db.Tag ToEntity(Tag tag)
        {
            return tag == null ? null :
                new Db.Tag
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
