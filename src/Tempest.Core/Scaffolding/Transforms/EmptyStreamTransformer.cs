using System.IO;
using Tempest.Core.Utils;

namespace Tempest.Core.Scaffolding.Transforms
{
    public class EmptyStreamTransformer : AbstractStreamTransformer
    {
        public static IStreamTransformer Instance => new EmptyStreamTransformer();

        private EmptyStreamTransformer()
        {  
        }

        public override Stream Transform(Stream stream) => "".ToStream();
    }
}