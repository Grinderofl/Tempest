using System.IO;
using Tempest.Generation;

namespace Tempest.Transformation
{
    /// <summary>
    /// Transforms the input stream into output transformation result
    /// Could be:
    /// TokenTransformer
    /// XdtTransformer
    /// JsonTransformer
    /// 
    /// </summary>
    public abstract class Transformer
    {
        public virtual TransformationResult Transform(TransformerContext context)
        {
            return new TransformationResult()
            {
                OutputStream = TransformCore(context)
            };
        }

        protected virtual Stream TransformCore(TransformerContext context)
        {
            return context.InputStream;
        }
    }
}