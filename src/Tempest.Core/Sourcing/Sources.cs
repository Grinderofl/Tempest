using System.Reflection;

namespace Tempest.Core.Sourcing
{
    public class Sources
    {
        public static Source FromString(string source) => new StringContentSource(source);
        public static Source FromTemplate(string filePath) => new TemplateFileSource(filePath);

        public static Source FromResource(string resourcePath, Assembly resourceAssembly) => new ResourceFileSource(resourcePath, resourceAssembly);
    }
}