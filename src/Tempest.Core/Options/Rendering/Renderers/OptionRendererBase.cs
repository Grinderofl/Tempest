using System;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Options.Impl;

namespace Tempest.Core.Options.Rendering.Renderers
{
    public abstract class OptionRendererBase
    {
        protected OptionRendererBase(ConfigurationOptionBase associatedOption)
        {
            AssociatedOption = associatedOption;
        }

        protected ConfigurationOptionBase AssociatedOption { get; }

        /// <summary>
        ///     Render the option. Should have a RenderContext to output to an abstraction around console possibly? at some point
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual string Render(RenderContext context)
        {
            Console.WriteLine($"\n{AssociatedOption.Title}\n");
            return RenderOptionCore(context);
        }

        protected abstract string RenderOptionCore(RenderContext context);
    }
}