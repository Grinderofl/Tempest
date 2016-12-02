using System;
using Tempest.Core.Sourcing;

namespace Tempest.Core
{
    public abstract class BuilderFactoryBase
    {
        private readonly GeneratorEngineBase _engine;

        protected BuilderFactoryBase(GeneratorEngineBase engine)
        {
            if (engine == null) throw new ArgumentNullException(nameof(engine));
            _engine = engine;
        }

        protected virtual TemplateStep CreateStep(Source source)
        {
            var step = new TemplateStep(source);
            _engine.Steps.Add(step);
            return step;
        }
    }
}