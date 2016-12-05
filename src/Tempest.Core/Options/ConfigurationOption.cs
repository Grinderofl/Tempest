using System;

namespace Tempest.Core.Options
{
    public abstract class ConfigurationOption<TOption> : ConfigurationOption
        where TOption : ConfigurationOption<TOption>
    {
        protected ConfigurationOption(Action<string> resultingAction, string title) : base(resultingAction, title)
        {
        }

        public virtual TOption When(Func<bool> showOnlyWhen) => (TOption) RenderWhen(showOnlyWhen);
    }

    public abstract class ConfigurationOption : RenderableOptionBase
    {
        protected ConfigurationOption(Action<string> resultingAction, string title) : base(title)
        {
            ResultingAction = resultingAction;
        }

        protected Action<string> ResultingAction { get; }
        public virtual void ActOn(string choice) => ResultingAction?.Invoke(choice);
        public virtual bool CanActUpon(string choice) => ResultingAction != null;
    }
}