using System;
using System.Reflection;

namespace Tempest.Core.Setup.Sourcing
{
    public class Sources
    {
        public static SourceGenerator FromString(string source) => new StringContentSourceGenerator(source);
        public static SourceGenerator FromTemplate(string filePath) => new TemplateFileSourceGenerator(filePath);
        public static SourceGenerator FromResource(string resourcePath, Assembly resourceAssembly)
            => new ResourceFileSourceGenerator(resourcePath, resourceAssembly);
        public static SourceGenerator FromTarget(string filePath) =>new TargetFileSourceGenerator(filePath);
        public static SourceGenerator FromTemplateGlob(string glob) => new TemplateGlobSourceGenerator(glob);
        public static SourceGenerator FromUri(Uri uri) => new WebSourceGenerator(uri);
    }
}