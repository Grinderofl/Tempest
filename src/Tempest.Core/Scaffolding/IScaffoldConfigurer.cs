using Tempest.Core.Operations;

namespace Tempest.Core.Scaffolding
{
    public interface IScaffoldConfigurer
    {
        int Order { get; }
        void ConfigureOperations(ScaffoldOperationConfiguration configuration);
    }
}