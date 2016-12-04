using Microsoft.Extensions.DependencyInjection;

namespace Tempest.Boot
{
    public interface IServiceConfigurationConvention
    {
        void Configure(IServiceCollection services);
    }
}