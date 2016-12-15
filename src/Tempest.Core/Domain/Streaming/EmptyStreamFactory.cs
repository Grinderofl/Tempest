using System.IO;

namespace Tempest.Core.Domain.Streaming
{
    public class EmptyStreamFactory : StreamFactory
    {

        public static StreamFactory Instance = new EmptyStreamFactory();

        private EmptyStreamFactory()
        {
        }

        public override Stream Create()
        {
            return new MemoryStream();
        }
    }
}