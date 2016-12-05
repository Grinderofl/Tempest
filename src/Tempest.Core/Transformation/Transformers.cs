namespace Tempest.Core.Transformation
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

        /// <summary>
        /// Token transformer. Replaces a token with a string.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="replaceWith"></param>
        /// <param name="replaceFileNames"></param>
        /// <returns></returns>
        public static Transformer Token(string token, string replaceWith, bool replaceFileNames = true) => new TokenTransformer(token, replaceWith, replaceFileNames);
    }
}