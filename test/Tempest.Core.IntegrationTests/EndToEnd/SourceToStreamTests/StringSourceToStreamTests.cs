using System.IO;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core.IntegrationTests.EndToEnd.Base;
using Tempest.Core.Utils;
using Xunit;

namespace Tempest.Core.IntegrationTests.EndToEnd.SourceToStreamTests
{
    public class StringSourceToStreamTests : ScaffoldingTestBase
    {

        [Fact]
        public void test_simple()
        {
            var stream = new MemoryStream();
            var strapper = CreateBootstrapper(scaffold => scaffold.Create.FromString("Foo").ToStream(stream));
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("Foo", stream.ReadAsString());
        }


        [Fact]
        public void test_with_replace()
        {
            var stream = new MemoryStream();
            var strapper =
                CreateBootstrapper(
                    scaffold => scaffold.Create.FromString("Foo").TransformToken("o", "bar").ToStream(stream));
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("Fbarbar", stream.ReadAsString());
        }

        [Fact]
        public void test_with_global_replace()
        {
            var stream = new MemoryStream();
            var strapper = CreateBootstrapper(scaffold =>
            {
                scaffold.Create.FromString("Foo").ToStream(stream);
                scaffold.Globally.TransformToken("o", "bar");
            });
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("Fbarbar", stream.ReadAsString());
        }

        [Fact]
        public void test_with_replace_and_global_replace()
        {
            var stream = new MemoryStream();
            var strapper = CreateBootstrapper(scaffold =>
            {
                scaffold.Create.FromString("Bar").TransformToken("B", "F").ToStream(stream);
                scaffold.Globally.TransformToken("a", "o");
            });
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("For", stream.ReadAsString());
        }

        [Fact]
        public void test_with_two_replaces_and_global_replace()
        {
            var stream = new MemoryStream();
            var strapper = CreateBootstrapper(scaffold =>
            {
                scaffold.Create.FromString("Bar").TransformToken("B", "F").TransformToken("r", "o").ToStream(stream);
                scaffold.Globally.TransformToken("a", "o");
            });
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("Foo", stream.ReadAsString());
        }

        [Fact]
        public void test_with_replace_and_two_global_replaces()
        {
            var stream = new MemoryStream();
            var strapper = CreateBootstrapper(scaffold =>
            {
                scaffold.Create.FromString("Bar").TransformToken("B", "F").ToStream(stream);
                scaffold.Globally.TransformToken("a", "o");
                scaffold.Globally.TransformToken("r", "o");
            });
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("Foo", stream.ReadAsString());
        }

        
    }
}
