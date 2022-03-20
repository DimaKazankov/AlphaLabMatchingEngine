using MatchingEngine.Application;

namespace MatchingEngine.Domain.Orders;

public record MarketOrder(int Quantity, string OrderId, Side Side) : IOrder
{
    public static MarketOrder FromStrings(string quantity, string orderId, string side)
    {
        if (string.IsNullOrWhiteSpace(quantity) || !int.TryParse(quantity, out var q))
            throw new ArgumentException("Null or empty parameter", nameof(quantity));
        if (string.IsNullOrWhiteSpace(orderId))
            throw new ArgumentException("Null or empty parameter", nameof(orderId));
        if (string.IsNullOrWhiteSpace(side) || !new []{"B", "S"}.Contains(side))
            throw new ArgumentException("Null or empty parameter", nameof(side));
        
        return new MarketOrder(q, orderId, side == "B" ? Side.Buy : Side.Sell);
    }
}