using System;
using System.Collections.Generic;
using System.Linq;
using Tempest.Core.Configuration.Options;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Configuration.Options.Defaults;
using Tempest.Core.Options.Impl;

namespace Tempest.Core.Options.Rendering.Renderers
{
    public class CheckOptionRenderer : OptionRendererBase
    {
        public CheckOptionRenderer(ConfigurationOptionBase associatedOption) : base(associatedOption)
        {
        }



        protected override string RenderOptionCore(RenderContext context)
        {
            return RenderCheckOptionsCore((CheckConfigurationOption) AssociatedOption, context);
        }

        private string RenderCheckOptionsCore(CheckConfigurationOption associatedOption, RenderContext context)
        {
            var optionChoices = associatedOption.Choices as IList<OptionChoice> ?? associatedOption.Choices.ToList();

            Console.WriteLine("Here are the options:\n");
            RenderMenu(optionChoices, context);
            var responses = string.Join(" ", optionChoices.Where(x => x.Selected).Select(x => x.Id));
            return responses;
        }

        protected virtual void RenderMenu(IList<OptionChoice> optionChoices, RenderContext context)
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
                RenderMenuOptions(optionChoices, optionChoices[index], context);
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                    index = Math.Max(0, index - 1);
                if (key == ConsoleKey.DownArrow)
                    index = Math.Min(optionChoices.Count - 1, index + 1);
                if (key == ConsoleKey.Spacebar)
                    optionChoices[index].Selected = !optionChoices[index].Selected;
            }
        }

        protected virtual void RenderMenuOptions(IList<OptionChoice> optionChoices, OptionChoice currentlySelected, RenderContext context)
        {
            foreach (var option in optionChoices)
            {
                if (currentlySelected == option)
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

                Console.Write($"[{(option.Selected ? "x" : " ")}] {option.Title} [{option.Id}]");
                Console.ForegroundColor = context.ColorFor(ColorType.NormalTextForeground);
                Console.BackgroundColor = context.ColorFor(ColorType.NormalTextBackground);
                Console.Write("\n");
            }
        }
    }
}