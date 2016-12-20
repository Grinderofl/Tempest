using Tempest.Core.Scaffolding;

namespace Tempest.Core
{
    public abstract class ScaffolderConfigurerBase
    {
        protected ScaffolderConfigurerBase(ScaffoldingConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ScaffoldingConfiguration Configuration { get; private set; }
    }
}