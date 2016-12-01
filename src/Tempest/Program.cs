using System.Collections.Generic;
using System.Threading;

namespace Tempest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var arguments = new RunnerArgumentFactory().Create(args);
            var directoryFinder = new DirectoryFinder();
            var runner = new TempestRunner(arguments);

        }
    }
}

