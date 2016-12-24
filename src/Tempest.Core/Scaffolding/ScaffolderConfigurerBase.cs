using System;
using System.Collections.Generic;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Configuration.Operations.Sourcing;
using Tempest.Core.Operations;

namespace Tempest.Core.Scaffolding
{
    public abstract class ScaffolderConfigurerBase : IScaffoldConfigurer
    {
        //protected readonly IList<Action<ScaffoldOperationConfiguration>> Actions =
        //    new List<Action<ScaffoldOperationConfiguration>>();
        private IList<OperationBuilderBase> OperationBuilders { get; } = new List<OperationBuilderBase>();

        public virtual int Order => 0;

        //protected virtual OperationStep CreateStep(SourceFactory sourceFactory)
        //{
        //    var step = new OperationStep(sourceFactory);
        //    Actions.Add(conf => conf.Steps.Add(step));
        //    return step;
        //}

        protected TBuilder AddBuilder<TBuilder>(TBuilder builder) where TBuilder : OperationBuilderBase
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            OperationBuilders.Add(builder);
            return builder;
        }

        protected virtual ScaffoldOperationConfiguration ConfigureOperationsCore(
            ScaffoldOperationConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            //foreach (var action in Actions)
            //    action(configuration);

            foreach (var builder in OperationBuilders)
                builder.VisitConfiguration(configuration);

            return configuration;
        }

        void IScaffoldConfigurer.ConfigureOperations(
            ScaffoldOperationConfiguration configuration) => ConfigureOperationsCore(configuration);

    }
}