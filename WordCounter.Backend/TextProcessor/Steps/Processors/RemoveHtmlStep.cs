using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using TextProcessor.Attributes;
using TextProcessor.Core;
using Utilities;

namespace TextProcessor.Steps.Processors;

[Order(StepsOrder.FormatStep)]
public class RemoveHtmlStep : TextProcessingPipelineStep
{
    public RemoveHtmlStep(ILogger<TextProcessingPipelineStep> logger) : base(logger)
    {
    }

    public override async Task<string> ExecuteAsync(string text, TextProcessorSettings settings)
    {
        return await Task.Run(() =>
        {
            // Remove html entities
            text = Regex.Replace(text, @"&(?:[a-z\d]+|#\d+|#x[a-f\d]+)", CharDictionary.Whitespace.ToString(), RegexOptions.Compiled);
                
            // Remove tags
            var regexExpression = new Regex("<[^>]+>");
            var result = new StringBuilder();
            var currentPos = 0;
            var match = regexExpression.Match(text);

            while(match.Success)
            {
                result.Append(text.Substring(currentPos, match.Index - currentPos) + CharDictionary.Whitespace);
                currentPos = match.Index + match.Length;

                match = regexExpression.Match(text, currentPos);
            }
            if (currentPos == 0) 
            {
                return text;
            }

            result.Append(text.Substring(currentPos));

            return result.ToString();
        });
    }
}