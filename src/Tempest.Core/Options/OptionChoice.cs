using System;

namespace Tempest.Core.Options
{
    /// <summary>
    ///     Option choice. Loaded conventionally by Id through arguments.
    /// </summary>
    public class OptionChoice
    {
        public OptionChoice(string title, string id)
        {
            Title = title;
            Id = id;
        }

        public OptionChoice(string title, string id, Action action) : this(title, id)
        {
            Action = action;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public Action Action { get; set; }
    }
}