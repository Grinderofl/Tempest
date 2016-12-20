using Tempest.Core.Options;

namespace Tempest.Core
{
    public class ScaffolderServiceFactory : IScaffolderServiceFactory
    {
        private readonly ScaffolderConfigurer _scaffoldingConfiguration;
        private readonly OptionConfigurer _optionConfigurer;
        private readonly OptionExecutor _executor;

        public ScaffolderServiceFactory(ScaffolderConfigurer scaffoldingConfiguration, OptionConfigurer optionConfigurer,
            OptionExecutor executor)
        {
            _scaffoldingConfiguration = scaffoldingConfiguration;
            _optionConfigurer = optionConfigurer;
            _executor = executor;
        }

        public ScaffolderConfigurer CreateScaffolder()
        {
            return _scaffoldingConfiguration;
        }

        public OptionConfigurer CreateOptions()
        {
            return _optionConfigurer;
        }

        public OptionExecutor CreateExecutor()
        {
            return _executor;
        }
    }
}