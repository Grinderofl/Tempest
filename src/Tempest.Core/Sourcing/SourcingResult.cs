﻿using System.IO;

namespace Tempest.Core.Sourcing
{
    public class SourcingResult
    {
        public Stream OutputStream { get; set; }
        public string FileName { get; set; }
    }
}