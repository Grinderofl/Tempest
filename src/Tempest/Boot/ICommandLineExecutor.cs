namespace Tempest.Boot
{
    public interface ICommandLineExecutor
    {
        int Execute(string[] args);
    }
}