namespace TextProcessor.Extensions;

public static class StringExtension
{
    /// <summary>
    /// Возвращает пустую строку в случае если передан null
    /// </summary>
    public static string GetStringOrEmpty(this string text)
    {
        return text ?? string.Empty;
    }
}