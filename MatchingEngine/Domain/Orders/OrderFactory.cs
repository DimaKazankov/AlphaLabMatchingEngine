using MatchingEngine.Application;

namespace MatchingEngine.Domain.Orders;

public class OrderFactory
{
    public IOrder? Parse(string orderInfo)
    {
        if (string.IsNullOrWhiteSpace(orderInfo))
            throw new ArgumentException("Null or empty parameter", nameof(orderInfo));

        var inputSplit = orderInfo.Split(" ");
        
        IOrder? GetByAction(IReadOnlyList<string> input)
        {
            if (input == null || input.Count == 0)
                throw new ArgumentException("Null or empty parameter", nameof(orderInfo));

            IOrder? GetByOrderType()
            {
                if (input.Count == 1)
                    throw new ArgumentException("Null or empty parameter", nameof(orderInfo));
                
                return input[1] switch
                {
                    "LO" => LimitOrder.FromStrings(input[4], input[5], input[3], input[2]),
                    "MO" => MarketOrder.FromStrings(input[4], input[3], input[2]),
                    _ => null
                };
            }

            return input[0] switch
            {
                "SUB" => GetByOrderType(),
                "CXL" => new CancelOrder(input[1]),
                _ => null
            };
        }

        return GetByAction(inputSplit);
    }
}