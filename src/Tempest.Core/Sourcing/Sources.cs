namespace Tempest.Core.Sourcing
{
    public class Sources
    {
        public static Source FromString(string source) => new StringContentSource(source);
    }
}