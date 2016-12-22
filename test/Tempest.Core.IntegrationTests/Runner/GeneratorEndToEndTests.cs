using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions.Defaults;
using Tempest.Boot.Runner.Activation.Impl;
using Tempest.Boot.Strappers.Defaults;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Generator;
using Tempest.Core.Operations;
using Tempest.Core.Scaffolding;
using Tempest.Core.Utils;
using Xunit;

namespace Tempest.Core.IntegrationTests.Runner
{
    public class GeneratorEndToEndTests
    {
        public class RunningMockGenerator
        {
            private class TestStreamSource
            {
                public Stream Stream { get; set; }
            }

            private class MockGenerator : GeneratorBase
            {
                private readonly TestStreamSource _streamSource;

                public MockGenerator(TestStreamSource streamSource)
                {
                    _streamSource = streamSource;
                }

                protected override void ConfigureOptions(OptionsFactory options)
                {    
                }

                protected override void ConfigureGenerator(ScaffolderConfigurer scaffold)
                {
                    scaffold.Create.FromString("Foo").ToStream(_streamSource.Stream);
                }
            }

            private class TestGeneratorBootstrapperFactory : GeneratorBootstrapperFactory
            {
                private readonly TestStreamSource _streamSource;

                public TestGeneratorBootstrapperFactory(TestStreamSource streamSource)
                {
                    _streamSource = streamSource;
                }

                protected override void ConfigureBootstrapper(GeneratorBootstrapper bootstrapper, GeneratorContext generatorContext)
                {
                    base.ConfigureBootstrapper(bootstrapper, generatorContext);
                    bootstrapper.RegisterConvention(new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(_streamSource)));
                }
            }

            [Fact]
            public void executes_generator()
            {
                var streamSource = new TestStreamSource()
                {
                    Stream = new MemoryStream()
                };
                var factory = new TestGeneratorBootstrapperFactory(streamSource);
                factory.Create(new GeneratorContext() {GeneratorType = typeof(MockGenerator), WorkingDirectory = new DirectoryInfo("C:\\")}).Execute(new GeneratorExecutor());
                Assert.Equal("Foo", streamSource.Stream.ReadAsString());
            }
        }
    }
}
