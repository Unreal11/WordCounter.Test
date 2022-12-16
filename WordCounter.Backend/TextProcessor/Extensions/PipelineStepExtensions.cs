using System.Reflection;
using TextProcessor.Attributes;
using TextProcessor.Core;

namespace TextProcessor.Extensions;

public static class PipelineStepExtensions
{
    public static int GetOrder(this TextProcessingPipelineStep step)
    {
        var orderAttribute = step.GetType().GetCustomAttribute<OrderAttribute>();

        if (orderAttribute == null)
        {
            return int.MinValue;
        }

        return orderAttribute.Order;
    }
}