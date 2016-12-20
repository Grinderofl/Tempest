using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot.Strapper;
using Tempest.Core;
using Tempest.Core.Scaffolding;

namespace Tempest.Boot.Runner.Impl
{
    public class RegisterOperationsConvention : IServiceConfigurationConvention
    {
        private readonly ScaffoldOperationConfiguration _configuration;
        private readonly GeneratorContext _context;

        public RegisterOperationsConvention(ScaffoldOperationConfiguration configuration, GeneratorContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public void Configure(IServiceCollection services)
        {
            services.AddSingleton(_configuration).AddSingleton(_context);
        }
    }
}