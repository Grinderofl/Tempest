using Tempest.Core.Options;

namespace Tempest.Core
{
    public interface IScaffolderServiceFactory
    {
        ScaffolderConfigurer CreateScaffolder();
        OptionConfigurer CreateOptions();
        OptionExecutor CreateExecutor();

    }
}