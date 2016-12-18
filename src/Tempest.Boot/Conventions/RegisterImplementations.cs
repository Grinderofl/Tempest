using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Tempest.Boot.Strapper;

namespace Tempest.Boot.Conventions
{
    public abstract class RegisterImplementations : IServiceConfigurationConvention
    {

        protected abstract IEnumerable<Assembly> IncludedAssemblies();

        public void Configure(IServiceCollection services)
        {
            services.Scan(
                s =>
                    s.FromAssemblies(IncludedAssemblies())
                        .AddClasses(c => c.Where(t => t.Namespace?.EndsWith("Impl") == true))
                        .AsImplementedInterfaces()
                        .WithScopedLifetime());
        }
    }
}