using System.Collections.Generic;
using Tempest.Core.Emission;
using Tempest.Core.Sourcing;
using Tempest.Core.Transformation;

namespace Tempest.Core
{
    public abstract class TemplateStepBase
    {
        protected IList<Transformer> InternalTransformers = new List<Transformer>();
        protected IList<Emitter> InternalEmitters = new List<Emitter>();
        private readonly Source _source;

        protected TemplateStepBase(Source source)
        {
            _source = source;
        }

        public Source GetSource() => _source;

        public IEnumerable<Transformer> GetTransformers() => InternalTransformers;

        public IEnumerable<Emitter> GetEmitters() => InternalEmitters;

    }
}