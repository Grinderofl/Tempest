using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Tempest.Runner.Impl;

namespace Tempest.Boot.Conventions
{
    public class RegisterImplementations : IServiceConfigurationConvention
    {
        public void Configure(IServiceCollection services)
        {
            services.Scan(
                s =>
                    s.FromAssemblyOf<TempestRunner>()
                        .AddClasses(c => c.Where(t => t.Namespace?.EndsWith("Impl") == true))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());
        }
    }
}