using System.IO;

namespace Tempest.Core.Operations.Providers
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
