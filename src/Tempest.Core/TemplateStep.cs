using System.Collections.Generic;
using Tempest.Core.Emission;
using Tempest.Core.Sourcing;
using Tempest.Core.Transformation;

namespace Tempest.Core
{
    public class TemplateStep
    {
        public TemplateStep(Source source)
        {
            Source = source;
        }

        public Source Source { get; }
        public IEnumerable<Transformer> Transformers { get; set; }
        public IEnumerable<Emitter> Emitters { get; set; }
    }
}