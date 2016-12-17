using System.Collections.Generic;
using System.Diagnostics;
using Tempest.Core.Scaffolding.Persistence;

namespace Tempest.Core.Setup.Persisters
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
    public abstract class PersisterFactory
    {
        protected virtual string DebuggerDisplay()
        {
            return $"{GetType()} for {GetPersistenceTarget()}";
        }

        protected abstract string GetPersistenceTarget();
        
        public abstract IEnumerable<IStreamPersister> CreatePersisters(PersistenceContext context);
    }
}