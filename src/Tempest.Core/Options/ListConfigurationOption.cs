using System;
using System.Collections.Generic;
using System.Linq;
using Tempest.Core.Options.Rendering;

namespace Tempest.Core.Options
{
    public class ListConfigurationOption : ConfigurationOption<ListConfigurationOption>
    {

        public ListConfigurationOption(string optionTitle) : this(optionTitle, null)
        {
        }
        public ListConfigurationOption(string optionTitle, Action<string> resultingAction) : base(resultingAction, optionTitle)
        {
        }

        protected IList<OptionChoice> OptionChoices = new List<OptionChoice>();
        public IEnumerable<OptionChoice> Choices => OptionChoices;
        
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

        protected override OptionRendererBase Renderer => new ListOptionRenderer(this);

        public override void ActOn(string choice)
        {
            OptionChoice option = FindOptionWithChoice(choice);
            option?.Action?.Invoke();
            base.ActOn(choice);
        }

        protected virtual OptionChoice FindOptionWithChoice(string choice)
            => OptionChoices.FirstOrDefault(x => x.Id == choice);

        public override bool CanActUpon(string choice)
            => FindOptionWithChoice(choice) != null || base.CanActUpon(choice);

        
    }
}