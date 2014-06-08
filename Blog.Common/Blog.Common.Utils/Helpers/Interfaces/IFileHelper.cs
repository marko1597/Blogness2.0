namespace Blog.Common.Utils.Helpers.Interfaces
{
    public interface IFileHelper
    {
        bool CreateDirectory(string path);
        bool DeleteDirectory(string path);
        bool CreateFile(string path);
        bool DeleteFile(string path);
        bool MoveFile(string sourcePath, string targetPath);
    }
}
