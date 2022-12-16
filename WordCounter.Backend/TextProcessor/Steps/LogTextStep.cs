using Microsoft.Extensions.Logging;
using TextProcessor.Core;

namespace TextProcessor.Steps;

public class LogTextStep : TextProcessingPipelineStep
{
    public LogTextStep(ILogger<TextProcessingPipelineStep> logger) : base(logger)
    {
    }

    public override async Task<string> ExecuteAsync(string text, TextProcessorSettings settings)
    {
        await Task.CompletedTask;

        Logger.LogDebug(text);

        return text;
    }
}