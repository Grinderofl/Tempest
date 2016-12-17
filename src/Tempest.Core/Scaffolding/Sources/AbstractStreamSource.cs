using System.IO;

namespace Tempest.Core.Scaffolding.Sources
{
    public abstract class AbstractStreamSource : IStreamSource
    {

        public abstract Stream Create();
        public virtual string Describe()
        {
            return $"{GetStreamDescriptor()} - {GetType()}";
        }

        protected abstract string GetStreamDescriptor();
    }
}
