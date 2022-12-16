using Microsoft.Extensions.Logging;
using TextProcessor.Core;

namespace TextProcessor;

/// <summary>
/// Стандартный процессор текста. 
/// Использует стандартные настройки обработки.
/// </summary>
public class PipelineTextProcessor : IPipelineTextProcessor
{
    public TextProcessorSettings Settings { get; set; }
    public TextProcessingPipeline Pipeline { get; set; }

    private readonly ILogger<IPipelineTextProcessor> _logger;

    public PipelineTextProcessor(ILogger<IPipelineTextProcessor> logger)
    {
        Settings = TextProcessorSettings.Default;
        Pipeline = TextProcessingPipeline.Empty;

        _logger = logger;
    }

    public async Task<string> ProcessingAsync(string text)
    {
        if (Settings == null)
        {
            _logger.LogError("Был запущен не настроенный текстовый процессор!");
            return text;
        }

        if (Pipeline == null || !Pipeline.Any())
        {
            _logger.LogWarning("Был запущен текстовый процессор с пусты пайплайном!");
            return text;
        }

        if (Settings.MaximalCharsNumber >= 0 && text.Length > Settings.MaximalCharsNumber)
        {
            _logger.LogWarning(@$"Текст будет сокращен до {Settings.MaximalCharsNumber} симовлов.");
            text = text.Substring(0, Settings.MaximalCharsNumber);
        }


        foreach (var step in Pipeline)
        {
            text = await step.ExecuteAsync(text, Settings);
        }

        return text;
    }
}