using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tempest.Core.Operations.Transforms
{
    public class CompoundStreamTransformer : AbstractStreamTransformer
    {
        private readonly IEnumerable<IStreamTransformer> _transformers;

        public CompoundStreamTransformer(IEnumerable<IStreamTransformer> transformers)
        {
            if (transformers == null) throw new ArgumentNullException(nameof(transformers));
            _transformers = transformers;
        }

        public override Stream Transform(Stream stream)
        {
            var outputStream = stream;
            foreach (var transformer in _transformers)
            {
                outputStream = transformer.Transform(outputStream);
            }
            return outputStream;
        }

        protected override string GetTransformerDescription()
        {
            return string.Join(", ", _transformers.Select(x => $"[{x.Describe()}]"));
        }
    }
}