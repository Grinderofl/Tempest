using Tempest.Core.Operations.Transforms;

namespace Tempest.Core.Configuration.Operations.Transformation
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