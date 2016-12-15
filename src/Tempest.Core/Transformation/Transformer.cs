using System.IO;

namespace Tempest.Core.Transformation
{
    /// <summary>
    ///     Transforms the input stream into output transformation result
    ///     Could be:
    ///     TokenTransformer
    ///     XdtTransformer
    ///     JsonTransformer
    /// </summary>
    public abstract class Transformer
    {
        //public virtual TransformationResult Transform(TransformerContext context)
        //{
        //    return new TransformationResult(TransformStream(context), TransformFilename(context.Filename));

        //}

        public virtual string TransformFilename(string source) => source;

        public virtual Stream TransformStream(Stream context) => context;
    }
}