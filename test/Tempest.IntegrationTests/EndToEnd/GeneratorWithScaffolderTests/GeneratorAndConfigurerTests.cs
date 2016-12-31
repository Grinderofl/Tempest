using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Helpers;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Conventions.Defaults;
using Tempest.Core.Generator;
using Tempest.Core.Scaffolding;
using Tempest.Core.Utils;
using Tempest.IntegrationTests.EndToEnd.Helpers;
using Xunit;

namespace Tempest.IntegrationTests.EndToEnd.GeneratorWithScaffolderTests
{
    public class GeneratorAndConfigurerTests
    {
        private class TestGenerator : GeneratorBase
        {
            private TestHelper _testHelper;
            private TestGeneratorOptions _generatorOptions;

            public TestGenerator(TestHelper testHelper, TestGeneratorOptions generatorOptions)
            {
                _testHelper = testHelper;
                _generatorOptions = generatorOptions;
            }

            protected override void ConfigureOptions(OptionsFactory options)
            {
                _testHelper.OptionsAction?.Invoke(options, _generatorOptions);
            }

            protected override void ConfigureGenerator(IScaffoldBuilder builder)
            {
                _testHelper.ScaffoldAction?.Invoke(builder, _generatorOptions);
            }
        }

        private class TestHelper
        {
            public Stream Stream1 { get; set; } = new MemoryStream();
            public Stream Stream2 { get; set; } = new MemoryStream();

            public Action<OptionsFactory, TestGeneratorOptions> OptionsAction { get; set; }
            public Action<IScaffoldBuilder, TestGeneratorOptions> ScaffoldAction { get; set; }
        }

        private class TestGeneratorOptions
        {
            public string Choice1 { get; set; }
            public string Choice2 { get; set; }
        }

        private class TestScaffolderConfigurer : AbstractScaffolderConfigurer
        {
            public override int Order => 0;

            private TestGeneratorOptions _options;
            private TestHelper _helper;

            public TestScaffolderConfigurer(TestGeneratorOptions options, TestHelper helper)
            {
                _options = options;
                _helper = helper;
            }

            protected override void ConfigureScaffolder(IScaffoldBuilder scaffolder)
            {
                scaffolder.Create.FromString(_options.Choice1).ToStream(_helper.Stream1);
            }
        }

        [Fact]
        public void test_simple()
        {
            var helper = new TestHelper();
            helper.OptionsAction = (factory, options) => options.Choice1 = "foo";

            var context = BootstrapperHelper.CreateTestContext<TestGenerator>();
            new TestBootstrapperFactory(strapper =>
                strapper.RegisterConvention(
                    new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(helper)
                        .AddSingleton<TestGeneratorOptions>().AddSingleton<IScaffoldConfigurer, TestScaffolderConfigurer>()))).Create(context).Execute();

            Assert.Equal("foo", helper.Stream1.ReadAsString());
        }

        [Fact]
        public void test_combined()
        {
            var helper = new TestHelper();
            helper.OptionsAction = (factory, options) => options.Choice1 = "foo";
            helper.ScaffoldAction = (builder, options) => builder.Create.FromString("bar").ToStream(helper.Stream2);
            var context = BootstrapperHelper.CreateTestContext<TestGenerator>();
            new TestBootstrapperFactory(strapper =>
                strapper.RegisterConvention(
                    new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(helper)
                        .AddSingleton<TestGeneratorOptions>().AddSingleton<IScaffoldConfigurer, TestScaffolderConfigurer>()))).Create(context).Execute();

            Assert.Equal("foo", helper.Stream1.ReadAsString());
            Assert.Equal("bar", helper.Stream2.ReadAsString());
        }

        [Fact]
        public void test_generator_runs_before_configurer()
        {
            var helper = new TestHelper();
            helper.OptionsAction = (factory, options) => options.Choice1 = "foo";
            helper.ScaffoldAction = (builder, options) => builder.Create.FromString("bar").ToStream(helper.Stream1);
            var context = BootstrapperHelper.CreateTestContext<TestGenerator>();
            new TestBootstrapperFactory(strapper =>
                strapper.RegisterConvention(
                    new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(helper)
                        .AddSingleton<TestGeneratorOptions>().AddSingleton<IScaffoldConfigurer, TestScaffolderConfigurer>()))).Create(context).Execute();

            Assert.Equal("barfoo", helper.Stream1.ReadAsString());
        }

    }
}