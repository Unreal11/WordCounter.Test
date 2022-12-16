using System.Collections;

namespace TextProcessor.Core;

public class TextProcessingPipeline : IEnumerable<TextProcessingPipelineStep>
{
    public static TextProcessingPipeline Empty =>
        new TextProcessingPipeline(Enumerable.Empty<TextProcessingPipelineStep>());

    private readonly List<TextProcessingPipelineStep> _steps;

    public TextProcessingPipeline(IEnumerable<TextProcessingPipelineStep> steps)
    {
        _steps = steps.ToList();
    }

    public IEnumerator<TextProcessingPipelineStep> GetEnumerator()
    {
        return _steps.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _steps.GetEnumerator();
    }
}