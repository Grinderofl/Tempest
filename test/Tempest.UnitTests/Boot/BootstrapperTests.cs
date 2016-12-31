using System;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Strappers.Defaults;
using Tempest.Boot.Strappers.Execution;
using Tempest.Core.Conventions.Defaults;
using Xunit;

namespace Tempest.UnitTests.Boot
{
    public class BootstrapperTests
    {
        private interface IMockService
        {
            string Foo { get; set; }
        }

        private class MockBootstrapperExecutor : IBootstrapperExecutor
        {
            public int Execute(IServiceProvider provider)
            {
                provider.GetService<IMockService>().Foo = "Bar";
                return 0;
            }
        }

        public class ExecutingBasicService
        {

            private class MockService : IMockService
            {
                public string Foo { get; set; }
            }

            [Fact]
            public void executes_service()
            {
                var mockService = new MockService();
                var strapper = TempestBootstrapper.Create();
                strapper.RegisterConvention(new ActionBasedServiceConfigurationConvention(s => s.AddSingleton<IMockService>(mockService)));
                var mockExecutor = new MockBootstrapperExecutor();
                strapper.Execute(mockExecutor);

                Assert.Equal("Bar", mockService.Foo);
            }
        }

        
    }
}
