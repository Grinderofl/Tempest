using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Tempest.Boot.Strapper;
using Tempest.Core;

namespace Tempest.Boot.Runner.Impl
{
    public class GeneratorRegistrationConvention : IServiceConfigurationConvention
    {
        private readonly Type _generatorType;

        public GeneratorRegistrationConvention(Type generatorType)
        {
            _generatorType = generatorType;
        }


        public void Configure(IServiceCollection services)
        {
            services.AddTransient(typeof(IExecutableGenerator), _generatorType);
            var conventions = FindConventions(_generatorType.GetTypeInfo().Assembly);
            foreach (var serviceConfigurationConvention in conventions)
            {
                serviceConfigurationConvention.Configure(services);
            }
        }

        private IEnumerable<IServiceConfigurationConvention> FindConventions(Assembly assembly)
        {
            var services = new ServiceCollection();
            services.Scan(
                s =>
                    s.FromAssemblies(assembly)
                        .AddClasses(c => c.AssignableTo<IServiceConfigurationConvention>())
                        .As<IServiceConfigurationConvention>());
            var provider = services.BuildServiceProvider();
            return provider.GetServices<IServiceConfigurationConvention>();
        }
    }
}