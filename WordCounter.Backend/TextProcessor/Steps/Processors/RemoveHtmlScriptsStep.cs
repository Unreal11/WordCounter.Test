using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using TextProcessor.Attributes;
using TextProcessor.Core;
using Utilities;

namespace TextProcessor.Steps.Processors;

[Order(StepsOrder.RemoveHtmlScriptsStep)]
public class RemoveHtmlScriptsStep : TextProcessingPipelineStep
{
    public RemoveHtmlScriptsStep(ILogger<TextProcessingPipelineStep> logger) : base(logger)
    {
    }

    public override async Task<string> ExecuteAsync(string text, TextProcessorSettings settings)
    {
        return await Task.Run(() =>
        {
            var htmlTagStartExpression = new Regex($"<scripts[^>]*>");
            var htmlTagEndExpression = new Regex($"</scripts^>]*>");

            var result = new StringBuilder();
            var position = 0;
            var match = htmlTagStartExpression.Match(text, position);

            while (match.Success)
            {
                result.Append(text.Substring(position, match.Index - position));

                var endMatch = htmlTagEndExpression.Match(text, match.Index);
                position = endMatch.Success ? endMatch.Index + endMatch.Length : text.Length;
                match = htmlTagStartExpression.Match(text, position);
            }

            result.Append(text.Substring(position));

            return result.ToString();
        });
    }
}