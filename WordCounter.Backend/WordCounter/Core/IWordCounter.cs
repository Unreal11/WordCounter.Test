using WordCounter.Models;

namespace WordCounter.Core;

public interface IWordCounter
{
    WordCounterSettings Settings { get; set; }

    Task<List<WordGroupContainer>> CountWordsAsync(IContract contract);
}