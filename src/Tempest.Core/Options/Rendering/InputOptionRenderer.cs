using System;

namespace Tempest.Core.Options.Rendering
{
    public class InputOptionRenderer : OptionRendererBase
    {
        public InputOptionRenderer(InputConfigurationOption associatedOption) : base(associatedOption)
        {
        }

        protected override string RenderOptionCore()
        {
            var answer = Console.ReadLine();
            return answer;
        }
    }
}