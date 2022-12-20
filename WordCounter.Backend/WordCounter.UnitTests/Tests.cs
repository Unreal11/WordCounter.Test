using TextProcessor.UnitTests.Utils;
using WordCounter.Models;

namespace WordCounter.UnitTests;

public class Tests
{
    [Test(Description = "Тест подсчёта количества слов")]
    public async Task ThreeWordCheckCount()
    {
        var simpleWordCounter = new SimpleWordCounter(new TestLogger<SimpleWordCounter>());
        var words = new List<string>
        {
            "раз два три раз два три раз два три",
            "раз два раз два",
            "раз два"
        };
        var listWords = await simpleWordCounter.CountWordsAsync(new Contract(words));

        Assert.True(listWords[2].Groups[0].Count == 2);
    }

    [Test(Description = "Тест подсчёта количества слов при пустом тексте")]
    public async Task EmptyTextCheckCount()
    {
        var simpleWordCounter = new SimpleWordCounter(new TestLogger<SimpleWordCounter>());
        var words = new List<string>
        {
            " "
        };
        var listWords = await simpleWordCounter.CountWordsAsync(new Contract(words));

        Assert.True(listWords[0].Groups[0].Count == 1);
    }

    [Test(Description = "Тест null")]
    public void WhenNullContractCheck()
    {
        var simpleWordCounter = new SimpleWordCounter(new TestLogger<SimpleWordCounter>());

        Assert.ThrowsAsync<NullReferenceException>(async () => await simpleWordCounter.CountWordsAsync(null));
    }
}