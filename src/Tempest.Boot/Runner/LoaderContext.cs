using System;

namespace Tempest.Boot.Runner
{
    public class LoaderContext
    {
        public string Name { get; set; }
        public string AdditionalSearchPath { get; set; }
        public Type Type { get; set; }
    }
}