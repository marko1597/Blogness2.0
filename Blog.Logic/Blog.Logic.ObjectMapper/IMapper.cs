namespace Blog.Logic.ObjectMapper
{
    public interface IMapper<TD, TE> where TD : class where TE: class
    {
        TD ToDto(TE entity);
        TE ToEntity(TD dto);
    }
}
