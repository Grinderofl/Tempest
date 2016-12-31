using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Helpers;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Conventions.Defaults;
using Tempest.Core.Generator;
using Tempest.Core.Scaffolding;
using Tempest.Core.Utils;
using Tempest.IntegrationTests.EndToEnd.Helpers;
using Xunit;

namespace Tempest.IntegrationTests.EndToEnd.ConfigurationTests
{
    public class CombinedTests
    {
        private class TestGenerator : GeneratorBase
        {
            private readonly TestHelper _helper;
            private string _text;
            private bool _foo;

            public TestGenerator(TestHelper helper)
            {
                _helper = helper;
            }

            protected override void ConfigureOptions(OptionsFactory options)
            {
                options.List("Foo or bar?").Choice("Foo", "foo", () => _foo = true).Choice("Bar", "bar");
                options.Input("Output?", s => _text = s);
            }

            protected override void ConfigureGenerator(IScaffoldBuilder builder)
            {
                builder.Create.FromString(_text).ToStream(_helper.Stream2);
                if (_foo)
                    builder.Create.FromString("AllYourBase").ToStream(_helper.Stream1);
                else
                    builder.Create.FromString("AreBelongToUs").ToStream(_helper.Stream1);
            }
        }

        private class TestHelper
        {
            public Stream Stream1 { get; set; } = new MemoryStream();
            public Stream Stream2 { get; set; } = new MemoryStream();
        }

        [Fact]
        public void test_combined_options_one()
        {
            var helper = new TestHelper();
            var context =
                BootstrapperHelper.CreateTestContext<TestGenerator>(x => x.Arguments = new[] { "foo", "base" });
            new TestBootstrapperFactory(
                    x =>
                        x.RegisterConvention(new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(helper))))
                .Create(context).Execute();

            Assert.Equal("base", helper.Stream2.ReadAsString());
            Assert.Equal("AllYourBase", helper.Stream1.ReadAsString());
        }

        [Fact]
        public void test_combined_options_two()
        {
            var helper = new TestHelper();
            var context =
                BootstrapperHelper.CreateTestContext<TestGenerator>(x => x.Arguments = new[] { "bar", "base" });
            new TestBootstrapperFactory(
                    x =>
                        x.RegisterConvention(new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(helper))))
                .Create(context).Execute(new GeneratorExecutor());

            Assert.Equal("base", helper.Stream2.ReadAsString());
            Assert.Equal("AreBelongToUs", helper.Stream1.ReadAsString());
        }
    }
}