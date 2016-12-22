using Tempest.Core.Configuration.Options.Base;

namespace Tempest.Core.Options
{
    public interface IOptionExecutor
    {
        void Execute(ConfigurationOption[] options, string[] selectedOptions);
    }
}