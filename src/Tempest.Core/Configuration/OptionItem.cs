using System;
using System.Collections.Generic;

namespace Tempest.Core.Configuration
{
    public class OptionItem : OptionItemBase
    {
        public Action<string> Action { get; protected set; }

        public Func<bool> Condition { get; protected set; }

        public Func<IList<string>, bool> ResultCondition { get; protected set; }

        public bool ShouldRender(List<string> results)
        {
            if (Condition != null)
                return Condition();

            if (ResultCondition != null)
                return ResultCondition(results);

            return true;
        }

        public OptionItem When(Func<bool> func)
        {
            Condition = func;
            return this;
        }
    }
}