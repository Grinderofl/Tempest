using Tempest.Boot;

namespace Tempest
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var strapper = DefaultTempestBootstrapper.Create();
            return strapper.Execute(args);
            //var semanticArguments = SemanticArgumentParser.Parse(args);


            // Should support following arguments:
            // 
            // -i | --install [<PackageName>|<PackageName.zip>]
            //      --update all

            // -u | --uninstall <PackageName>
            // -s | --search <SearchPath>                   Configures the directory to search for generators
            // -a | --add-search <SearchPath>               Adds a default path to search for generators
            // -r | --remove-search <SearchPath>            Removes a default path from being searched for generators
            // -l | --list [Generators|Search]              Lists all generators or search paths
            // -v | --verbosity <VerbosityLevel>            Specifies the verbosity level
            // -p | --para <Parameters>                     Specifies the generator parameters
            // -g | --generator <Name>                      Specifies the generator to use

            // Syntax:
            // tempest <generatorName> -iusarlvpg <command>
            // tempest <generatorName> <Parameters> -iusarlvpg <command>
            // tempest <generatorName> -iusarlvpg <command>

            // tempest -i <command> -u <command> -s <command> <generatorName> <Parameters>
            // tempest <generatorName> <Parameters> -i <command> -u <command> -s <command>
            // tempest -iusarlvpg command
            // tempest -iusarlvpg command <generatorName>
            // tempest -iusarlvpg command <generatorName> <Parameters>

            // What's the pattern here?
            // If we assume that an argument is always in a form "-X" or "--Xxxxx", and always follows by a single word, or something between " and " ... maybe we can extract it like that?


            //return 0;
        }
    }
}