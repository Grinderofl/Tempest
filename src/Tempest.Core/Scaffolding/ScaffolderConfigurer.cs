using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Scaffolding.Base;

namespace Tempest.Core.Scaffolding
{
    /// <summary>
    /// Actually is 'ScaffoldConfigurationBuilder' or something
    /// </summary>
    public class ScaffolderConfigurer : ScaffolderConfigurerBase, IScaffoldBuilder
    {
        public ScaffolderConfigurer()
        {
            Create = AddBuilder(new CreateOperationBuilder());
            Update = AddBuilder(new UpdateOperationBuilder());
            Copy = AddBuilder(new CopyOperationBuilder());
            Set = AddBuilder(new SetOperationBuilder());
            Globally = AddBuilder(new GlobalOperationBuilder());
        }

        /// <summary>
        ///     Creates a new file
        /// </summary>
        public CreateOperationBuilder Create { get; set; }

        /// <summary>
        ///     Updates an existing file in the target directory
        /// </summary>
        public UpdateOperationBuilder Update { get; set; }

        /// <summary>
        ///     Copies a file from templates
        /// </summary>
        public CopyOperationBuilder Copy { get; set; }

        /// <summary>
        ///     Sets some internal variables
        /// </summary>
        public SetOperationBuilder Set { get; set; }

        /// <summary>
        ///     Globally uses transformers or emitters (executed for every source)
        /// </summary>
        public GlobalOperationBuilder Globally { get; set; }
    }
}