using Microsoft.Extensions.DependencyInjection;

namespace Tempest.Boot.Conventions
{
    public interface IServiceConfigurationConvention
    {
        void Configure(IServiceCollection services);
    }
}