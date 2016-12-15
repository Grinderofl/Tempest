using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Tempest.Core.Domain.Streaming
{
    public abstract class StreamFactory
    {
        public abstract Stream Create();
    }
}
