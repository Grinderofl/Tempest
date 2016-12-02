using System;
using System.Collections.Generic;

namespace Tempest.Core.Options
{
    public abstract class ConfigurationOption : OptionItemBase
    {
        protected Action<string> Action { get; set; }

        protected Func<bool> Condition { get; set; }

        public virtual bool ShouldRender(List<string> results)
        {
            if (Condition != null)
                return Condition();

            return true;
        }

        public ConfigurationOption When(Func<bool> func)
        {
            Condition = func;
            return this;
        }

        public virtual void ActOn(string choice)
        {
            Action?.Invoke(choice);
        }
    }
}