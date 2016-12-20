using System.Collections.Generic;
using Tempest.Core.Options;

namespace Tempest.Core
{
    public class OptionsConfiguration
    {
        public IList<ConfigurationOption> Options { get; set; }
        public IList<string> Values { get; set; } = new List<string>();
    }
}