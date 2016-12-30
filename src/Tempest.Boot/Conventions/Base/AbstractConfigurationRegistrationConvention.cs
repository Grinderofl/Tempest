using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Core.Conventions;

namespace Tempest.Boot.Conventions.Base
{
    public abstract class AbstractConfigurationRegistrationConvention : IServiceConfigurationConvention
    {
        protected abstract void ModifyBuilder(ConfigurationBuilder builder);

        public void Configure(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder();
            ModifyBuilder(builder);
            var conf = builder.Build();
            ModifyConfiguration(conf);
            services.AddSingleton(conf);
            ModifyServices(services, conf);
        }

        protected abstract void ModifyServices(IServiceCollection services, IConfigurationRoot conf);

        protected virtual void ModifyConfiguration(IConfigurationRoot conf)
        {
            
        }
    }
}