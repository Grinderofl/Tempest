using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Tempest.Core.Setup.Sourcing;
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
                var generator = new StringContentSourceGenerator("TestContent");

                var result = generator.Generate(new SourcingContext());

                var resultValue = result.First().Source.Create().ReadAsString();
                Assert.Equal("TestContent", resultValue);
            }
        }

        public class TemplateFileSourceTests : IDisposable
        {
            public TemplateFileSourceTests()
            {
                File.WriteAllText("foo.bar", "Foobar");
            }
            [Fact]
            public void generates_valid_stream()
            {
                var source = BuildTemplateSourceLocation();
                var context = new SourcingContext()
                {
                    TemplateRoot = new DirectoryInfo(Directory.GetCurrentDirectory())
                };

                var result = source.Generate(context);
                var resultValue = result.First().Source.Create().ReadAsString();
                Assert.Equal("Foobar", resultValue);
            }

            private static TemplateFileSourceGenerator BuildTemplateSourceLocation()
            {
                var generator = new TemplateFileSourceGenerator("foo.bar");
                return generator;
            }

            public void Dispose()
            {
                File.Delete("foo.bar");
            }
        }

        public class ResourceSourceTests
        {
            [Fact]
            public void generates_valid_stream()
            {
                var source = new ResourceFileSourceGenerator("Tempest.CoreTests.Sourcing.EmbeddedResource.txt",
                    typeof(TemplateFileSourceTests).GetTypeInfo().Assembly);
                var context = new SourcingContext();
                var result = source.Generate(context);
                var resultValue = result.First().Source.Create().ReadAsString();
                Assert.Equal("FOOBAR", resultValue);
            }
        }
    }
}
