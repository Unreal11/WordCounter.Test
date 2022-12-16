using TextProcessor.Core;
using TextProcessor.UnitTests.Utils;

namespace TextProcessor.UnitTests.PipelineTests.Data;

public class StepWithOrderZeroLogger : TestLogger<StepWithOrderZero>
{
}

[TextProcessor.Attributes.Order(0)]
public class StepWithOrderZero : TextProcessingPipelineStep
{
    public StepWithOrderZero(StepWithOrderZeroLogger logger) : base(logger)
    {
    }

    public override Task<string> ExecuteAsync(string text, TextProcessorSettings settings) =>
        throw new NotImplementedException();
}