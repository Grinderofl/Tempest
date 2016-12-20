using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Logging;
using Tempest.Core.Options;
using Tempest.Core.Setup.OperationBuilding;
using Tempest.Core.Setup.Sourcing;
using Tempest.Core.Setup.Transformation;

namespace Tempest.Core
{

    /// <summary>
    /// Generator base. Has the task of configuring options and steps for scaffolder
    /// Also executes the scaffolder
    /// </summary>
    public abstract class GeneratorBase : ExecutableGeneratorBase
    {
        protected GeneratorBase(IScaffolderServiceFactory scaffolderServiceFactory) : base(scaffolderServiceFactory)
        {
        }

        
    }

    public abstract class GenericGenerator<TConfiguration, TOptions> : GeneratorBase
        where TConfiguration : ScaffolderConfigurer, new() where TOptions : OptionConfigurer, new()
    {
        protected GenericGenerator()
            : base(new ScaffolderServiceFactory(new TConfiguration(), new TOptions(), new OptionExecutor()))
        {
        }
    }

    public class ScaffolderServiceFactory : IScaffolderServiceFactory
    {
        private readonly ScaffolderConfigurer _scaffoldingConfiguration;
        private readonly OptionConfigurer _optionConfigurer;
        private readonly OptionExecutor _executor;

        public ScaffolderServiceFactory(ScaffolderConfigurer scaffoldingConfiguration, OptionConfigurer optionConfigurer,
            OptionExecutor executor)
        {
            _scaffoldingConfiguration = scaffoldingConfiguration;
            _optionConfigurer = optionConfigurer;
            _executor = executor;
        }

        public ScaffolderConfigurer CreateScaffolder()
        {
            return _scaffoldingConfiguration;
        }

        public OptionConfigurer CreateOptions()
        {
            return _optionConfigurer;
        }

        public OptionExecutor CreateExecutor()
        {
            return _executor;
        }
    }

    public class DefaultScaffolderServiceFactory : IScaffolderServiceFactory
    {
        public DefaultScaffolderServiceFactory()
        {
            
        }

        public ScaffolderConfigurer CreateScaffolder()
        {
            
        }

        public OptionConfigurer CreateOptions()
        {
            return new OptionConfigurer();
        }

        public OptionExecutor CreateExecutor()
        {
            return new OptionExecutor();
        }
    }

    public interface IScaffolderServiceFactory
    {
        ScaffolderConfigurer CreateScaffolder();
        OptionConfigurer CreateOptions();
        OptionExecutor CreateExecutor();

    }

    public abstract class ExecutableGeneratorBase
    {
        private readonly IScaffolderServiceFactory _scaffolderServiceFactory;

        protected ExecutableGeneratorBase(IScaffolderServiceFactory scaffolderServiceFactory)
        {
            if (scaffolderServiceFactory == null) throw new ArgumentNullException(nameof(scaffolderServiceFactory));
            _scaffolderServiceFactory = scaffolderServiceFactory;
        }

        protected abstract void ConfigureOptions(OptionConfigurer configuration);

        public virtual void Execute(GeneratorContext context)
        {
            var options = _scaffolderServiceFactory.CreateOptions();
            ConfigureOptions(options);

            var configuration = _scaffolderServiceFactory.CreateScaffolder();
            ConfigureScaffolder(configuration);

        }

        protected abstract void ConfigureScaffolder(ScaffolderConfigurer configuration);
    }
    

    public class OptionConfigurer
    {
        public OptionsConfiguration Options { get; }

        public OptionConfigurer()
        {
            Options = new OptionsConfiguration();
        }

        protected T AddOption<T>(T option) where T : ConfigurationOption
        {
            Options.Options.Add(option);
            return option;
        }

        public ListConfigurationOption List(string optionTitle)
        {
            return AddOption(new ListConfigurationOption(optionTitle));
        }

        public ListConfigurationOption List(string optionTitle, Action<string> action)
        {
            return AddOption(new ListConfigurationOption(optionTitle, action));
        }

        public InputConfigurationOption Input(string optionTitle, Action<string> action)
        {
            return AddOption(new InputConfigurationOption(optionTitle, action));
        }

        public ListConfigurationOption List(Func<string> optionTitle)
        {
            return AddOption(new ListConfigurationOption(optionTitle));
        }

        public ListConfigurationOption List(Func<string> optionTitle, Action<string> resultingAction)
        {
            return AddOption(new ListConfigurationOption(optionTitle, resultingAction));
        }
    }

    public abstract class GeneratorEngineBase
    {

        private readonly OptionExecutor _optionExecutor = new OptionExecutor();
        
        protected internal IList<OperationStep> Steps { get; set; } = new List<OperationStep>();

        protected internal IList<OperationTransformer> GlobalTransformers { get; set; } = new List<OperationTransformer>();

        protected abstract DirectoryInfo BuildTargetPath(GeneratorContext generatorContext);

        protected abstract DirectoryInfo BuildTemplatePath(GeneratorContext generatorContext);

        /// <summary>
        ///     Setup the options
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<ConfigurationOption> SetupOptions()
        {
            yield break;
        }

        /// <summary>
        ///     Execute your codez!
        /// </summary>
        protected abstract void ExecuteCore();

        protected virtual IEnumerable<OperationTransformer> GetEligibleTransformers(OperationStep step)
            => GlobalTransformers.Union(step.GetTransformers());

        public virtual void Run(GeneratorContext context)
        {
            var options = SetupOptions();
            _optionExecutor.Execute(options.ToArray(), context.Arguments);
            ExecuteCore();

            var sourcingContext = new SourcingContext
            {
                TemplateRoot = context.TemplateDirectory,
                TargetRoot = BuildTargetPath(context)
            };

            var operationBuilder = new OperationBuilder();
            var operations = operationBuilder.Build(Steps, GlobalTransformers, sourcingContext);

            foreach (var operation in operations)
            {
                if (context.ShouldLogOperation())
                {
                    // Log
                }
                operation.Execute();
            }
        }


    }
}