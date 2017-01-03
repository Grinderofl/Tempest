using System;
using System.Collections.Generic;
using System.Linq;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Options.Rendering;
using Tempest.Core.Options.Rendering.Renderers;

namespace Tempest.Core.Configuration.Options.Defaults
{
    public class CheckConfigurationOption : ConfigurationOption<CheckConfigurationOption>
    {
        protected IList<OptionChoice> OptionChoices = new List<OptionChoice>();

        public CheckConfigurationOption(string title) : base(s => { }, title)
        {
        }

        public CheckConfigurationOption(Func<string> titleAction) : base(titleAction)
        {
        }
        protected override OptionRendererBase Renderer => new CheckOptionRenderer(this);
        public IEnumerable<OptionChoice> Choices => OptionChoices;
        public CheckConfigurationOption Choice(string optionText, string id, Action action)
        {
            OptionChoices.Add(new OptionChoice(optionText, id, action));
            return this;
        }

        protected override void ActOnCore(string choice)
        {
            var option = FindOptionWithChoice(choice);
            foreach (var optionChoice in option)
            {
                optionChoice.Action?.Invoke();
            }
        }

        protected virtual IEnumerable<OptionChoice> FindOptionWithChoice(string choice)
        {
            // Wow, .net team, wow... .Split("", StringSplitOptions.RemoveEmptyEntries) does not exist...
            // Just... bravo. Lets force people to new more

            var choices = choice.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
            return OptionChoices.Where(x => choices.Contains(x.Id));
        }

        protected override bool CanActUponCore(string choice)
            => (FindOptionWithChoice(choice).Any());
    }
}