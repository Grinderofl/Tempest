using System;
using Tempest.Core.Configuration.Options.Base;

namespace Tempest.Core.Configuration.Options.Rendering
{
    public abstract class OptionRendererBase
    {
        protected OptionRendererBase(ConfigurationOptionBase associatedOption)
        {
            AssociatedOption = associatedOption;
        }

        protected ConfigurationOptionBase AssociatedOption { get; }

        /// <summary>
        ///     Render the option. Should have a RenderContext to output to - an abstraction around console possibly? at some point
        /// </summary>
        /// <returns></returns>
        public virtual string Render()
        {
            Console.WriteLine($"\n{AssociatedOption.Title}\n");
            return RenderOptionCore();
        }

        protected abstract string RenderOptionCore();
    }
}