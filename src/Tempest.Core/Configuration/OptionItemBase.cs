using System.Collections.Generic;

namespace Tempest.Core.Configuration
{
    public abstract class OptionItemBase
    {
        protected IList<OptionChoice> OptionChoices = new List<OptionChoice>();
        public string Title { get; protected set; }
        public OptionType Type { get; protected set; }
        public IEnumerable<OptionChoice> Choices => OptionChoices;
    }
}