using System.IO;
using Tempest.Core.Sourcing;
using Tempest.Core.Utils;
using Xunit;

namespace Tempest.CoreTests.Sourcing
{
    public class SourcingTests
    {
        public class StringContentSourceTests
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

        public class TemplateFileSourceTests
        {
            [Fact]
            public void generates_valid_stream()
            {
                var generator = new TemplateFileSource("LICENSE.txt"); // Tests current directory should be dotnet.exe I think?
                var context = new SourcingContext()
                {
                    TemplateRoot = new DirectoryInfo(Directory.GetCurrentDirectory())
                };
                
                var result = generator.Generate(context);
                var resultValue = result.OutputStream.ReadAsString();
                Assert.True(resultValue.Length > 0);
            }
        }
    }
}
