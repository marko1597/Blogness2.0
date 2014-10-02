namespace Blog.Common.Utils.Helpers.Interfaces
{
    public interface IPropertyReflection
    {
        bool HasProperty<T>(T src, string property) where T : class;
        object GetPropertyValue<T>(T src, string property) where T : class;
        object GetPropertyValue<T>(T src, string property, bool isRecursive) where T : class;
        void SetProperty<T>(T src, string property, object value) where T : class;
    }
}
