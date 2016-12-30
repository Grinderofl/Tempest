using System;
using Microsoft.Extensions.DependencyInjection;

namespace Tempest.Core.Conventions.Defaults
{
    public class ActionBasedServiceConfigurationConvention : IServiceConfigurationConvention
    {
        private readonly Action<IServiceCollection> _action;

        public ActionBasedServiceConfigurationConvention(Action<IServiceCollection> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));
            _action = action;
        }

        public void Configure(IServiceCollection services)
        {
            _action(services);
        }
    }
}
