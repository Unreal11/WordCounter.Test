namespace WordCounter.Core;

public interface IContract
{
    IEnumerable<string> Phrases { get; set; }
}