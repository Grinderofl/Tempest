using System;
using System.IO;

namespace Tempest.Core.Operations.Transforms
{
    public class ExpressionBasedStreamTransformer : AbstractStreamTransformer
    {
        private readonly Func<Stream, Stream> _transformerFunc;

        public static IStreamTransformer Create(Func<Stream, Stream> func) => new ExpressionBasedStreamTransformer(func);

        private ExpressionBasedStreamTransformer(Func<Stream, Stream> transformerFunc)
        {
            _transformerFunc = transformerFunc;
        }

        public override Stream Transform(Stream stream) => _transformerFunc(stream);
        protected override string GetTransformerDescription()
        {
            return $"Expression";
        }
    }
}