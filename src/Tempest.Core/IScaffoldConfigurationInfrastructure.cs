using Tempest.Core.Scaffolding;

namespace Tempest.Core
{
    public interface IScaffoldConfigurationInfrastructure
    {
        ScaffoldOperationConfiguration Configuration { get; set; }
    }
}