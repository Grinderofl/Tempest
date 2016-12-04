using System;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Boot;

namespace Tempest
{
    public class DefaultTempestBootstrapper : TempestBootstrapper<IServiceCollection>
    {
        private DefaultTempestBootstrapper(IServiceProviderFactory<IServiceCollection> serviceProviderFactory) : base(serviceProviderFactory)
        {
        }

        public static TempestBootstrapper<IServiceCollection> Create()
        {
            return new DefaultTempestBootstrapper(new DefaultServiceProviderFactory());
        }
    }

    public abstract class TempestBootstrapper
    {
        protected abstract IServiceProvider CreateProvider();

        public virtual int Execute(string[] args)
        {
            var provider = CreateProvider();
            var executor = provider.GetService<ICommandLineExecutor>();
            return executor.Execute(args);
        }
    }

    public interface IServiceConfigurationConvention
    {
        void Configure(IServiceCollection services);
    }

    public class TempestBootstrapper<T> : TempestBootstrapper
    {
        private readonly IServiceProviderFactory<T> _serviceProviderFactory;

        public static TempestBootstrapper<TBuilder> Create<TBuilder>(IServiceProviderFactory<TBuilder> factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return new TempestBootstrapper<TBuilder>(factory);
        }

        protected TempestBootstrapper(IServiceProviderFactory<T> serviceProviderFactory)
        {
            _serviceProviderFactory = serviceProviderFactory;
        }

        protected override IServiceProvider CreateProvider()
        {
            var services = new ServiceCollection();
            ConfigureLocalServices(services);
            var builder = _serviceProviderFactory.CreateBuilder(services);
            return _serviceProviderFactory.CreateServiceProvider(builder);
        }

        protected virtual void ConfigureLocalServices(IServiceCollection services)
        {
        }

        public int Strap(TempestRunnerArguments args)
        {
            var container = CreateProvider();

            // Resolve what?

            //var arguments = new RunnerArgumentFactory().Create(args);
            var runner = BuildRunner(args);
            runner.Execute();
            return 0;
        }

        private static TempestRunner BuildRunner(TempestRunnerArguments arguments)
        {
            GeneratorLoader loader = BuildGeneratorLoader();
            var runner = new TempestRunner(arguments, loader);
            return runner;
        }

        private static GeneratorLoader BuildGeneratorLoader()
        {
            var directoryFinder = new DirectoryFinder();
            var assemblyFinder = new GeneratorAssemblyFinder(directoryFinder);
            var loader = new GeneratorLoader(assemblyFinder);
            return loader;
        }
    }
}