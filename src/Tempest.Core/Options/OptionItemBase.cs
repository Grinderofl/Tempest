using System;
using Tempest.Core.Options.Rendering;

namespace Tempest.Core.Options
{
    public abstract class OptionItemBase
    {
        public string Title { get; protected set; }
        protected abstract OptionRenderer Renderer { get; }

        public virtual string Render()
        {
            if(Renderer == null)
                throw new NullReferenceException($"The Option Renderer for Option {Title} is null");
            return Renderer.Render();
        }
    }
}