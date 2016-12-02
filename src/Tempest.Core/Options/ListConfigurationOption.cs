using System;
using System.Collections.Generic;
using System.Linq;
using Tempest.Core.Options.Rendering;

namespace Tempest.Core.Options
{
    public class ListConfigurationOption : ConfigurationOption
    {

        public ListConfigurationOption(string optionTitle)
        {
            Title = optionTitle;
        }

        protected IList<OptionChoice> OptionChoices = new List<OptionChoice>();
        public IEnumerable<OptionChoice> Choices => OptionChoices;
        public ListConfigurationOption(string optionTitle, Action<string> action) : this(optionTitle)
        {
            Action = action;
        }

        public ListConfigurationOption Choice(string optionText, string id, Action action)
        {
            OptionChoices.Add(new OptionChoice(optionText, id, action));
            return this;
        }

        public ListConfigurationOption Choice(string optionText, string id)
        {
            OptionChoices.Add(new OptionChoice(optionText, id));
            return this;
        }

        public override void ActOn(string choice)
        {
            var option = OptionChoices.FirstOrDefault(x => x.Id == choice);
            option?.Action?.Invoke();
            base.ActOn(choice);
        }

        protected override OptionRenderer Renderer => new ListOptionRenderer(this);
    }
}