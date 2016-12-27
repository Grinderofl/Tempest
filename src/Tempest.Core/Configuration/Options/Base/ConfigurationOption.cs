using System;
using System.Collections.Generic;

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

    public abstract class ConfigurationOption : ConfigurationOptionBase
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

        protected Func<bool> RenderCondition { get; private set; }

        public virtual ConfigurationOptionBase RenderWhen(Func<bool> func)
        {
            RenderCondition = func;
            return this;
        }

        protected Action<string> ResultingAction { get; }

        protected override void ActOnCore(string choice) => ResultingAction?.Invoke(choice);
        protected override bool CanActUponCore(string choice) => ResultingAction != null;
        protected override bool ShouldRenderCore(List<string> results) => (RenderCondition == null) || RenderCondition();

    }
}