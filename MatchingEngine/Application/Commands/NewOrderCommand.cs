using MatchingEngine.Domain.Orders;

namespace MatchingEngine.Application.Commands;

public readonly record struct NewOrderCommand(string OrderInfo)
{
    public IOrder GetDomainObject()
    {
        var order = new OrderFactory().Parse(OrderInfo);
        if (order == null)
            throw new ArgumentException($"Impossible to parse incoming order: {OrderInfo}");
        
        return order;
    }
}