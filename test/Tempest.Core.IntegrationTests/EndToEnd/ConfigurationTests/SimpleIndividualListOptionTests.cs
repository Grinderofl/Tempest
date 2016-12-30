using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Helpers;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Conventions.Defaults;
using Tempest.Core.Generator;
using Tempest.Core.IntegrationTests.EndToEnd.Helpers;
using Tempest.Core.Scaffolding;
using Tempest.Core.Utils;
using Xunit;

namespace Tempest.Core.IntegrationTests.EndToEnd.ConfigurationTests
{
    public class SimpleIndividualListOptionTests
    {
        private class TestGenerator : GeneratorBase
        {
            private readonly TestHelper _helper;
            private string _text;

            public TestGenerator(TestHelper helper)
            {
                _helper = helper;
            }

            protected override void ConfigureOptions(OptionsFactory options)
            {
                options.List("Foo or bar?").Choice("Foo", "foo", () => _text = "foo").Choice("Bar", "bar", () => _text = "bar");
            }

            protected override void ConfigureGenerator(IScaffoldBuilder builder)
            {
                builder.Create.FromString(_text).ToStream(_helper.Stream);
            }
        }

        private class TestHelper
        {
            public Stream Stream { get; set; } = new MemoryStream();
        }

        [Fact]
        public void test_simple_list_option_one()
        {
            var helper = new TestHelper();
            var context =
                BootstrapperHelper.CreateTestContext<TestGenerator>(x => x.Arguments = new[] {"foo"});
            new TestBootstrapperFactory(
                    x =>
                        x.RegisterConvention(new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(helper))))
                .Create(context).Execute(new GeneratorExecutor());

            Assert.Equal("foo", helper.Stream.ReadAsString());
        }

        [Fact]
        public void test_simple_list_option_two()
        {
            var helper = new TestHelper();
            var context =
                BootstrapperHelper.CreateTestContext<TestGenerator>(x => x.Arguments = new[] { "bar" });
            new TestBootstrapperFactory(
                    x =>
                        x.RegisterConvention(new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(helper))))
                .Create(context).Execute(new GeneratorExecutor());

            Assert.Equal("bar", helper.Stream.ReadAsString());
        }
    }
}