﻿namespace Tempest.Core.Configuration.Operations.Transformation
{
    public class Transformers
    {
        /// <summary>
        ///     No-operation transformer. Does absolutely nothing with the stream.
        /// </summary>
        public static OperationTransformerBase NoOp => new NoOpOperationTransformer();

        /// <summary>
        ///     Empty transformer. Yields a completely empty stream.
        /// </summary>
        public static OperationTransformerBase Empty => new EmptyOperationTransformer();

        /// <summary>
        ///     Token transformer. Replaces a token with a string.
        /// </summary>
        /// <param name="token"></param>
        /// <param name="replaceWith"></param>
        /// <param name="replaceFileNames"></param>
        /// <returns></returns>
        public static OperationTransformerBase Token(string token, string replaceWith, bool replaceFileNames = true)
            => new TokenOperationTransformer(token, replaceWith, replaceFileNames);
    }
}