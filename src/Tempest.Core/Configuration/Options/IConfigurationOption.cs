using System.Collections.Generic;
using Tempest.Core.Options.Impl;
using Tempest.Core.Options.Rendering;

namespace Tempest.Core.Configuration.Options
{
    public interface IConfigurationOption
    {
        bool CanActUpon(string choice);
        void ActOn(string choice);
        bool ShouldRender(List<string> results);
        string Render(RenderContext renderContext);
    }
}