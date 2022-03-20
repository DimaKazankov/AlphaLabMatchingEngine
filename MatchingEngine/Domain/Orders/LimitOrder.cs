using System.Text.RegularExpressions;
using MatchingEngine.Application;

namespace MatchingEngine.Domain.Orders;


// BuyOrder, SellOrder
// - Side, (enum of Buy or Sell)
// - OrderId (string) - ticker, case sensitive
// - Price (integer)
// - Quantity (integer)

// StockMarket

// OrderBook
// - 2 lists ordered by priority
// - for Buy orders higher price has higher priority
// - for Sell orders lower price has higher priority
// - if several the buy or sell orders have the same price then should be handled as FIFO 

// MatchingEngine maintain OrderBook

public class LimitOrder : IOrder
{
    public LimitOrder(int quantity, int price, string orderId, Side side)
    {
        this.Quantity = quantity;
        this.Price = price;
        this.OrderId = orderId;
        this.Side = side;
    }
    public int Quantity { get; set; }
    public int Price { get; }
    public string OrderId { get; }
    public Side Side { get; }
    public static LimitOrder FromString(string input, Side side)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new ArgumentException("Null or empty parameter", nameof(input));

        const string pattern = "^([0-9]+)@([0-9]+)#([\\w]*)";
        var match = Regex.Match(input, pattern);

        if (match == null)
            throw new ArgumentException($"Limit order cannot be created for the input parameters: {input}");

        if (!int.TryParse(match.Groups[1].Value, out var quantity) ||
            !int.TryParse(match.Groups[2].Value, out var price))
            throw new ArgumentException($"Limit order cannot be created for the input parameters: {input}");
        
        return new LimitOrder(quantity, price, match.Groups[3].Value, side);
    }
    
    public static LimitOrder FromStrings(string quantity, string price, string orderId, string side)
    {
        if (string.IsNullOrWhiteSpace(quantity) || !int.TryParse(quantity, out var q))
            throw new ArgumentException("Null or empty parameter", nameof(quantity));
        if (string.IsNullOrWhiteSpace(price) || !int.TryParse(price, out var p))
            throw new ArgumentException("Null or empty parameter", nameof(price));
        if (string.IsNullOrWhiteSpace(orderId))
            throw new ArgumentException("Null or empty parameter", nameof(orderId));
        if (string.IsNullOrWhiteSpace(side) || !new []{"B", "S"}.Contains(side))
            throw new ArgumentException("Null or empty parameter", nameof(side));
        
        return new LimitOrder(q, p, orderId, side == "B" ? Side.Buy : Side.Sell);
    }

    public override string ToString()
    {
        return $"{Quantity}@{Price}#{OrderId}";
    }
}