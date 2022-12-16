namespace WordCounter.Models;

public class WordGroupContainer
{
    /// <summary>
    /// Размер группы. Указывает на количество слов.
    /// </summary>
    public int GroupSize { get; set; }
    
    /// <summary>
    /// Группы слов
    /// </summary>
    public List<WordCountGroup> Groups { get; set; }
}