using System.Collections.Generic;

namespace Tempest.Configuration
{
    public interface ITempestConfigurationService
    {
        bool ShouldInstallGeneratorsAutomatically();
        IEnumerable<string> GetGeneratorPaths();
        void AddGeneratorPath(string path);
        void RemoveGeneratorPath(string path);
    }
}