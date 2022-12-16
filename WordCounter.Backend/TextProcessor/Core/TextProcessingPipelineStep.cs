using Microsoft.Extensions.Logging;

namespace TextProcessor.Core;

public abstract class TextProcessingPipelineStep
{
    protected ILogger Logger { get; }
    
    public TextProcessingPipelineStep(ILogger<TextProcessingPipelineStep> logger)
    {
        if (logger == null)
        {
            throw new ArgumentNullException(nameof(logger));
        }

        Logger = logger;
    }

    public abstract Task<string> ExecuteAsync(string text, TextProcessorSettings settings);
}