using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Conventions.Defaults;
using Tempest.Boot.Strappers.Defaults;
using Tempest.Boot.Strappers.Execution;
using Xunit;

namespace Tempest.CoreTests.Boot
{
    public class BootstrapperTests
    {
        public class ExecutingTempestBootstrapper
        {
            private class MockBootstrapperExecutor : IBootstrapperExecutor
            {
                public int Execute(IServiceProvider provider)
                {
                    provider.GetService<MockService>().Foo = "Bar";
                    return 0;
                }
            }

            private class MockService
            {
                public string Foo { get; set; }
            }

            [Fact]
            public void executes_service()
            {
                var mockService = new MockService();
                var strapper = TempestBootstrapper.Create();
                strapper.RegisterConvention(new ActionBasedServiceConfigurationConvention(s => s.AddSingleton(mockService)));
                var mockExecutor = new MockBootstrapperExecutor();
                strapper.Execute(mockExecutor);

                Assert.Equal("Bar", mockService.Foo);
            }
        }
    }
}
