using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Tempest.Boot.Conventions.Base
{
    public abstract class ImplementationRegistrationConventionBase : IServiceConfigurationConvention
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