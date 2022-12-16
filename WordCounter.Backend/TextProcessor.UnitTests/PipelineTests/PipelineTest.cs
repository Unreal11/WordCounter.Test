using TextProcessor.Core;
using TextProcessor.Extensions;
using TextProcessor.UnitTests.PipelineTests.Data;
using TextProcessor.UnitTests.Utils;

namespace TextProcessor.UnitTests.PipelineTests;

public class Tests
{
    [Test(Description = "Тест получения порядка выполнения шага")]
    public void TestOrderGetter()
    {
        var stepWithoutOrder = new StepWithoutOrder(new StepWithoutOrderLogger());

        Assert.True(stepWithoutOrder.GetOrder() < 0);
    }

    [Test(Description = "Тест очередей")]
    public void TestOrderSequence()
    {
        var provider = new TestProvider();
        var stepWithOrderOne = new StepWithOrderOne(new StepWithOrderOneLogger());
        var stepWithOrderZero = new StepWithOrderZero(new StepWithOrderZeroLogger());
        var stepWithOrderNegative = new StepWithOrderNegative(new StepWithOrderNegativeLogger());
        var stepWithoutOrder = new StepWithoutOrder(new StepWithoutOrderLogger());
   
        TestAddSequence(provider, 
            stepWithOrderOne, 
            stepWithOrderZero, 
            stepWithOrderNegative);
        
        TestAddSequence(provider, 
            stepWithOrderOne, 
            stepWithOrderZero, 
            stepWithoutOrder);
        
        TestAddSequence(provider, 
            stepWithoutOrder,
            stepWithOrderOne, 
            stepWithOrderZero);
        
        TestAddSequence(provider, 
            stepWithoutOrder,
            stepWithOrderOne, 
            stepWithOrderNegative,
            stepWithOrderZero);
    }
    
    [Test(Description = "Тест нарушения очерёдности шагов обработки текста")]
    public void TestOrder()
    {
        var provider = new TestProvider();
        
        Assert.Catch<IncorrectOrderException>(() =>
        {
            TextProcessingPipelineBuilder.Create(provider)
                .AddStep<StepWithOrderZero>()
                .AddStep<StepWithOrderOne>();
        });
    }
    
    private void TestAddSequence(TestProvider provider, 
        params TextProcessingPipelineStep[] steps) 
    {
        var builder = TextProcessingPipelineBuilder.Create(provider);
        
        foreach (var step in steps)
        {
            builder = builder.AddStep(step);
        }

        var pipeline = builder.Build();

        for (int i = 0; i < steps.Length; i++)
        {
            var pipelineStep = pipeline.ElementAt(i);
            
            Assert.True(pipelineStep == steps[i]);
        }
    }
}