using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Tempest.Core.Scaffolding.Operations;
using Tempest.Core.Scaffolding.Persistence;
using Tempest.Core.Scaffolding.Transforms;
using Tempest.Core.Setup.OperationBuilding;
using Tempest.Core.Setup.Sourcing;
using Tempest.Core.Setup.Transformation;

namespace Tempest.Core
{
    public class OperationBuilder
    {
        public virtual IEnumerable<Operation> Build(IEnumerable<OperationStep> steps, IList<OperationTransformer> globalTransformers, SourcingContext context)
        {
            foreach (var step in steps)
            {
                foreach (var operation in BuildOperations(step, globalTransformers, context))
                    yield return operation;
            }

            // 1) Get source (file, web url...)
            // 2) Transformers
            // 3) Emitters? Targets?
        }

        private IEnumerable<Operation> BuildOperations(OperationStep step, IList<OperationTransformer> globalTransformers, SourcingContext context)
        {
            var source = step.GetSource();
            var sourcingResults = source.Generate(context);
            
            foreach (var result in sourcingResults)
            {
                var destinationFilename = result.FileName;
                var destinationFilepath = result.FilePath;
                var streamTransformers = new List<IStreamTransformer>();
                
                foreach (var transformer in globalTransformers.Union(step.GetTransformers()))
                {
                    destinationFilepath = transformer.TransformFilename(destinationFilepath);
                    destinationFilename = transformer.TransformFilename(destinationFilename);
                    streamTransformers.Add(transformer.CreateStreamTransformer());
                }

                var actualTransformer = new CompoundStreamTransformer(streamTransformers);

                var emissionContext = new PersistenceContext()
                {
                    FilePath = destinationFilepath,
                    Filename = destinationFilename,
                    TargetDirectory = context.TargetRoot
                };

                foreach (var emitter in step.GetEmitters())
                {
                    foreach (var actualEmitter in  emitter.CreatePersisters(emissionContext))
                        yield return new Operation(result.Provider, actualTransformer, actualEmitter);
                }
            }

        }


    }
}