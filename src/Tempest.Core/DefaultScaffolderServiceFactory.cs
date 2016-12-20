using Tempest.Core.Options;

namespace Tempest.Core
{
    public class DefaultScaffolderServiceFactory : IScaffolderServiceFactory
    {
        public DefaultScaffolderServiceFactory()
        {
            
        }

        public ScaffolderConfigurer CreateScaffolder()
        {
            
        }

        public OptionConfigurer CreateOptions()
        {
            return new OptionConfigurer();
        }

        public OptionExecutor CreateExecutor()
        {
            return new OptionExecutor();
        }
    }
}