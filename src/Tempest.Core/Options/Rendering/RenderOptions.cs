using System;
using System.Collections.Generic;

namespace Tempest.Core.Options.Rendering
{
    public class RenderOptions
    {
        public Dictionary<ColorType, ConsoleColor> RenderColors { get; } = new Dictionary<ColorType, ConsoleColor>()
        {
            [ColorType.NormalTextForeground] = Console.ForegroundColor,
            [ColorType.NormalTextBackground] = Console.BackgroundColor,
            [ColorType.SpecialTextForeground] = ConsoleColor.White,
            [ColorType.SpecialTextBackground] = Console.BackgroundColor,
            [ColorType.SpecialTextHighlightForeground] = ConsoleColor.Cyan,
            [ColorType.SpecialTextHighlightBackground] = Console.BackgroundColor
        };
    }
}