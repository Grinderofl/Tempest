using System;
using System.Collections.Generic;
using System.IO;
using Tempest.Core.Domain.Operations;
using Tempest.Core.Emission;
using Tempest.Core.Setup;
using Tempest.Core.Sourcing;

namespace Tempest.Core
{
    public class OperationBuilder
    {
        public virtual IEnumerable<Operation> Build(IEnumerable<ScaffoldStep> steps, SourcingContext context)
        {
            foreach (var step in steps)
            {
                foreach (var operation in BuildOperations(step, context))
                    yield return operation;
            }

            // 1) Get source (file, web url...)
            // 2) Transformers
            // 3) Emitters? Targets?
        }

        private IEnumerable<Operation> BuildOperations(ScaffoldStep step, SourcingContext context)
        {
            var source = step.GetSource();
            var sourcingResults = source.Generate(context);
            
            foreach (var result in sourcingResults)
            {
                var funcs = new List<Func<Stream, Stream>>();
                var destinationFilename = result.FileName;
                Func<Stream, Stream> transformer = stream => stream;

                foreach (var t in step.GetTransformers())
                {
                    destinationFilename = t.TransformFilename(destinationFilename);

                    transformer = stream => t.TransformStream(stream);
                    
                    //

                    Func<Stream, Stream> func = s => t.TransformStream(s);
                    funcs.Add(func);
                }
                
                var emissionContext = new EmissionContext()
                {
                    FilePath = result.FilePath,
                    Filename = destinationFilename
                };

                foreach (var emitter in step.GetEmitters())
                {
                    foreach (var actualEmitter in  emitter.CreateEmitters(emissionContext))
                        yield return new Operation(result.OutputStream, transformer, actualEmitter);
                }
            }

        }


    }
}