using Tempest.Core.Transformation;
using Tempest.Core.Utils;
using Xunit;

namespace Tempest.CoreTests.Transformation
{
    public class TransformersTests
    {
        public class EmptyTransformerTests
        {
            [Fact]
            public void transforms_to_empty()
            {
                var transformer = Transformers.Empty;
                var result = transformer.Transform(new TransformerContext() {TransformationStream = "TestContent".ToStream()});
                var resultString = result.OutputStream.ReadAsString();
                Assert.Equal("", resultString);

            }
        }

        public class NoOpTransformerTests
        {
            [Fact]
            public void does_not_transform()
            {
                var transformer = Transformers.NoOp;
                var result = transformer.Transform(new TransformerContext() {TransformationStream = "TestContent".ToStream()});
                var resultString = result.OutputStream.ReadAsString();
                Assert.Equal("TestContent", resultString);
            }
        }
    }
}
