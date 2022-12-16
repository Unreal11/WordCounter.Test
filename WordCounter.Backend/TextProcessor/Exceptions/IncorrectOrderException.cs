using System.Runtime.Serialization;

/// <summary>
/// Нарушение очерёдности шагов обработки текста
/// </summary>
[Serializable]
public class IncorrectOrderException : Exception
{
    public IncorrectOrderException() : base("Нарушение очерёдности шагов обработки текста")
    {
    }
    
    protected IncorrectOrderException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}