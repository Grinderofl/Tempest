using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tempest.Arguments.Impl
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
                    commandArgs.Add(argument.Replace("\"", ""));
                    // Allow insertion of a single '-' argument
                    // and its single parameter
                    if (!argument.StartsWith("-"))
                        justAddedCommandArgument = true;
                }
            }

            if (semanticArgs.Any())
            {
                commandArgs.Add("-g");
                commandArgs.Add(semanticArgs.First());
            }

            if (semanticArgs.Count > 1)
            {
                commandArgs.Add("-p");
                commandArgs.Add($"{string.Join(" ", semanticArgs.Skip(1))}");
            }


            return commandArgs.ToArray();
        }
    }
}
