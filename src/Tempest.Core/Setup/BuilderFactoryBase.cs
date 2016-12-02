using System;
using Tempest.Core.Sourcing;

namespace Tempest.Core.Setup
{
    public abstract class BuilderFactoryBase
    {
        protected readonly GeneratorBase Engine;

        protected BuilderFactoryBase(GeneratorBase engine)
        {
            if (engine == null) throw new ArgumentNullException(nameof(engine));
            Engine = engine;
        }

        protected virtual TemplateStep CreateStep(Source source)
        {
            var step = new TemplateStep(source);
            Engine.Steps.Add(step);
            return step;
        }
    }
}