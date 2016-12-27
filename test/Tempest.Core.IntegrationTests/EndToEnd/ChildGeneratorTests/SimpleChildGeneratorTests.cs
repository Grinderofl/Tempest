using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions.Defaults;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Generator;
using Tempest.Core.IntegrationTests.EndToEnd.Helpers;
using Tempest.Core.Operations;
using Tempest.Core.Scaffolding;
using Tempest.Core.Utils;
using Xunit;

namespace Tempest.Core.IntegrationTests.EndToEnd.ChildGeneratorTests
{
    public class SimpleChildGeneratorTests
    {
        private class TestGenerator : GeneratorBase
        {
            private readonly TestHelper _helper;
            private readonly DependentGenerator _dependentGenerator;
            private string _text;

            public TestGenerator(TestHelper helper, DependentGenerator dependentGenerator)
            {
                _helper = helper;
                _dependentGenerator = dependentGenerator;
            }

            protected override IEnumerable<ConfigurationOption> CreateOptionsCore()
            {
                foreach(var option in base.CreateOptionsCore())
                    yield return option;
                foreach(var childOption in ((IExecutableGenerator) _dependentGenerator).CreateOptions())
                    yield return childOption;
            }

            protected override void ConfigureOptions(OptionsFactory options)
            {
                options.List("Foo or bar?").Choice("Foo", "foo", () => _text = "foo").Choice("Bar", "bar", () => _text = "bar");
            }

            protected override ScaffoldOperationConfiguration ConfigureOperationsCore(ScaffoldOperationConfiguration configuration)
            {
                configuration = base.ConfigureOperationsCore(configuration);
                return ((IExecutableGenerator) _dependentGenerator).ConfigureOperations(configuration);
            }

            protected override void ConfigureGenerator(IScaffoldBuilder builder)
            {
                builder.Create.FromString(_text).ToStream(_helper.Stream1);
            }
        }

        private class DependentGenerator : GeneratorBase
        {
            private readonly TestHelper _helper;
            private string _text;

            public DependentGenerator(TestHelper helper)
            {
                _helper = helper;
            }

            protected override void ConfigureOptions(OptionsFactory options)
            {
                options.Input("Allyourbase?", s => _text = s);
            }

            protected override void ConfigureGenerator(IScaffoldBuilder builder)
            {
                builder.Create.FromString(_text).ToStream(_helper.Stream2);
            }
        }

        private class TestHelper
        {
            public Stream Stream1 { get; set; } = new MemoryStream();
            public Stream Stream2 { get; set; } = new MemoryStream();
        }

        [Fact]
        public void test_dependent_generator()
        {
            var helper = new TestHelper();
            var context =
                BootstrapperHelper.CreateTestContext<TestGenerator>(x => x.Arguments = new[] {"foo", "AreBelongToUs"});
            new TestBootstrapperFactory(
                    x =>
                        x.RegisterConvention(new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(helper).AddScoped<DependentGenerator>())))
                .Create(context).Execute(new GeneratorExecutor());

            Assert.Equal("foo", helper.Stream1.ReadAsString());
            Assert.Equal("AreBelongToUs", helper.Stream2.ReadAsString());
        }
    }
}