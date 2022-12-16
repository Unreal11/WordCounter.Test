namespace TextProcessor.Exceptions;

[Serializable]
public class ServiceNotFoundExceptionException : System.Exception
{
    public ServiceNotFoundExceptionException(Type type) : base($"Не удалось найти сервис {type}") { }
}