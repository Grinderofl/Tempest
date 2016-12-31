using System.IO;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core.Utils;
using Tempest.IntegrationTests.EndToEnd.Base;
using Xunit;

namespace Tempest.IntegrationTests.EndToEnd.SourceToStreamTests
{
    public class MultipleStringSourceToStreamTests : ScaffoldingTestBase
    {

        [Fact]
        public void test_simple()
        {
            var stream1 = new MemoryStream();
            var stream2 = new MemoryStream();

            var strapper = CreateBootstrapper(scaffold =>
            {
                scaffold.Create.FromString("Foo").ToStream(stream1);
                scaffold.Create.FromString("Bar").ToStream(stream2);
            });
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("Foo", stream1.ReadAsString());
            Assert.Equal("Bar", stream2.ReadAsString());
        }


        [Fact]
        public void test_with_replace()
        {
            var stream1 = new MemoryStream();
            var stream2 = new MemoryStream();
            var strapper =
                CreateBootstrapper(
                    scaffold =>
                    {
                        scaffold.Create.FromString("Foo").TransformToken("o", "bar").ToStream(stream1);
                        scaffold.Create.FromString("Bar").TransformToken("a", "o").ToStream(stream2);
                    });
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("Fbarbar", stream1.ReadAsString());
            Assert.Equal("Bor", stream2.ReadAsString());
        }

        [Fact]
        public void test_with_global_replace()
        {
            var stream1 = new MemoryStream();
            var stream2 = new MemoryStream();
            var strapper = CreateBootstrapper(scaffold =>
            {
                scaffold.Create.FromString("Foo").ToStream(stream1);
                scaffold.Create.FromString("Bor").ToStream(stream2);
                scaffold.Globally.TransformToken("o", "bar");
            });
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("Fbarbar", stream1.ReadAsString());
            Assert.Equal("Bbarr", stream2.ReadAsString());
        }

        [Fact]
        public void test_with_replace_and_global_replace()
        {
            var stream1 = new MemoryStream();
            var stream2 = new MemoryStream();
            var strapper = CreateBootstrapper(scaffold =>
            {
                scaffold.Create.FromString("Bar").TransformToken("B", "F").ToStream(stream1);
                scaffold.Create.FromString("Far").TransformToken("r", "o").ToStream(stream2);
                scaffold.Globally.TransformToken("a", "o");
            });
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("For", stream1.ReadAsString());
            Assert.Equal("Foo", stream2.ReadAsString());
        }

        [Fact]
        public void test_with_two_replaces_and_global_replace()
        {
            var stream1 = new MemoryStream();
            var stream2 = new MemoryStream();
            var strapper = CreateBootstrapper(scaffold =>
            {
                scaffold.Create.FromString("Bar").TransformToken("B", "F").TransformToken("r", "o").ToStream(stream1);
                scaffold.Create.FromString("Baz").TransformToken("B", "F").TransformToken("z", "o").ToStream(stream2);
                scaffold.Globally.TransformToken("a", "o");
            });
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("Foo", stream1.ReadAsString());
            Assert.Equal("Foo", stream2.ReadAsString());
        }

        [Fact]
        public void test_with_replace_and_two_global_replaces()
        {
            var stream1 = new MemoryStream();
            var stream2 = new MemoryStream();
            var strapper = CreateBootstrapper(scaffold =>
            {
                scaffold.Create.FromString("Bar").TransformToken("r", "o").ToStream(stream1);
                scaffold.Create.FromString("Baz").TransformToken("z", "o").ToStream(stream2);
                scaffold.Globally.TransformToken("a", "o");
                scaffold.Globally.TransformToken("B", "F");
            });
            strapper.Execute(new GeneratorExecutor());
            Assert.Equal("Foo", stream1.ReadAsString());
            Assert.Equal("Foo", stream2.ReadAsString());
        }


    }
}