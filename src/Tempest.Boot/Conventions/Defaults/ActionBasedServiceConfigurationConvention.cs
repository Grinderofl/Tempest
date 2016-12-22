using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Tempest.Boot.Conventions.Defaults
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
