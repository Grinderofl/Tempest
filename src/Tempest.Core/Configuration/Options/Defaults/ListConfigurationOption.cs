using System;
using System.Collections.Generic;
using System.Linq;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Options.Rendering;
using Tempest.Core.Options.Rendering.Renderers;

namespace Tempest.Core.Configuration.Options.Defaults
{
    public class ListConfigurationOption : ConfigurationOption<ListConfigurationOption>
    {
        protected IList<OptionChoice> OptionChoices = new List<OptionChoice>();

        public ListConfigurationOption(string optionTitle) : this(optionTitle, null)
        {
        }

        public ListConfigurationOption(string optionTitle, Action<string> resultingAction)
            : base(resultingAction, optionTitle)
        {
        }

        public ListConfigurationOption(Func<string> optionTitle) : base(optionTitle)
        {
        }

        public ListConfigurationOption(Func<string> optionTitle, Action<string> resultingAction) : base(optionTitle, resultingAction)
        {
        }

        public IEnumerable<OptionChoice> Choices => OptionChoices;

        protected override OptionRendererBase Renderer => new ListOptionRenderer(this);

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

        protected override void ActOnCore(string choice)
        {
            var option = FindOptionWithChoice(choice);
            option?.Action?.Invoke();
            base.ActOnCore(choice);
        }

        protected virtual OptionChoice FindOptionWithChoice(string choice)
            => OptionChoices.FirstOrDefault(x => x.Id == choice);

        protected override bool CanActUponCore(string choice)
            => (FindOptionWithChoice(choice) != null) || base.CanActUponCore(choice);
    }
}