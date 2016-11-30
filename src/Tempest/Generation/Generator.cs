using System.IO;

namespace Tempest.Generation
{
    /// <summary>
    /// Generates an output stream
    /// Could be:
    /// GenerateEmpty
    /// GenerateByCopying
    /// GenerateFromWebstream
    /// </summary>
    public abstract class Generator
    {
        public virtual GenerationResult Generate(GenesisContext context)
        {
            return new GenerationResult() {OutputStream = GenerateCore(context)};
        }

        protected abstract Stream GenerateCore(GenesisContext context);
    }
}