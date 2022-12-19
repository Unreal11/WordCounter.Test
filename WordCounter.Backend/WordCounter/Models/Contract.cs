using WordCounter.Core;

namespace WordCounter.Models;

public class Contract : IContract
{
    public IEnumerable<string> Phrases { get; set; }
    
    public Contract(IEnumerable<string> phrases)
    {
        Phrases = phrases;
    }
}