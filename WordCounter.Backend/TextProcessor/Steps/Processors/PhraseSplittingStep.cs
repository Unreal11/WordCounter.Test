using System.Text;
using Microsoft.Extensions.Logging;
using TextProcessor.Attributes;
using TextProcessor.Core;
using Utilities;

namespace TextProcessor.Steps.Processors;

[Order(StepsOrder.PhraseSplittingOrder)]
public class PhraseSplittingStep : TextProcessingPipelineStep
{
    public PhraseSplittingStep(ILogger<TextProcessingPipelineStep> logger) : base(logger)
    {
    }

    public override async Task<string> ExecuteAsync(string text, TextProcessorSettings settings)
    {
        var builder = new StringBuilder();

        await SafeCycle.WhileAsync(
            position => position < text.Length,
            position =>
            {
                var index = text.IndexOfAny(settings.PhraseSplitter, position);

                if (index < 0)
                {
                    index = text.Length;
                }

                var phrase = text.Substring(position, index - position);

                if (!string.IsNullOrEmpty(phrase))
                {
                    builder.AppendLine(phrase);
                }

                return index + 1;

            }, Logger);

        return builder.ToString();
    }
}