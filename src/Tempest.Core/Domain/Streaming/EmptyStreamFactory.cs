using System.IO;

namespace Tempest.Core.Domain.Streaming
{
    public class EmptyStreamFactory : AbstractStreamFactory
    {

        public static AbstractStreamFactory Instance = new EmptyStreamFactory();

        private EmptyStreamFactory()
        {
        }

        public override Stream Create()
        {
            return new MemoryStream();
        }
    }
}