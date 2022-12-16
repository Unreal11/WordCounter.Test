namespace WordCounter.Models;

public class WordCountGroup
{
    /// <summary>
    /// Размер группы. Указывает на количество слов.
    /// </summary>
    public int GroupSize { get; set; }

    /// <summary>
    /// Количество повторений группы слов в тексте
    /// </summary>
    public int Count { get; set; }
    
    /// <summary>
    /// Процент вхождения группы в текст
    /// </summary>
    public double Frequency { get; set; }

    /// <summary>
    /// Слово или словосочетание
    /// </summary>
    public string? Phrase { get; set; }
}