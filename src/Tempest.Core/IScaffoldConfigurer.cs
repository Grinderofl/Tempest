using Tempest.Core.Scaffolding;

namespace Tempest.Core
{
    public interface IScaffoldConfigurer
    {
        int Order { get; }
        void ConfigureOperations(ScaffoldOperationConfiguration configuration);
    }
}