using TextProcessor.Core;
using TextProcessor.UnitTests.Utils;

namespace TextProcessor.UnitTests.PipelineTests.Data;

public class StepWithoutOrderLogger : TestLogger<StepWithoutOrder>
{
}
 
[Order(0)]
public class StepWithoutOrder : TextProcessingPipelineStep
{
    public StepWithoutOrder(StepWithoutOrderLogger logger) : base(logger)
    {
    }

    public override Task<string> ExecuteAsync(string text, TextProcessorSettings settings) =>
        throw new NotImplementedException();
}