using System.Linq;
using Tempest.Core.Generator;
using Tempest.Core.Options;

namespace Tempest.Core.Configuration.Scaffolding.Impl
{
    public class OptionRunner : IOptionRunner
    {
        private readonly IExecutableGenerator _generator;
        private readonly IOptionExecutor _optionExecutor;

        public OptionRunner(IExecutableGenerator generator, IOptionExecutor optionExecutor)
        {
            _generator = generator;
            _optionExecutor = optionExecutor;
        }

        public void Run(GeneratorContext context)
        {
            var options = _generator.CreateOptions();
            _optionExecutor.Execute(options.ToArray(), context.Arguments);
        }
    }
}