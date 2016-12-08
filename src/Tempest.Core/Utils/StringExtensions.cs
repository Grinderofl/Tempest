using System;
using System.IO;
using System.Reflection;

namespace Tempest.Core.Utils
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

        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);

        public static bool IsNullOrWhiteSpace(this string str) => string.IsNullOrWhiteSpace(str);

        public static bool IsNotNullOrEmpty(this string str) => !str.IsNullOrEmpty();

        public static bool IsNotNullOrWhiteSpace(this string str) => !str.IsNullOrWhiteSpace();
    }

    public static class TypeExtensions
    {
        public static Assembly GetAssembly(this Type type)
        {
            return type.GetTypeInfo().Assembly;
        }
    }

    public static class AssemblyExtensions
    {
        public static DirectoryInfo GetAssemblyDirectory(this Assembly assembly)
        {
            var assemblyUri = new UriBuilder(assembly.CodeBase);
            var path = Uri.UnescapeDataString(assemblyUri.Path);
            return new DirectoryInfo(Path.GetDirectoryName(path));
        }
    }
}