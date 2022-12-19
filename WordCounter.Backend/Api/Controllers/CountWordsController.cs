using Api.Data;
using Api.Models;
using Api.Serivces;
using Microsoft.AspNetCore.Mvc;
using TextProcessor.Core;
using TextProcessor.Steps.Processors;
using Utilities;
using WordCounter.Core;
using WordCounter.Models;

namespace Api.Controllers;

[Route("api/[action]")]
[ApiController]
public class CountWordsController : ControllerBase
{
    private readonly IWordCounter _wordCounter;
    private readonly IPipelineTextProcessor _pipelineTextProcessor;
    private readonly ITextRequestService _textRequestService;
    private readonly IServiceProvider _serviceProvider;

    public CountWordsController(ITextRequestService textRequestService,
        IPipelineTextProcessor pipelineTextProcessor,
        IWordCounter wordCounter,
        IServiceProvider serviceProvider)
    {
        _wordCounter = wordCounter;
        _pipelineTextProcessor = pipelineTextProcessor;
        _textRequestService = textRequestService;
        _serviceProvider = serviceProvider;
    }

    [HttpPost]
    public async Task<WordCountGroup[]> CountWordGroupsAsync(RequestModel model)
    {
        var text = await _textRequestService.GetTextAsync(model.Uri);

        _wordCounter.Settings.Exceptions = model.RemoveGrammars == false
            ? Array.Empty<string>()
            : Grammars.Default;

        _pipelineTextProcessor.Pipeline = TextProcessingPipelineBuilder.Create(_serviceProvider)
            .AddStep<RemoveHtmlScriptsStep>()
            .AddStep<RemoveHtmlStep>()
            .AddStep<RemoveFormatStep>()
            .AddStep<PhraseSplittingStep>()
            .AddStep<RemovePunctuationStep>()
            .AddStep<RemoveNumbersStep>()
            .AddStep<RemoveRepeateWhitespacesStep>()
            .Build();

        var processedText = await _pipelineTextProcessor.ProcessingAsync(text);
        var contractDto = new Contract(processedText.Split(CharDictionary.NewLine));
        var wordGroups = await _wordCounter.CountWordsAsync(contractDto);

        var result = wordGroups.SelectMany(x => x.Groups
                .OrderByDescending(x => x.Count)
                .Take(model.GroupsCount)
                .ToArray())
            .ToArray();

        return result;
    }
}