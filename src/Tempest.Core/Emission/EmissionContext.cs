using System;
using System.IO;

namespace Tempest.Core.Emission
{
    public class EmissionContext
    {
        public DirectoryInfo TargetDirectory { get; set; }
        public Stream InputStream { get; set; }
        public string ReadInputAsString()
        {
            throw new NotImplementedException();
        }
    }
}