using System;
using System.Collections.Generic;
using System.Linq;
using Tempest.Core.Configuration.Options;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Configuration.Options.Defaults;
using Tempest.Core.Options.Impl;

namespace Tempest.Core.Options.Rendering.Renderers
{
    public class ListOptionRenderer : OptionRendererBase
    {
        public ListOptionRenderer(ConfigurationOptionBase associatedOption) : base(associatedOption)
        {
        }

        protected virtual string RenderListOptionCore(ListConfigurationOption configurationOption, RenderContext context)
        {
            var optionChoices = configurationOption.Choices as IList<OptionChoice> ??
                                configurationOption.Choices.ToList();

            Console.WriteLine("Here are the options:\n");
            return RenderMenu(optionChoices, context);
        }

        protected virtual string RenderMenu(IList<OptionChoice> optionChoices, RenderContext context)
        {
            Console.CursorVisible = false;
            ConsoleKey? key = null;
            var index = 0;

            while (key != ConsoleKey.Enter)
            {
                if (key != null)
                {
                    Console.SetCursorPosition(0, Console.CursorTop - optionChoices.Count);
                }
                RenderMenu(optionChoices, optionChoices[index], context);

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                {
                    index = Math.Max(0, index-1);
                }
                if (key == ConsoleKey.DownArrow)
                {
                    index = Math.Min(optionChoices.Count -1, index+1);
                }
            }
            Console.CursorVisible = true;
            return optionChoices[index].Id;
        }

        protected virtual void RenderMenu(IList<OptionChoice> optionChoices, OptionChoice currentlySelectedOption, RenderContext context)
        {
            foreach (var option in optionChoices)
            {
                if (currentlySelectedOption == option)
                {
                    Console.ForegroundColor = context.ColorFor(ColorType.SpecialTextHighlightForeground);
                    Console.BackgroundColor = context.ColorFor(ColorType.SpecialTextHighlightBackground);
                    Console.Write("> ");
                }
                else
                {
                    Console.ForegroundColor = context.ColorFor(ColorType.SpecialTextForeground);
                    Console.BackgroundColor = context.ColorFor(ColorType.SpecialTextBackground);
                    Console.Write("  ");
                }

                Console.Write($"{option.Title} [{option.Id}]");
                Console.ForegroundColor = context.ColorFor(ColorType.NormalTextForeground);
                Console.BackgroundColor = context.ColorFor(ColorType.NormalTextBackground);
                Console.Write("\n");
            }
        }

        protected override string RenderOptionCore(RenderContext context)
        {
            return RenderListOptionCore((ListConfigurationOption) AssociatedOption, context);
        }
    }
}