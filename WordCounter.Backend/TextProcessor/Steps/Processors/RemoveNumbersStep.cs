using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using TextProcessor.Attributes;
using TextProcessor.Core;
using Utilities;

namespace TextProcessor.Steps.Processors;

[Order(StepsOrder.RemovePunctuationOrder)]
public class RemoveNumbersStep : TextProcessingPipelineStep
{
    private const string Pattern = @"[0-9]";

    public RemoveNumbersStep(ILogger<TextProcessingPipelineStep> logger) : base(logger)
    {
    }

    public override async Task<string> ExecuteAsync(string text, TextProcessorSettings settings)
    {
        await Task.CompletedTask;

        return Regex.Replace(text, Pattern, string.Empty, RegexOptions.Compiled);
    }
}