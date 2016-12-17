namespace Tempest.Core
{
    //public class ScaffoldStepExecutor
    //{
    //    private readonly ScaffoldStep _step;

    //    private IEnumerable<SourcingResult> _sourcingResults;
    //    private IList<TransformationResult> _transformationResults;
    //    public ScaffoldStepExecutor(ScaffoldStep step)
    //    {
    //        _step = step;
    //    }

    //    public virtual ScaffoldStepExecutor ExecuteSources(SourcingContext context)
    //    {
    //        if (context == null) throw new ArgumentNullException(nameof(context));
    //        var source = _step.GetSource();
    //        _sourcingResults = source.Generate(context);
    //        return this;
    //    }

    //    public virtual ScaffoldStepExecutor ExecuteTransformers(ICollection<Transformer> globalTransformers)
    //    {
    //        if(_sourcingResults == null)
    //            throw new NullReferenceException("Sources have not been executed yet.");

    //        _transformationResults = new List<TransformationResult>();

    //        foreach (var source in _sourcingResults)
    //        {
    //            var transformerContext = new TransformerContext(source.FileName, source.OutputStream);
    //            var transformResult = new TransformationResult(transformerContext.TransformationStream,
    //                transformerContext.Filename) {FilePath = source.FilePath};
    //            var transformers = globalTransformers.Union(_step.GetTransformers());

    //            foreach (var transformer in transformers)
    //            {
    //                transformResult = transformer.Transform(transformerContext);
    //                transformerContext.TransformationStream = transformResult.OutputStream;
    //                transformerContext.Filename = transformResult.Filename;
    //            }
    //            _transformationResults.Add(transformResult);
    //        }

    //        return this;
    //    }

    //    public virtual ScaffoldStepExecutor ExecuteEmitters(SourcingContext context)
    //    {
    //        if(_transformationResults == null)
    //            throw new NullReferenceException("Transformations have not been executed yet");

    //        foreach (var transform in _transformationResults)
    //        {
    //            var emissionContext = new EmissionContext()
    //            {
    //                Filename = transform.Filename,
    //                EmissionStream = transform.OutputStream,
    //                TargetDirectory = context.TargetRoot,
    //                FilePath = transform.FilePath
    //            };
    //            foreach (var emitter in _step.GetEmitters())
    //            {
    //                emitter.Emit(emissionContext);
    //            }
    //        }
    //        return this;
    //    }
    //}
}