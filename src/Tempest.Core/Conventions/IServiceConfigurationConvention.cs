using Microsoft.Extensions.DependencyInjection;

namespace Tempest.Core.Conventions
{
    public interface IServiceConfigurationConvention
    {
        void Configure(IServiceCollection services);
    }
}