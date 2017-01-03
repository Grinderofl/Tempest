using System;
using Tempest.Core.Configuration.Options.Defaults;
using Tempest.Core.Options.Impl;

namespace Tempest.Core.Options.Rendering.Renderers
{
    public class InputOptionRenderer : OptionRendererBase
    {
        public InputOptionRenderer(InputConfigurationOption associatedOption) : base(associatedOption)
        {
        }

        protected override string RenderOptionCore(RenderContext renderContext)
        {
            var answer = Console.ReadLine();
            return answer;
        }
    }
}