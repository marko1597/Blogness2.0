using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Net.Http;
using Blog.Common.Utils.Extensions;

namespace Blog.Common.Utils.Helpers
{
    [ExcludeFromCodeCoverage]
    public class MediaStream
    {
        private readonly string _filename;

        public MediaStream(string filename)
        {
            _filename = filename;
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                var buffer = new byte[65536];

                using (var media = File.Open(_filename, FileMode.Open, FileAccess.Read))
                {
                    var length = (int)media.Length;
                    var bytesRead = 1;

                    while (length > 0 && bytesRead > 0)
                    {
                        bytesRead = media.Read(buffer, 0, Math.Min(length, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        length -= bytesRead;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex);
            }
            finally
            {
                outputStream.Close();
            }
        }
    }
}
