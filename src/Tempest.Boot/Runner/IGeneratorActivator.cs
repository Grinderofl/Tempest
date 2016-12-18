using System;
using Tempest.Core;

namespace Tempest.Boot.Runner
{
    public interface IGeneratorActivator
    {
        GeneratorEngineBase Activate(Type generatorType);
    }
}
