using System;

namespace Tempest.Core.Configuration.Options.Base
{
    public abstract class ConfigurationOption<TOption> : ConfigurationOption
        where TOption : ConfigurationOption<TOption>
    {
        protected ConfigurationOption(Action<string> resultingAction, string title) : base(resultingAction, title)
        {
        }

        protected ConfigurationOption(Func<string> titleAction) : base(titleAction)
        {
        }

        protected ConfigurationOption(Func<string> titleAction, Action<string> resultingAction)
            : base(titleAction, resultingAction)
        {
        }

        public virtual TOption When(Func<bool> showOnlyWhen) => (TOption) RenderWhen(showOnlyWhen);
    }

    public abstract class ConfigurationOption : RenderableOptionBase
    {
        protected ConfigurationOption(Action<string> resultingAction, string title) : this(() => title, resultingAction)
        {
        }

        protected ConfigurationOption(Func<string> titleAction) : this(titleAction, null)
        {
        }

        protected ConfigurationOption(Func<string> titleAction, Action<string> resultingAction) : base(titleAction)
        {
            ResultingAction = resultingAction;
        }

        protected Action<string> ResultingAction { get; }
        public virtual void ActOn(string choice) => ResultingAction?.Invoke(choice);
        public virtual bool CanActUpon(string choice) => ResultingAction != null;
    }
}