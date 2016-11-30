using System.IO;
using Tempest.Utils;

namespace Tempest.Transformation
{
    public class EmptyTransformer : Transformer
    {
        protected override Stream TransformCore(TransformerContext context)
        {
            return "".ToStream();
        }
    }
}