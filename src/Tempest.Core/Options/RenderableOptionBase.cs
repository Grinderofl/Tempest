using System;
using System.Collections.Generic;
using Tempest.Core.Options.Rendering;

namespace Tempest.Core.Options
{
    public abstract class RenderableOptionBase
    {
        protected RenderableOptionBase(string title)
        {
            Title = title;
        }
        protected abstract OptionRendererBase Renderer { get; }
        protected Func<bool> RenderCondition { get; private set; }
        public virtual bool ShouldRender(List<string> results) => RenderCondition == null || RenderCondition();
        public string Title { get; }
        public RenderableOptionBase RenderWhen(Func<bool> func)
        {
            RenderCondition = func;
            return this;
        }
        
        public virtual string Render()
        {
            if(Renderer == null)
                throw new NullReferenceException($"The Option Renderer for Option {Title} is null");
            return Renderer.Render();
        }
    }
}