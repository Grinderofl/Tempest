namespace Tempest.Boot.Strapper
{
    public interface ICommandLineExecutor
    {
        int Execute(string[] args);
    }
}