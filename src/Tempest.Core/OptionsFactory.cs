using System;
using Tempest.Core.Configuration;

namespace Tempest.Core
{
    public class OptionsFactory
    {
        public ListOptionItem List(string optionTitle)
        {
            return new ListOptionItem(optionTitle);
        }

        public ListOptionItem List(string optionTitle, Action<string> action)
        {
            return new ListOptionItem(optionTitle, action);
        }

        public InputOptionItem Input(string optionTitle, Action<string> action)
        {
            return new InputOptionItem(optionTitle, action);
        }
    }
}