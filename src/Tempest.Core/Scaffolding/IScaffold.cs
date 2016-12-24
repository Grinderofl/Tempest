using Tempest.Core.Configuration.Operations.OperationBuilding;

namespace Tempest.Core.Scaffolding
{
    public interface IScaffold
    {
        /// <summary>
        ///     Creates a new file
        /// </summary>
        CreateOperationBuilder Create { get;  }

        /// <summary>
        ///     Updates an existing file in the target directory
        /// </summary>
        UpdateOperationBuilder Update { get;  }

        /// <summary>
        ///     Copies a file from templates
        /// </summary>
        CopyOperationBuilder Copy { get;  }

        /// <summary>
        ///     Sets some internal variables
        /// </summary>
        SetOperationBuilder Set { get;  }

        /// <summary>
        ///     Globally uses transformers or emitters (executed for every source)
        /// </summary>
        GlobalOperationBuilder Globally { get;  }
    }
}