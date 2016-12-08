using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tempest.Core;

namespace Tempest.Runner
{
    public interface IGeneratorActivator
    {
        GeneratorEngineBase Activate(Type generatorType);
    }
}
