using System.Collections.Generic;
using System.Diagnostics;
using Tempest.Core.Operations.Persistence;

namespace Tempest.Core.Configuration.Operations.Persistence
{
    /// <summary>
    ///     Emits the input stream into ... wherever
    ///     Could be:
    ///     FileEmitter
    ///     GithubEmitter
    ///     FtpUploadEmitter
    ///     DeleteFileEmitter? No, that's wrong, should file deletes even be possible? Maybe template has command 'undo'? :P
    ///     no, maybe the template needs to delete something in order to use another feature? idk!
    /// </summary>
    [DebuggerDisplay("{DebuggerDisplay()}")]
    public abstract class PersisterFactoryBase
    {
        protected virtual string DebuggerDisplay()
        {
            return $"{GetType()} for {GetPersistenceTarget()}";
        }

        protected abstract string GetPersistenceTarget();
        
        public abstract IEnumerable<IStreamPersister> CreatePersisters(PersistenceContext context);
    }
}