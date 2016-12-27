using System.Collections.Generic;

namespace Tempest.Core.Configuration.Options
{
    public interface IConfigurationOption
    {
        bool CanActUpon(string choice);
        void ActOn(string choice);
        bool ShouldRender(List<string> results);
        string Render();
    }
}