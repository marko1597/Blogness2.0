using System;
using System.IO;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers.Interfaces;

namespace Blog.Common.Utils.Helpers
{
    public class FileHelper : IFileHelper
    {
        public bool CreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool DeleteDirectory(string path)
        {
            try
            {
                var dirInfo = new DirectoryInfo(path);
                dirInfo.Delete(true);

                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool CreateFile(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    var tPath = Path.GetDirectoryName(path);
                    if (tPath != null) Directory.CreateDirectory(tPath);
                }

                File.Create(path).Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool MoveFile(string sourcePath, string targetPath)
        {
            try
            {
                File.Move(sourcePath, targetPath);
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
