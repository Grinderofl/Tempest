using Tempest.Core.Options;

namespace Tempest.Core
{
    public abstract class GenericGenerator<TConfiguration, TOptions> : GeneratorBase
        where TConfiguration : ScaffolderConfigurer, new() where TOptions : OptionConfigurer, new()
    {
        protected GenericGenerator()
            : base(new ScaffolderServiceFactory(new TConfiguration(), new TOptions(), new OptionExecutor()))
        {
        }
    }
}