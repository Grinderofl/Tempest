using Microsoft.Extensions.DependencyInjection;

namespace Tempest.Boot.Strapper
{
    public interface IServiceConfigurationConvention
    {
        void Configure(IServiceCollection services);
    }
}