using System;
using System.Collections.Generic;

namespace Tempest.Core.Options
{
    public abstract class ConfigurationOption : RenderableOptionBase
    {
        protected Action<string> Action { get; set; }

        protected Func<bool> Condition { get; set; }

        public virtual bool ShouldRender(List<string> results) => Condition == null || Condition();

        public ConfigurationOption When(Func<bool> func)
        {
            Condition = func;
            return this;
        }

        public virtual void ActOn(string choice) => Action?.Invoke(choice);
        public virtual bool CanActUpon(string choice) => Action != null;
    }
}