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
        public virtual TransformationResult Transform(TransformerContext context)
        {
            return new TransformationResult
            {
                OutputStream = TransformStream(context),
                Filename = TransformFilename(context.Filename)
            };
        }

        protected virtual string TransformFilename(string source) => source;

        protected virtual Stream TransformStream(TransformerContext context) => context.TransformationStream;
    }
}