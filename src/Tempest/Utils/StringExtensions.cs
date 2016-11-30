using System;
using System.IO;

namespace Tempest.Utils
{
    public static class StringExtensions
    {
        public static Stream ToStream(this string source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(source);
            writer.Flush();
            stream.ResetPosition();
            return stream;
        }
    }
}