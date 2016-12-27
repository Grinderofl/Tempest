using System;
using System.Collections.Generic;
using Tempest.Core.Configuration.Options.Rendering;

namespace Tempest.Core.Configuration.Options.Base
{
    public abstract class ConfigurationOptionBase : IConfigurationOption
    {
        private readonly Func<string> _titleAction;

        protected ConfigurationOptionBase(Func<string> titleAction)
        {
            if (titleAction == null) throw new ArgumentNullException(nameof(titleAction));
            _titleAction = titleAction;
        }

        protected abstract OptionRendererBase Renderer { get; }

        public string Title => _titleAction();

        string IConfigurationOption.Render()
        {
            if (Renderer == null)
                throw new NullReferenceException($"The Option Renderer for Option {Title} is null");
            return Renderer.Render();
        }

        bool IConfigurationOption.ShouldRender(List<string> results) => ShouldRenderCore(results);
        void IConfigurationOption.ActOn(string choice) => ActOnCore(choice);
        bool IConfigurationOption.CanActUpon(string choice) => CanActUponCore(choice);

        protected abstract bool ShouldRenderCore(List<string> results);
        protected abstract void ActOnCore(string choice);
        protected abstract bool CanActUponCore(string choice);
    }
}