using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Tempest.Boot.Configuration;
using Tempest.Boot.Conventions.Base;

namespace Tempest.Boot.Conventions.Defaults
{
    public class TempestConfigurationRegistrationConvention : AbstractConfigurationRegistrationConvention
    {
        protected override void ModifyBuilder(ConfigurationBuilder builder)
        {
            builder.AddEnvironmentVariables();
            builder.AddJsonFile($"Tempest.config.json", true);
        }

        protected override void ModifyServices(IServiceCollection services, IConfigurationRoot conf)
        {
            services.AddOptions();
            services.Configure<TempestConfiguration>(conf.GetSection("Tempest"));
            services.AddSingleton(p => p.GetService<IOptions<TempestConfiguration>>().Value);
        }
    }
}