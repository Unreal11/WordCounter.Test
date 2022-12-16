namespace TextProcessor;

public class TextProcessorSettings
{
    /// <summary>
    /// Указывает максимальное допустимое количество символов.
    /// Для отключения ограничения установите значение -1
    /// </summary>
    public int MaximalCharsNumber { get; set; }

    /// <summary>
    /// Симвлы разделения предложений.
    /// </summary>
    public char[] PhraseSplitter
    {
        get => _phraseSplitter;
        set => _phraseSplitter = value;
    }
    
    public static TextProcessorSettings Default => new TextProcessorSettings()
    {
        MaximalCharsNumber = 690731,
        PhraseSplitter = new []
        {
            '.',
            ',',
            ';',
            '!',
            '?',
        }
    };

    private char[] _phraseSplitter = Array.Empty<char>();
}