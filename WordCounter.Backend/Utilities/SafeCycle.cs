using Microsoft.Extensions.Logging;

namespace Utilities;

public static class SafeCycle
{
    /// <summary>
    /// Максимальное количество итераций в циклах.
    /// Позволяет не повесить приложение в бесконечном цикле.
    /// </summary>
    /// <value></value>
    public static int MaximalIterationCount { get; set; } = 4096;

    public static void While(ILogger logger, Func<bool> iterationAction)
    {
        var iteration = 0;

        while (iteration < MaximalIterationCount)
        {
            if (!iterationAction.Invoke())
            {
                return;
            }

            iteration++;
        }

        logger.LogError("Достигнуто максимальное число итераций!");
    }

    public static Task WhileAsync(Func<int, bool> conditionFunc, Func<int, int> iterationFunc, ILogger logger)
    {
        return Task.Run(() =>
        {
            var iteration = 0;
            var counter = 0;

            while (iteration < MaximalIterationCount)
            {
                if (!conditionFunc.Invoke(counter))
                {
                    return;
                }

                counter = iterationFunc.Invoke(counter);
                iteration++;
            }

            logger.LogError("Достигнуто максимальное число итераций!");
        });
    }

    public static Task WhileAsync(Func<bool> iterationFunc, ILogger logger)
    {
        return Task.Run(() =>
        {
            var iteration = 0;

            while (iteration < MaximalIterationCount)
            {
                if (!iterationFunc.Invoke())
                {
                    return;
                }

                iteration++;
            }

            logger.LogError("Достигнуто максимальное число итераций!");
        });
    }
}