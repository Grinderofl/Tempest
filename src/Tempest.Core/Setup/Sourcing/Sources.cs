using System;
using System.Reflection;

namespace Tempest.Core.Setup.Sourcing
{
    public class Sources
    {
        public static SourceFactory FromString(string source) => new StringContentSourceFactory(source);
        public static SourceFactory FromTemplate(string filePath) => new TemplateFileSourceFactory(filePath);
        public static SourceFactory FromResource(string resourcePath, Assembly resourceAssembly)
            => new ResourceFileSourceFactory(resourcePath, resourceAssembly);
        public static SourceFactory FromTarget(string filePath) =>new TargetFileSourceFactory(filePath);
        public static SourceFactory FromTemplateGlob(string glob) => new TemplateGlobSourceFactory(glob);
        public static SourceFactory FromUri(Uri uri) => new WebSourceFactory(uri);
    }
}