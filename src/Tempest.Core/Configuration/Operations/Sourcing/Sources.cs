using System;
using System.Reflection;
using Tempest.Core.Utils;

namespace Tempest.Core.Configuration.Operations.Sourcing
{
    public class Sources
    {
        public static SourceFactory FromString(string source, string filename = "") => new StringContentSourceFactory(source, filename);
        public static SourceFactory FromTemplate(string filePath) => new TemplateFileSourceFactory(filePath);

        public static SourceFactory FromResourceOf<T>(string resourcePath)
            => FromResource(resourcePath, typeof(T).GetAssembly());
        public static SourceFactory FromResource(string resourcePath, Assembly resourceAssembly)
            => new ResourceFileSourceFactory(resourcePath, resourceAssembly);
        public static SourceFactory FromResource(string resourcePath, Func<Assembly> assemblyFunc)
            => new ResourceFileSourceFuncFactory(resourcePath, assemblyFunc);

        public static SourceFactory FromResourcePath(string resourcePath, Assembly resourceAssembly) => new
            ResourcePathFileSourceFactory(resourcePath, resourceAssembly);

        public static SourceFactory FromResourcePathOf<T>(string resourcePath) => FromResourcePath(resourcePath,
            typeof(T).GetAssembly());

        public static SourceFactory FromTarget(string filePath) =>new TargetFileSourceFactory(filePath);
        public static SourceFactory FromTemplateGlob(string glob) => new TemplateGlobSourceFactory(glob);
        public static SourceFactory FromUri(Uri uri) => new WebSourceFactory(uri);
    }
}