using Tempest.Core.Configuration.Options;
using Tempest.Core.Configuration.Options.Base;

namespace Tempest.Core.Options
{
    public interface IOptionExecutor
    {
        void Execute(IConfigurationOption[] options, string[] selectedOptions);
    }
}