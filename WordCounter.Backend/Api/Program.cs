using Api;
using Api.Data;
using Api.Models;
using Api.Serivces;
using TextProcessor.Core;
using TextProcessor.Steps.Processors;
using WordCounter.Core;

var builder = WebApplication.CreateBuilder(args);

Module.RegisterDependencies(builder.Services);
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(x => { 
    x.AllowAnyOrigin();
    x.AllowAnyMethod();
    x.AllowAnyHeader();
});

app.MapPost("/api/CountWordGroups", async (RequestModel model) =>
{
    using (var scope = app.Services.CreateScope())
    {
        var request = scope.ServiceProvider.GetService<ITextRequestService>();
        var processor = scope.ServiceProvider.GetService<IPipelineTextProcessor>();
        var counter = scope.ServiceProvider.GetService<IWordCounter>();

        var text = await request.GetTextAsync(model.Uri);

        counter.Settings.Exceptions = model.RemoveGrammars == false
            ? Array.Empty<string>()
            : Grammars.Default;
        
        processor.Pipeline = TextProcessingPipelineBuilder.Create(scope.ServiceProvider)
            .AddStep<RemoveHtmlScriptsStep>()
            .AddStep<RemoveHtmlStep>()
            .AddStep<RemoveFormatStep>()
            .AddStep<PhraseSplittingStep>()
            .AddStep<RemovePunctuationStep>()
            .AddStep<RemoveNumbersStep>()
            .AddStep<RemoveRepeateWhitespacesStep>()
            .Build();

        var processedText = await processor.ProcessingAsync(text);
        var wordGroups = await counter.CountWordsAsync(processedText);
        
        var result = wordGroups.SelectMany(x => x.Groups
                .OrderByDescending(x => x.Count)
                .Take(model.GroupsCount)
                .ToArray())
            .ToArray();

        return result;
    }
});

app.Run();