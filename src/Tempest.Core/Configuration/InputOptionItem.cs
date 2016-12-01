using System;

namespace Tempest.Core.Configuration
{
    public class InputOptionItem : OptionItem
    {
        public InputOptionItem(string optionTitle, Action<string> action)
        {
            Title = optionTitle;
            Action = action;
        }
    }
}