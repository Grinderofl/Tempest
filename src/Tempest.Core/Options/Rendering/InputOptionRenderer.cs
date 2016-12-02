using System;

namespace Tempest.Core.Options.Rendering
{
    public class InputOptionRenderer : OptionRenderer
    {
        public InputOptionRenderer(ConfigurationOption configurationOption) : base(configurationOption)
        {
        }

        public override string Render()
        {
            Console.WriteLine(ConfigurationOption.Title);
            var answer = Console.ReadLine();
            return answer;
        }
    }
}