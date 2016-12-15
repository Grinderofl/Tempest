using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Tempest.Core.Emission
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
    public abstract class EmitterFactory
    {
        protected virtual string DebuggerDisplay()
        {
            return $"{GetType()} for {GetEmissionTarget()}";
        }

        protected abstract string GetEmissionTarget();
        
        public abstract IEnumerable<ActualEmitter> CreateEmitters(EmissionContext context);
    }
}