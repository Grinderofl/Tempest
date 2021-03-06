using System.IO;

namespace Tempest.Core.Operations.Transforms
{
    public class NoOpStreamTransformer : AbstractStreamTransformer
    {

        public static IStreamTransformer Instance => new NoOpStreamTransformer();

        protected NoOpStreamTransformer()
        {
            
        }

        public override Stream Transform(Stream stream)
        {
            return stream;
        }

        protected override string GetTransformerDescription()
        {
            return "Nothing";
        }
    }
}