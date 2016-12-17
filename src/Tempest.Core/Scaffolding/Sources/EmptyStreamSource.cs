using System.IO;

namespace Tempest.Core.Scaffolding.Sources
{
    public class EmptyStreamSource : AbstractStreamSource
    {

        public static AbstractStreamSource Instance = new EmptyStreamSource();

        private EmptyStreamSource()
        {
        }

        public override Stream Create()
        {
            return new MemoryStream();
        }

        protected override string GetStreamDescriptor()
        {
            return "[Empty stream]";
        }
    }
}