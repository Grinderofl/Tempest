using System;

namespace Tempest.Core.Options.Rendering
{
    public abstract class OptionRendererBase
    {
        protected RenderableOptionBase AssociatedOption { get; private set; }

        protected OptionRendererBase(RenderableOptionBase associatedOption)
        {
            AssociatedOption = associatedOption;   
        }

        /// <summary>
        /// Render the option. Should have a RenderContext to output to - an abstraction around console possibly? at some point
        /// </summary>
        /// <returns></returns>
        public virtual string Render()
        {
            Console.WriteLine(AssociatedOption.Title);
            return RenderOptionCore();
        }

        protected abstract string RenderOptionCore();
    }
}