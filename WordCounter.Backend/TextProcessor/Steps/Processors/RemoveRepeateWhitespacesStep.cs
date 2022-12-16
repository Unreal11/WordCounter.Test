using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using TextProcessor.Attributes;
using TextProcessor.Core;
using Utilities;

namespace TextProcessor.Steps.Processors;

[Order(StepsOrder.RemoveSpacesOrder)]
public class RemoveRepeateWhitespacesStep : TextProcessingPipelineStep
{
    private const string Pattern = @"\s{2,}";

    public RemoveRepeateWhitespacesStep(ILogger<TextProcessingPipelineStep> logger) : base(logger)
    {
    }

    public override async Task<string> ExecuteAsync(string text, TextProcessorSettings settings)
    {
        await Task.CompletedTask;
        
        return Regex.Replace(text, Pattern, CharDictionary.Whitespace.ToString(), RegexOptions.Compiled);
    }
}