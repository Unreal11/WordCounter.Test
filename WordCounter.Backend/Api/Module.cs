using Api.Serivces;
using TextProcessor;
using TextProcessor.Core;
using WordCounter;
using WordCounter.Core;

namespace Api;

public static class Module
{
    public static void RegisterDependencies(IServiceCollection services)
    {
        services.AddScoped<IPipelineTextProcessor, PipelineTextProcessor>();
        services.AddScoped<IWordCounter, SimpleWordCounter>();
        services.AddScoped<ITextRequestService, TextRequestService>();
    }
}