using System;
using System.Collections.Generic;
using System.Linq;
using Tempest.Core.Configuration.Options;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Configuration.Options.Defaults;

namespace Tempest.Core.Options.Rendering
{
    public class CheckOptionRenderer : OptionRendererBase
    {
        public CheckOptionRenderer(ConfigurationOptionBase associatedOption) : base(associatedOption)
        {
        }



        protected override string RenderOptionCore()
        {
            return RenderCheckOptionsCore((CheckConfigurationOption) AssociatedOption);
        }

        private string RenderCheckOptionsCore(CheckConfigurationOption associatedOption)
        {
            var optionChoices = associatedOption.Choices as IList<OptionChoice> ?? associatedOption.Choices.ToList();

            Console.WriteLine("Here are the options:");
            RenderMenu(optionChoices);
            var responses = string.Join(" ", optionChoices.Where(x => x.Selected).Select(x => x.Id));
            return responses;
        }

        protected virtual void RenderMenu(IList<OptionChoice> optionChoices)
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
                RenderMenuOptions(optionChoices, optionChoices[index]);
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow)
                    index = Math.Max(0, index - 1);
                if (key == ConsoleKey.DownArrow)
                    index = Math.Min(optionChoices.Count - 1, index + 1);
                if (key == ConsoleKey.Spacebar)
                    optionChoices[index].Selected = !optionChoices[index].Selected;
            }
        }

        protected virtual void RenderMenuOptions(IList<OptionChoice> optionChoices, OptionChoice currentlySelected)
        {
            var origForeColor = Console.ForegroundColor;
            var origBackColor = Console.BackgroundColor;

            foreach (var option in optionChoices)
            {
                if (currentlySelected == option)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }

                Console.Write($"[{(option.Selected ? "x" : " ")}] {option.Title} [{option.Id}]");
                Console.ForegroundColor = origForeColor;
                Console.BackgroundColor = origBackColor;
                Console.Write("\n");
            }
        }
    }
}