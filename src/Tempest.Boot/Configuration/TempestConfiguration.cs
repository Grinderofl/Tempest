using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempest.Boot.Configuration
{
    public class TempestConfiguration
    {
        public bool AutoInstallGenerators { get; set; }
        public IList<string> AdditionalPaths { get; set; } = new List<string>();
    }
}
