using Microsoft.Extensions.Logging;
using Utilities;
using WordCounter.Core;
using WordCounter.Models;

namespace WordCounter;

public class SimpleWordCounter : IWordCounter
{
    public record GroupContainer(int Size, Dictionary<string, int> Groups);

    public WordCounterSettings Settings
    {
        get => _settings;
        set => _settings = value ?? WordCounterSettings.Default;
    }

    private WordCounterSettings _settings = WordCounterSettings.Default;
    private readonly ILogger<SimpleWordCounter> _logger;

    public SimpleWordCounter(ILogger<SimpleWordCounter> logger)
    {
        _logger = logger;
    }

    public async Task<List<WordGroupContainer>> CountWordsAsync(IContract contract)
    {
        if (contract is null)
            throw new NullReferenceException($"Parameter {nameof(contract)} cannot be null!");

        var groupContainers = Settings.WordCountGroups
            .Select(x => new GroupContainer(x, new Dictionary<string, int>()))
            .ToList();

        await Parallel.ForEachAsync(groupContainers, async (container, token) =>
        {
            foreach (var phrase in contract.Phrases)
            {
                await SafeCycle.WhileAsync(
                    position => position >= 0 && position < phrase.Length,
                    position =>
                    {
                        if (TryGetGroup(phrase, container.Size, position, out int index))
                        {
                            var wordGroup = phrase.Substring(position, index - position - 1);

                            if (!Settings.Exceptions.Any(x => wordGroup.Contains(x)))
                            {
                                if (!container.Groups.ContainsKey(wordGroup))
                                {
                                    container.Groups.Add(wordGroup, 1);
                                }
                                else
                                {
                                    container.Groups[wordGroup]++;
                                }
                            }
                        }

                        return GetNextWordIndex(phrase, position);
                    }, _logger);
            }
        });

        return groupContainers.Select(container => new WordGroupContainer()
        {
            GroupSize = container.Size,
            Groups = container.Groups.Select(x => new WordCountGroup()
            {
                GroupSize = container.Size,
                Count = x.Value,
                Frequency = Math.Round(x.Value / (double)container.Groups.Count, 2),
                Phrase = x.Key
            }).ToList()
        }).ToList();
    }
    
    private bool TryGetGroup(string phrase, int wordsNumber, int startIndex, out int index)
    {
        index = startIndex;

        for (int i = 0; i < wordsNumber; i++)
        {
            index = GetNextWordIndex(phrase, index);

            if (index < 0)
            {
                return false;
            }
        }

        return true;
    }

    private int GetNextWordIndex(string phrase, int startIndex)
    {
        var index = phrase.IndexOf(CharDictionary.Whitespace, startIndex);

        return index >= 0
            ? index + 1
            : index;
    }
}