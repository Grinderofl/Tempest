using System.IO;

namespace Tempest.Core.Scaffolding.Providers
{
    public class EmptyStreamProvider : AbstractStreamProvider
    {

        public static AbstractStreamProvider Instance = new EmptyStreamProvider();

        private EmptyStreamProvider()
        {
        }

        public override Stream Provide()
        {
            return new MemoryStream();
        }

        protected override string GetStreamDescriptor()
        {
            return "[Empty stream]";
        }
    }
}