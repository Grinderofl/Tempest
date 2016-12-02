using System;
using System.Collections.Generic;

namespace Tempest.Core.Configuration
{
    public class OptionExecutor
    {

        public virtual void Execute(IEnumerable<OptionItem> options)
        {
            List<string> results = new List<string>();
            foreach (var item in options)
            {
                if (item.ShouldRender(results))
                    results.Add(item.Renderer.Render(item));
            }
        }
    }
}