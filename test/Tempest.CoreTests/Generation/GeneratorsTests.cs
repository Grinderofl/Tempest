using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Generation;
using Tempest.Utils;
using Xunit;

namespace Tempest.CoreTests.Generators
{
    public class GeneratorsTests
    {
        public class StringContentGeneratorTests
        {
            [Fact]
            public void generates_valid_stream()
            {
                var generator = new StringContentGenerator("TestContent");

                var result = generator.Generate(new GenesisContext());

                var resultValue = result.OutputStream.ReadAsString();
                Assert.Equal("TestContent", resultValue);
            }
        }
    }
}
