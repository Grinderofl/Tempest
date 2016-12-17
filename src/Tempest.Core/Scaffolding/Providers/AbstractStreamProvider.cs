using System.IO;

namespace Tempest.Core.Scaffolding.Providers
{
    public abstract class AbstractStreamProvider : IStreamProvider
    {

        public abstract Stream Provide();
        public virtual string Describe()
        {
            return $"{GetStreamDescriptor()} - {GetType()}";
        }

        protected abstract string GetStreamDescriptor();
    }
}
