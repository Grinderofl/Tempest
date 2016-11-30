using System.IO;

namespace Tempest.Utils
{
    public static class StreamExtensions
    {
        public static Stream ResetPosition(this Stream stream)
        {
            stream.Position = 0;
            return stream;
        }

        public static string ReadAsString(this Stream stream)
        {
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}