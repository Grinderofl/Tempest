//using System.Linq;

//namespace Tempest.Arguments
//{
//    public class RunnerArgumentFactory
//    {
//        public RunnerArguments Create(TempestRunnerArguments args)
//        {
//            if (args[0] != null)
//            {
//                var generatorArgs = args.Skip(1).ToArray();
//                return new RunnerArguments()
//                {
//                    Generator = args[0],
//                    GeneratorArguments = generatorArgs
//                };
//            }
//            return new RunnerArguments();
//        }
//    }
//}