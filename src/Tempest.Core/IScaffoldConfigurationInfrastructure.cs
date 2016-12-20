using Tempest.Core.Scaffolding;

namespace Tempest.Core
{
    public interface IScaffoldConfigurationInfrastructure
    {
        ScaffoldingConfiguration Configuration { get; set; }
    }
}