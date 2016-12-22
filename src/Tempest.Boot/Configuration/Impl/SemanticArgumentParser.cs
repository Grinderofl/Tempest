using System.Collections.Generic;
using System.Linq;

namespace Tempest.Boot.Configuration.Impl
{
    public class SemanticArgumentParser : IArgumentParser
    {
        public string[] ParseArguments(string[] args)
        {
            var isSemanticContext = true;
            var justAddedCommandArgument = false;

            var semanticArgs = new List<string>();
            var commandArgs = new List<string>();

            foreach (var argument in args)
            {
                if (argument.StartsWith("-"))
                    isSemanticContext = false;
                else if (justAddedCommandArgument)
                    isSemanticContext = true;


                if (isSemanticContext)
                {
                    semanticArgs.Add(argument);
                    justAddedCommandArgument = false;
                }
                else
                {
                    commandArgs.Add(argument);
                    justAddedCommandArgument = !argument.StartsWith("-");
                }
            }

            if (semanticArgs.Any())
            {
                commandArgs.Add("-g");
                commandArgs.Add(semanticArgs.First());

                if (semanticArgs.Count > 1)
                {
                    commandArgs.Add("-p");
                    commandArgs.Add($"{string.Join(" ", semanticArgs.Skip(1))}");
                }
            }


            return commandArgs.ToArray();
        }
    }
}