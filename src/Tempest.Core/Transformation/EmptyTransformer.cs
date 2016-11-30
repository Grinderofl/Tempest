using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Transformation
{
    public class EmptyTransformer : Transformer
    {
        protected override Stream TransformCore(TransformerContext context)
        {
            return "".ToStream();
        }
    }
}