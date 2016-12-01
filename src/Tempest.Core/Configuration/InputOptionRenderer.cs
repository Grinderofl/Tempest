using System;

namespace Tempest.Core.Configuration
{
    public class InputOptionRenderer : OptionTypeRenderer
    {
        public override string Render(OptionItem optionItem)
        {
            Console.WriteLine(optionItem.Title);
            var answer = Console.ReadLine();
            optionItem.Action(answer);
            return answer;
        }
    }
}