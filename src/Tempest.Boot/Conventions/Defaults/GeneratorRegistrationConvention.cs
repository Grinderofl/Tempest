using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scrutor;
using Tempest.Core;
using Tempest.Core.Generator;
using Tempest.Core.Operations;

namespace Tempest.Boot.Conventions.Defaults
{
    /// <summary>
    /// Registers services required to execute a generator,
    /// including all IServiceConfigurationConvention implementations from 
    /// both generator's and Tempest's assemblies
    /// </summary>
    public class GeneratorRegistrationConvention : IServiceConfigurationConvention
    {
        private readonly ScaffoldOperationConfiguration _configuration;
        private readonly GeneratorContext _context;
        private readonly Assembly[] _additionalAssemblies;

        public GeneratorRegistrationConvention(ScaffoldOperationConfiguration configuration, GeneratorContext context, params Assembly[] additionalAssemblies)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            if (context == null) throw new ArgumentNullException(nameof(context));
            _configuration = configuration;
            _context = context;
            _additionalAssemblies = additionalAssemblies;
        }


        public virtual void Configure(IServiceCollection services)
        {
            RegisterGeneratorServices(services);
            RegisterAndExecuteConventions(services);

        }

        private void RegisterAndExecuteConventions(IServiceCollection services)
        {
            var conventions = FindConventions(FindAssemblies());
            foreach (var serviceConfigurationConvention in conventions)
                serviceConfigurationConvention.Configure(services);
        }

        private void RegisterGeneratorServices(IServiceCollection services)
        {
            services.TryAddScoped(typeof(IExecutableGenerator), _context.GeneratorType);
            services.TryAddSingleton(_configuration);
            services.TryAddSingleton(_context);
        }

        protected virtual IEnumerable<Assembly> FindAssemblies()
        {
            return _additionalAssemblies.Union(new[] {_context.GeneratorType.GetTypeInfo().Assembly});
        }

        protected virtual IEnumerable<IServiceConfigurationConvention> FindConventions(IEnumerable<Assembly> assemblies)
        {
            var services = new ServiceCollection();
            
            services.Scan(
                s =>
                    s.FromAssemblies(assemblies)
                        .AddClasses(c => c.AssignableTo<IServiceConfigurationConvention>())
                        .As<IServiceConfigurationConvention>());
            var provider = services.BuildServiceProvider();
            return provider.GetServices<IServiceConfigurationConvention>();
        }
    }
}