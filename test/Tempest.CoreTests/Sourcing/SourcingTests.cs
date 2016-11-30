using Tempest.Core.Sourcing;
using Tempest.Core.Utils;
using Xunit;

namespace Tempest.CoreTests.Generation
{
    public class SourcingTests
    {
        public class StringContentGeneratorTests
        {
            [Fact]
            public void generates_valid_stream()
            {
                var generator = new StringContentSource("TestContent");

                var result = generator.Generate(new SourcingContext());

                var resultValue = result.OutputStream.ReadAsString();
                Assert.Equal("TestContent", resultValue);
            }
        }
    }
}
