using Tempest.Core.Setup.Transformation;
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
                var result = transformer.CreateStreamTransformer();
                var resultString = result.Transform("Foo".ToStream()).ReadAsString();
                Assert.Equal("", resultString);

            }
        }

        public class NoOpTransformerTests
        {
            [Fact]
            public void does_not_transform()
            {
                var transformer = Transformers.NoOp;
                var result = transformer.CreateStreamTransformer();
                var resultString = result.Transform("TestContent".ToStream()).ReadAsString();
                Assert.Equal("TestContent", resultString);
            }
        }
    }
}
