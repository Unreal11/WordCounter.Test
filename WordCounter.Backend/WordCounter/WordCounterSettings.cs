namespace WordCounter;

public class WordCounterSettings
{
    /// <summary>
    /// Список групп слов
    /// </summary>
    public int[] WordCountGroups
    {
        get => _wordCountGroup;
        set => _wordCountGroup = value ?? Enumerable.Empty<int>().ToArray();
    }
    
    /// <summary>
    /// Слова-исключения
    /// </summary>
    public string[] Exceptions
    {
        get => _exceptions;
        set => _exceptions = value;
    }

    private int[] _wordCountGroup = Array.Empty<int>();
    private string[] _exceptions = Array.Empty<string>();

    public static 
    WordCounterSettings Default => new WordCounterSettings()
    {
        WordCountGroups = new[] { 1, 2, 3 }
    };
}