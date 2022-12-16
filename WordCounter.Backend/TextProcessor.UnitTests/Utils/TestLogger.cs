namespace TextProcessor.UnitTests.Utils;

public class TestLogger : ILogger
{
    public Dictionary<LogLevel, List<string>> Logs { get; }

    public TestLogger()
    {
        Logs = Enum.GetValues<LogLevel>()
            .ToDictionary(
                x => (LogLevel)x, 
                x => new List<string>());
    }
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException();
    }
}

public class TestLogger<T> : ILogger<T>
{
    public Dictionary<LogLevel, List<string>> Logs { get; }

    public TestLogger()
    {
        Logs = Enum.GetValues<LogLevel>()
            .ToDictionary(
                x => (LogLevel)x, 
                x => new List<string>());
    }
    
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        throw new NotImplementedException();
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        throw new NotImplementedException();
    }
}