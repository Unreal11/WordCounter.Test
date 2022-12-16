namespace TextProcessor.Core;

public interface IPipelineTextProcessor
{
    TextProcessorSettings Settings { get; set; }
    TextProcessingPipeline Pipeline { get; set; }
    Task<string> ProcessingAsync(string text);
}