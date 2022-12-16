namespace TextProcessor.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class OrderAttribute : Attribute
{
    public int Order { get; }

    public OrderAttribute(int order)
    {
        Order = order;
    }
}