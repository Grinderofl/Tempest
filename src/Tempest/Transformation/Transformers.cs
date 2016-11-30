namespace Tempest.Transformation
{
    public class Transformers
    {

        /// <summary>
        /// No-operation transformer. Does absolutely nothing with the stream.
        /// </summary>
        public static Transformer NoOp => new NoOpTransformer();

        /// <summary>
        /// Empty transformer. Yields a completely empty stream.
        /// </summary>
        public static Transformer Empty => new EmptyTransformer();
    }
}