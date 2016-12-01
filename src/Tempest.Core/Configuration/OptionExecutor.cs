using System;
using System.Collections.Generic;

namespace Tempest.Core.Configuration
{
    public class OptionExecutor
    {

        public virtual void Execute(IEnumerable<OptionItem> options)
        {
            foreach (var option in options)
            {
                Console.WriteLine(option.Title);
            }

        }
    }
}