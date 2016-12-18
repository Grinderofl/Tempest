using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Tempest.Boot.Conventions
{
    public class RegisterAbstractImplementations : RegisterDefaultImplementations
    {
        private readonly Type _type;

        public RegisterAbstractImplementations(Type type)
        {
            _type = type;
        }

        protected override IEnumerable<Assembly> IncludedAssemblies()
        {
            return base.IncludedAssemblies().Union(new[] {_type.GetTypeInfo().Assembly});
        }
    }
}