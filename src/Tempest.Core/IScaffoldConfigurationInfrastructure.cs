using Tempest.Core.Operations;

namespace Tempest.Core
{
    public interface IScaffoldConfigurationInfrastructure
    {
        ScaffoldOperationConfiguration Configuration { get; set; }
    }
}