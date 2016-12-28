using System;
using System.Collections.Generic;
using System.Linq;
using Tempest.Core.Configuration.Options;
using Tempest.Core.Configuration.Options.Base;
using Tempest.Core.Configuration.Options.Defaults;

namespace Tempest.Core.Options.Rendering
{
    public class ListOptionRenderer : OptionRendererBase
    {
        public ListOptionRenderer(ConfigurationOptionBase associatedOption) : base(associatedOption)
        {
        }

        protected virtual string RenderListOptionCore(ListConfigurationOption configurationOption)
        {
            var optionChoices = configurationOption.Choices as IList<OptionChoice> ??
                                configurationOption.Choices.ToList();

            Console.WriteLine("Here are the options");
            return RenderMenu(optionChoices);
        }

        protected virtual string RenderMenu(IList<OptionChoice> optionChoices)
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
                RenderMenu(optionChoices, optionChoices[index]);

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

        protected virtual void RenderMenu(IList<OptionChoice> optionChoices, OptionChoice currentlySelectedOption)
        {
            var origForeColor = Console.ForegroundColor;
            var origBackColor = Console.BackgroundColor;

            var cursorTop = Console.CursorTop;
            var cursorLeft = Console.CursorLeft;
            foreach (var option in optionChoices)
            {
                if (currentlySelectedOption == option)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }

                Console.Write($"{option.Title} [{option.Id}]");
                Console.ForegroundColor = origForeColor;
                Console.BackgroundColor = origBackColor;
                Console.Write("\n");
            }
        }

        protected override string RenderOptionCore()
        {
            return RenderListOptionCore((ListConfigurationOption) AssociatedOption);
        }
    }
}