using System;

namespace Tempest.Core.Configuration
{
    public class ListOptionItem : OptionItem
    {

        public ListOptionItem(string optionTitle)
        {
            Title = optionTitle;
        }

        public ListOptionItem(string optionTitle, Action<string> action) : this(optionTitle)
        {
            Action = action;
        }

        public ListOptionItem Choice(string optionText, string id, Action action)
        {
            OptionChoices.Add(new OptionChoice(optionText, id, action));
            return this;
        }

        public ListOptionItem Choice(string optionText, string id)
        {
            OptionChoices.Add(new OptionChoice(optionText, id));
            return this;
        }

        public override OptionTypeRenderer Renderer => new ListOptionRenderer();
    }
}