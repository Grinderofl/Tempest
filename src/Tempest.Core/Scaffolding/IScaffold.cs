using Tempest.Core.Configuration.Operations.OperationBuilding;

namespace Tempest.Core.Scaffolding
{
    /// <summary>
    /// Configures the scaffolding builder
    /// </summary>
    public interface IScaffold
    {
        /// <summary>
        ///     Creates a new file from no physically existing store
        /// </summary>
        CreateOperationBuilder Create { get;  }

        /// <summary>
        ///     Updates an existing file in the target directory
        /// </summary>
        UpdateOperationBuilder Update { get;  }

        /// <summary>
        ///     Copies a file from Template directory
        /// </summary>
        CopyOperationBuilder Copy { get;  }

        /// <summary>
        ///     Sets some internal variables
        /// </summary>
        SetOperationBuilder Set { get;  }

        /// <summary>
        ///     Executes an action for every operation
        /// </summary>
        GlobalOperationBuilder Globally { get;  }
    }
}