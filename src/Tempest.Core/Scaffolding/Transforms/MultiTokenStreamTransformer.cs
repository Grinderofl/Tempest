using System;
using System.Collections.Generic;
using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Scaffolding.Transforms
{
    public class MultiTokenStreamTransformer : AbstractStreamTransformer
    {
        private readonly Dictionary<string, string> _tokenReplacers;

        public MultiTokenStreamTransformer(Dictionary<string, string> tokenReplacers)
        {
            if (tokenReplacers == null) throw new ArgumentNullException(nameof(tokenReplacers));
            _tokenReplacers = tokenReplacers;
        }

        public override Stream Transform(Stream stream)
        {
            var @string = stream.ReadAsString();
            foreach (var replacer in _tokenReplacers)
            {
                @string = @string.Replace(replacer.Key, replacer.Value);
            }
            return @string.ToStream();
        }
    }
}