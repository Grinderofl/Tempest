using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tempest.Core.Scaffolding
{
    /// <summary>
    /// Represents a single unit of work.
    /// In the context of scaffolder, a unit of work is:
    /// 
    /// 1) The action of taking a source stream
    /// 2) Utilising various transformers to transform its contents
    /// 3) The action of outputting to a destination stream
    /// </summary>
    public abstract class Operation
    {
        private IList<Func<Stream, Stream>> _transformers = new List<Func<Stream, Stream>>();

        public virtual void Execute()
        {
            var source = ObtainSourceStream();
            var destination = ObtainDestinationStream();
            CopyFromSourceToDestination(source, destination);
        }

        protected virtual void CopyFromSourceToDestination(Stream source, Stream destination)
        {
            Stream outputStream = source;
            foreach (var transformer in _transformers)
            {
                outputStream = transformer(outputStream);
            }
        }

        protected abstract Stream ObtainSourceStream();
        protected abstract Stream ObtainDestinationStream();
    }

    public class EmptyOperation : Operation
    {
        public Stream SourceStream { get; set; } = new MemoryStream();
        public Stream DestinationStream { get; set; } = new MemoryStream();

        protected override Stream ObtainSourceStream()
        {
            return SourceStream;
        }

        protected override Stream ObtainDestinationStream()
        {
            return DestinationStream;
        }
    }
}
