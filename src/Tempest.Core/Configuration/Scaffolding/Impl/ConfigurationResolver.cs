using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Core.Generator;
using Tempest.Core.Operations;
using Tempest.Core.Scaffolding;

namespace Tempest.Core.Configuration.Scaffolding.Impl
{
    public class ConfigurationResolver : IConfigurationResolver
    {
        private readonly IExecutableGenerator _generator;
        private readonly IOptionRunner _optionRunner;
        private readonly GeneratorContext _generatorContext;
        private readonly ScaffoldOperationConfiguration _configuration;
        private readonly IEnumerable<IScaffoldConfigurer> _configurers;

        public ConfigurationResolver(IExecutableGenerator generator, IOptionRunner optionRunner, GeneratorContext generatorContext, ScaffoldOperationConfiguration configuration, IEnumerable<IScaffoldConfigurer> configurers)
        {
            if (generator == null) throw new ArgumentNullException(nameof(generator));
            if (generatorContext == null) throw new ArgumentNullException(nameof(generatorContext));
            _generator = generator;
            _optionRunner = optionRunner;
            _generatorContext = generatorContext;
            _configuration = configuration;
            _configurers = configurers ?? Enumerable.Empty<IScaffoldConfigurer>();
        }

        public ScaffoldOperationConfiguration Resolve()
        {
            _optionRunner.Run( _generatorContext);

            var configuration = _generator.ConfigureOperations(_configuration);
            foreach (var configurer in _configurers.OrderBy(x => x.Order))
                configurer.ConfigureOperations(configuration);

            return configuration;
        }
    }
}
