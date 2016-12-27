﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions.Defaults;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Generator;
using Tempest.Core.IntegrationTests.EndToEnd.Helpers;
using Tempest.Core.Scaffolding;
using Tempest.Core.Utils;
using Xunit;

namespace Tempest.Core.IntegrationTests.EndToEnd.ConfigurationTests
{
    public class SimpleInputTests
    {
        public class SimpleListOptionTests
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
                    options.Input("Output?", s => _text = s);
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
            public void test_simple_input()
            {
                var helper = new TestHelper();
                var context =
                    BootstrapperHelper.CreateTestContext<TestGenerator>(x => x.Arguments = new[] { "foo" });
                new TestBootstrapperFactory(
                        x =>
                            x.RegisterConvention(new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(helper))))
                    .Create(context).Execute(new GeneratorExecutor());

                Assert.Equal("foo", helper.Stream.ReadAsString());
            }

        }
    }
}