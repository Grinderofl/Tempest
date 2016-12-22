using System.Collections.Generic;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Options;

namespace Tempest.Core.Configuration.Options
{
    public class OptionsConfiguration
    {
        public IList<ConfigurationOption> Options { get; set; }
        public IList<string> Values { get; set; } = new List<string>();
    }
}