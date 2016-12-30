using System.Collections.Generic;
using System.Reflection;
using Tempest.Boot.Conventions.Base;
using Tempest.Boot.Runner.Impl;
using Tempest.Core.Scaffolding;

namespace Tempest.Boot.Conventions
{
    public class TempestAssemblyImplementationRegistrationConvention : ImplementationRegistrationConventionBase
    {
        protected override IEnumerable<Assembly> IncludedAssemblies()
        {
            return GetDefaultAssemblies();
        }

        protected virtual IEnumerable<Assembly> GetDefaultAssemblies()
        {
            yield return typeof(TempestRunner).GetTypeInfo().Assembly;
            yield return typeof(IScaffolder).GetTypeInfo().Assembly;
        }
    }
}