using MatchingEngine.Application;

namespace MatchingEngine.Domain.Orders;

public record CancelOrder(string OrderId) : IOrder;