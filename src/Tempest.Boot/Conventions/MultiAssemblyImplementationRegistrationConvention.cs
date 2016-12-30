using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tempest.Boot.Conventions
{
    public class MultiAssemblyImplementationRegistrationConvention : TempestAssemblyImplementationRegistrationConvention
    {
        private readonly Assembly[] _assemblies;

        public MultiAssemblyImplementationRegistrationConvention(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        protected override IEnumerable<Assembly> IncludedAssemblies()
        {
            return GetDefaultAssemblies().Union(_assemblies);
        }
    }
}