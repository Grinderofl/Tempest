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
            //var itemIndex = 0;
            var optionChoices = configurationOption.Choices as IList<OptionChoice> ??
                                configurationOption.Choices.ToList();

            Console.WriteLine("Here are the options");
            return RenderMenu(optionChoices);



//            foreach (var choice in optionChoices)
//            {
//                itemIndex++;
//                Console.WriteLine($"{itemIndex}) [{choice.Id}] {choice.Title}");
//            }
//
//            int? val = null;
//
//            while (val == null)
//            {
//                var key = Console.ReadKey();
//                try
//                {
//                    val = int.Parse(key.KeyChar.ToString());
//                }
//                catch (IndexOutOfRangeException)
//                {
//                    // Just catch if wrong key
//                }
//            }
//
//            var foundChoice = optionChoices.ElementAt(val.Value - 1);
//
//            return foundChoice.Id;
        }

        protected virtual string RenderMenu(IList<OptionChoice> optionChoices)
        {
            var key = ConsoleKey.A;
            var index = 0;
            while (key != ConsoleKey.Enter)
            {
                RenderMenu(optionChoices, optionChoices[index]);
                key = Console.ReadKey().Key;
                if (key == ConsoleKey.UpArrow)
                {
                    index = Math.Max(0, index--);
                }
                if (key == ConsoleKey.DownArrow)
                {
                    index = Math.Min(optionChoices.Count, index++);
                }
            }
            return optionChoices[index].Id;
        }

        protected virtual void RenderMenu(IList<OptionChoice> optionChoices, OptionChoice currentlySelectedOption)
        {
            var cursorTop = Console.CursorTop;
            var cursorLeft = Console.CursorLeft;

            foreach (var option in optionChoices)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (currentlySelectedOption == option)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("> ");
                }

                Console.Write($"{option.Title}\n");
            }

        }

        protected override string RenderOptionCore()
        {
            return RenderListOptionCore((ListConfigurationOption) AssociatedOption);
        }
    }
}