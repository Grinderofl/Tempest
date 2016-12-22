namespace Tempest.Boot.Runner
{
    public interface ICommandLineExecutor
    {
        int Execute(string[] args);
    }
}