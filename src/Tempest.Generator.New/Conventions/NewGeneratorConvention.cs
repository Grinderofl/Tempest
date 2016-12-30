using Microsoft.Extensions.DependencyInjection;
using Tempest.Core.Conventions;

namespace Tempest.Generator.New.Conventions
{
    public class NewGeneratorConvention : IServiceConfigurationConvention
    {
        public void Configure(IServiceCollection services)
        {
            services.AddSingleton<NewGeneratorOptions>();
        }
    }
}