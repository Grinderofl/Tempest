using System;
using System.Collections.Generic;

namespace Tempest.Boot.Configuration.Impl
{
    // This needs something todo with it
    public class TempestConfigurationService : ITempestConfigurationService
    {
        public IEnumerable<string> GetGeneratorPaths()
        {
            yield break;
        }

        public void AddGeneratorPath(string path)
        {
            throw new NotImplementedException();
        }

        public void RemoveGeneratorPath(string path)
        {
            throw new NotImplementedException();
        }

        public bool ShouldInstallGeneratorsAutomatically()
        {
            return false;
        }
    }
}