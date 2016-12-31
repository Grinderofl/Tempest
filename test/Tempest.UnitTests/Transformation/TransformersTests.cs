using System.Collections.Generic;
using Tempest.Core.Configuration.Operations.Transformation;
using Tempest.Core.Operations.Transforms;
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

        public class TokenTransformationTests
        {
            [Fact]
            public void transforms_tokens()
            {
                var transformer = new TokenStreamTransformer("foo", "bar");
                var result = transformer.Transform("foos".ToStream()).ReadAsString();
                Assert.Equal("bars", result);
            }

            [Fact]
            public void transforms_multiple_tokens()
            {
                var transformer = new MultiTokenStreamTransformer(new Dictionary<string, string>
                {
                    ["i"] = "u",
                    ["F"] = "B"
                });

                var result = transformer.Transform("Fizz".ToStream()).ReadAsString();
                Assert.Equal("Buzz", result);
            }

            [Fact]
            public void transforms_compound_tokens()
            {
                var transformer = new CompoundStreamTransformer(new []{new TokenStreamTransformer("i", "u"),new TokenStreamTransformer("F", "B") });
                var result = transformer.Transform("Fizz".ToStream()).ReadAsString();
                Assert.Equal("Buzz", result);
            }
        }
    }
}
