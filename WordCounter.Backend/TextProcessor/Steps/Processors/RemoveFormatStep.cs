using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using TextProcessor.Attributes;
using TextProcessor.Core;
using Utilities;

namespace TextProcessor.Steps.Processors;

[Order(StepsOrder.FormatStep)]
public class RemoveFormatStep : TextProcessingPipelineStep
{
    private const string Pattern = @"\n|\r";

    public RemoveFormatStep(ILogger<TextProcessingPipelineStep> logger) : base(logger)
    {
    }

    public override async Task<string> ExecuteAsync(string text, TextProcessorSettings settings)
    {
        await SafeCycle.WhileAsync(() =>
        {
            var match = Regex.Match(text, Pattern);

            if (match.Success)
            {
                text = text.Remove(match.Index, match.Length);
            }

            return match.Success;

        }, Logger);

        return text;
    }
}