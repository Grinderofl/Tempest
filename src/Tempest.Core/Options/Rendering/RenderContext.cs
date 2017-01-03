using System;

namespace Tempest.Core.Options.Rendering
{
    public class RenderContext
    {
        public RenderContext(RenderOptions renderOptions)
        {
            RenderOptions = renderOptions;
        }

        public RenderOptions RenderOptions { get; set; }

        public ConsoleColor ColorFor(ColorType type) => RenderOptions.RenderColors[type];
    }
}