using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TextProcessor.Exceptions;
using TextProcessor.Extensions;

namespace TextProcessor.Core;

public static class TextProcessingPipelineBuilder
{
    public class Builder
    {
        public ILogger Logger { get; }
        public IServiceProvider ServiceProvider { get; }
        public List<TextProcessingPipelineStep> StepCollection { get; }

        public Builder(IServiceProvider provider)
        {
            ServiceProvider = provider;
            StepCollection = new List<TextProcessingPipelineStep>();
            Logger = provider.GetService<ILogger>();
        }
    }

    public static Builder Create(IServiceProvider provider)
    {
        var builder = new Builder(provider);

        return builder;
    }

    public static Builder InsertStepWithOrder<T>(this Builder builder)
        where T : TextProcessingPipelineStep =>
        builder.InsertStepWithOrder(CreateStep<T>(builder));

    public static Builder InsertStepWithOrder(this Builder builder, TextProcessingPipelineStep step)
    {
        var order = step.GetOrder();
        var index = order >= 0
            ? builder.StepCollection.FindIndex(x => x.GetOrder() >= 0 && x.GetOrder() > order)
            : -1;

        if (index < 0)
        {
            builder.StepCollection.Add(step);
        }
        else
        {
            builder.StepCollection.Insert(index, step);
        }

        return builder;
    }

    public static Builder AddStep<T>(this Builder builder)
        where T : TextProcessingPipelineStep => 
        builder.AddStep(CreateStep<T>(builder));
    

    public static Builder AddStep(this Builder builder, TextProcessingPipelineStep step)
    {
        var order = step.GetOrder();

        if (order >= 0 && builder.StepCollection
                .Select(x => x.GetOrder())
                .Where(x => x >= 0)
                .Any(x => order > x))
        {
            throw new IncorrectOrderException();
        }

        builder.StepCollection.Add(step);

        return builder;
    }

    public static TextProcessingPipeline Build(this Builder builder)
    {
        var pipeline = new TextProcessingPipeline(builder.StepCollection.ToArray());

        return pipeline;
    }
    
    private static T CreateStep<T>(Builder builder)
        where T : TextProcessingPipelineStep
    {
        var logger = builder.ServiceProvider.GetService<ILogger<T>>() as ILogger<TextProcessingPipelineStep>;
        
        if (logger == null)
        {
            throw new ServiceNotFoundExceptionException(typeof(ILogger<T>));
        }

        var step = Activator.CreateInstance(typeof(T), logger) as T;

        if (step == null)
        {
            throw new Exception($"Не удалось создать экземпляр объекта с типом {typeof(T).Name}");
        }

        return step;
    }
}