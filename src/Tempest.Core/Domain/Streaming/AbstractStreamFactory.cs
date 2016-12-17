using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tempest.Core.Domain.Streaming
{
    public abstract class AbstractStreamFactory : IStreamFactory
    {
        public abstract Stream Create();
    }
}
