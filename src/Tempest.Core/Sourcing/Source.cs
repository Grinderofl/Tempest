using System.IO;

namespace Tempest.Core.Sourcing
{
    /// <summary>
    /// Generates an output stream
    /// Could be:
    /// GenerateEmpty
    /// GenerateByCopying
    /// GenerateFromWebstream
    /// </summary>
    public abstract class Source
    {
        public virtual SourcingResult Generate(SourcingContext context)
        {
            return new SourcingResult() {OutputStream = GenerateCore(context)};
        }

        protected abstract Stream GenerateCore(SourcingContext context);
    }
}