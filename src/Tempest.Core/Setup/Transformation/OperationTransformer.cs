using System.IO;
using Tempest.Core.Scaffolding.Transforms;

namespace Tempest.Core.Setup.Transformation
{
    /// <summary>
    ///     Transforms the input stream into output transformation result
    ///     Could be:
    ///     TokenTransformer
    ///     XdtTransformer
    ///     JsonTransformer
    /// </summary>
    public abstract class OperationTransformer
    {
        public virtual string TransformFilename(string source) => source;

        public virtual IStreamTransformer CreateStreamTransformer() => NoOpStreamTransformer.Instance;
    }
}