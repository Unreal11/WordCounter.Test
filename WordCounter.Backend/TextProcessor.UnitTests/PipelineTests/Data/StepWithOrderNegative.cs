using TextProcessor.Core;
using TextProcessor.UnitTests.Utils;

namespace TextProcessor.UnitTests.PipelineTests.Data;

public class StepWithOrderNegativeLogger : TestLogger<StepWithOrderNegative>
{
}
 
[Order(-1)]
public class StepWithOrderNegative : TextProcessingPipelineStep
{
    public StepWithOrderNegative(StepWithOrderNegativeLogger logger) : base(logger)
    {
    }

    public override Task<string> ExecuteAsync(string text, TextProcessorSettings settings) =>
        throw new NotImplementedException();
}