using System.Collections.Generic;
using System.Linq;
using Tempest.Core.Configuration.Operations.OperationBuilding;
using Tempest.Core.Configuration.Operations.Sourcing;
using Tempest.Core.Operations.Persistence;
using Tempest.Core.Operations.Transforms;

namespace Tempest.Core.Operations.Execution.Impl
{
    public class OperationBuilder : IOperationBuilder
    {
        public IEnumerable<Operation> Build(ScaffoldOperationConfiguration configuration, SourcingContext sourcingContext)
        {
            foreach (var step in configuration.Steps)
            {
                foreach (var operation in BuildOperations(step, configuration, sourcingContext))
                    yield return operation;
            }
        }

        protected virtual IEnumerable<Operation> BuildOperations(OperationStep step, ScaffoldOperationConfiguration configuration, SourcingContext context)
        {
            var source = step.GetSource(configuration);
            var sourcingResults = source.Generate(context);
            
            foreach (var result in sourcingResults)
            {
                var destinationFilename = result.FileName;
                var destinationFilepath = result.FilePath;
                var streamTransformers = new List<IStreamTransformer>();
                
                foreach (var transformer in configuration.GlobalTransformers.Union(step.GetTransformers()))
                {
                    destinationFilepath = transformer.TransformFilename(destinationFilepath);
                    destinationFilename = transformer.TransformFilename(destinationFilename);
                    streamTransformers.Add(transformer.CreateStreamTransformer());
                }

                var actualTransformer = new CompoundStreamTransformer(streamTransformers);

                var persistenceContext = new PersistenceContext()
                {
                    FilePath = destinationFilepath,
                    Filename = destinationFilename,
                    TargetDirectory = context.TargetRoot
                };

                foreach (var emitter in step.GetEmitters())
                {
                    foreach (var actualEmitter in  emitter.CreatePersisters(persistenceContext))
                        yield return new Operation(result.Provider, actualTransformer, actualEmitter);
                }
            }

        }



    }
}