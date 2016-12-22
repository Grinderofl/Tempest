using System;
using System.Collections.Generic;
using Tempest.Core.Configuration.Options.Rendering;

namespace Tempest.Core.Configuration.Options.Base
{
    public abstract class RenderableOptionBase
    {
        private readonly Func<string> _titleAction;

        protected RenderableOptionBase(Func<string> titleAction)
        {
            if (titleAction == null) throw new ArgumentNullException(nameof(titleAction));
            _titleAction = titleAction;
        }

        protected abstract OptionRendererBase Renderer { get; }
        protected Func<bool> RenderCondition { get; private set; }
        public string Title => _titleAction();
        public virtual bool ShouldRender(List<string> results) => (RenderCondition == null) || RenderCondition();

        public RenderableOptionBase RenderWhen(Func<bool> func)
        {
            RenderCondition = func;
            return this;
        }

        public virtual string Render()
        {
            if (Renderer == null)
                throw new NullReferenceException($"The Option Renderer for Option {Title} is null");
            return Renderer.Render();
        }
    }
}