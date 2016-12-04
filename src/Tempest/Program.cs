using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using Tempest.Arguments;

namespace Tempest
{
    public class Program
    {


        public static int Main(string[] args)
        {
            var strapper = TempestBootstrapper.CreateDefault();
            return strapper.Strap(args);
            

            // Should support following arguments:
            // 
            // -i | --install [<PackageName>|<PackageName.zip>]
            //      --update all
            
            // -u | --uninstall <PackageName>
            // -s | --search <SearchPath>                   Configures the directory to search for generators
            // -a | --add-search <SearchPath>               Adds a default path to search for generators
            // -r | --remove-search <SearchPath>            Removes a default path from being searched for generators
            // -l | --list [Generators|Search]              Lists all generators or search paths
            // -v | --verbosity <VerbosityLevel>            Specifies the verbosity level
            // -p | --para <Parameters>                     Specifies the generator parameters

            // Syntax:
            // tempest <generatorName> -iusarlvp <command>
            // tempest <generatorName> <Parameters> -iusarlvp <command>
            // tempest <generatorName> -iusarlvp <command> <Parameters>
            // tempest -iusarlvp command



            
        }
    }

    public abstract class TempestBootstrapper
    {
        public static TempestBootstrapper<IServiceCollection> CreateDefault()
        {
            return TempestBootstrapper<IServiceCollection>.Create(new DefaultServiceProviderFactory());
        }

    }

    public class TempestBootstrapper<T> : TempestBootstrapper
    {
        private readonly IServiceProviderFactory<T> _serviceProviderFactory;

        public static TempestBootstrapper<T2> Create<T2>(IServiceProviderFactory<T2> factory)
        {
            if (factory == null) throw new ArgumentNullException(nameof(factory));
            return new TempestBootstrapper<T2>(factory);
        }
        
        //public static TempestBootstrapper<T> Create(IServiceProviderFactory<T> factory = null)
        //{
        //    return new TempestBootstrapper<T>(factory);
        //}

        protected TempestBootstrapper(IServiceProviderFactory<T> serviceProviderFactory)
        {
            _serviceProviderFactory = serviceProviderFactory;
        }

        protected virtual IServiceProvider CreateProvider(IServiceProviderFactory<T> factory)
        {
            var services = new ServiceCollection();
            ConfigureLocalServices(services);
            var builder = factory.CreateBuilder(services);
            return factory.CreateServiceProvider(builder);
        }

        protected virtual void ConfigureLocalServices(IServiceCollection services)
        {
        }

        public int Strap(string[] args)
        {
            var container = CreateProvider(_serviceProviderFactory);

            // Resolve what?

            var arguments = new RunnerArgumentFactory().Create(args);
            var runner = BuildRunner(arguments);
            runner.Execute();
            return 0;
        }

        private static TempestRunner BuildRunner(RunnerArguments arguments)
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

