using Tempest.Core.Utils;

namespace Tempest.Core.Emission
{
    /// <summary>
    /// Emits the input stream into ... wherever
    /// Could be:
    /// FileEmitter
    /// GithubEmitter
    /// FtpUploadEmitter
    /// DeleteFileEmitter? No, that's wrong, should file deletes even be possible? Maybe template has command 'undo'? :P no, maybe the template needs to delete something in order to use another feature? idk!
    /// </summary>
    public abstract class Emitter
    {
        public virtual EmissionResult Emit(EmissionContext context)
        {
            return new EmissionResult();
        }

    }
}