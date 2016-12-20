using System;
using System.Collections.Generic;
using Tempest.Core.Options;
using Tempest.Core.Scaffolding;
using Tempest.Core.Setup.OperationBuilding;

namespace Tempest.Core
{
    public abstract class ScaffolderConfigurerBase : IScaffoldConfigurer
    {
        private IList<OperationBuilderBase> OperationBuilders { get; } = new List<OperationBuilderBase>();

        public virtual int Order => 0;

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

            foreach (var builder in OperationBuilders)
                builder.VisitConfiguration(configuration);

            return configuration;
        }

        void IScaffoldConfigurer.ConfigureOperations(
            ScaffoldOperationConfiguration configuration) => ConfigureOperationsCore(configuration);

    }
}