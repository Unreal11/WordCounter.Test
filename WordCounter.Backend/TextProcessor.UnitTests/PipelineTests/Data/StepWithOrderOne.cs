using TextProcessor.Core;
using TextProcessor.UnitTests.Utils;

namespace TextProcessor.UnitTests.PipelineTests.Data;

public class StepWithOrderOneLogger : TestLogger<StepWithOrderOne>
{
}

[Order(1)]
public class StepWithOrderOne : TextProcessingPipelineStep
{
    public StepWithOrderOne(StepWithOrderOneLogger logger) : base(logger)
    {
    }

    public override Task<string> ExecuteAsync(string text, TextProcessorSettings settings) =>
        throw new NotImplementedException();
}