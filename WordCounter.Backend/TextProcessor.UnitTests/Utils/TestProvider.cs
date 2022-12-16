using TextProcessor.UnitTests.PipelineTests.Data;

namespace TextProcessor.UnitTests.Utils;

public class TestProvider : IServiceProvider
{
    public List<object?> Services { get; }
    
    public TestProvider()
    {
        Services = new List<object?>();
    }
    
    public object? GetService(Type serviceType)
    {
        if (serviceType.IsAssignableFrom(typeof(TestLogger)))
        {
            var service = new TestLogger();
            Services.Add(service);
            
            return service;
        }

        if (serviceType == typeof(ILogger<StepWithOrderZero>))
        {
            var service = new StepWithOrderZeroLogger();
            Services.Add(service);

            return service;
        }
        
        if (serviceType == typeof(ILogger<StepWithOrderOne>))
        {
            var service = new StepWithOrderOneLogger();
            Services.Add(service);

            return service;
        }
        
        if (serviceType == typeof(ILogger<StepWithoutOrder>))
        {
            var service = new StepWithoutOrderLogger();
            Services.Add(service);

            return service;
        }
        
        if (serviceType == typeof(ILogger<StepWithOrderNegative>))
        {
            var service = new StepWithOrderNegativeLogger();
            Services.Add(service);

            return service;
        }

        throw new Exception($"TestProvider не может создать экземпляр сервиса с типом {serviceType.Name}");
    }
}