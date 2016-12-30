using Tempest.Core.Operations;
using Tempest.Core.Scaffolding.Impl;

namespace Tempest.Core.Scaffolding
{
    public abstract class AbstractScaffolderConfigurer : IScaffoldConfigurer
    {
        public abstract int Order { get; }
        public void ConfigureOperations(ScaffoldOperationConfiguration configuration)
        {
            var scaffolder = new ScaffolderConfigurer();
            ConfigureScaffolder((IScaffoldBuilder) scaffolder);
            ((IScaffoldConfigurer)scaffolder).ConfigureOperations(configuration);
        }

        protected abstract void ConfigureScaffolder(IScaffoldBuilder scaffolder);
    }
}