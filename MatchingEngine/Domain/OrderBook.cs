using System.Text;
using MatchingEngine.Application;
using MatchingEngine.Domain.Orders;

namespace MatchingEngine.Domain;

public class OrderBook
{
    private readonly ReaderWriterLockSlim _cacheLock = new();
    private readonly PricePriorityLinkedList _buyPriorityQueue;
    private readonly PricePriorityLinkedList _sellPriorityQueue;
    public OrderBook()
    {
        _buyPriorityQueue = new PricePriorityLinkedList(Side.Buy);
        _sellPriorityQueue = new PricePriorityLinkedList(Side.Sell);
    }

    public string GetAsString()
    {
        return $"[{_sellPriorityQueue.GetTree()}] --- [{_buyPriorityQueue.GetTree()}]";
    }
    
    public void HandleNewOrder(IOrder order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));
        _cacheLock.EnterWriteLock();
        try
        {
            HandleNewOrder(order as dynamic);
        }
        finally
        {
            _cacheLock.ExitWriteLock();
        }
    }
    
    private void HandleNewOrder(LimitOrder order)
    {
        if (order.Side == Side.Buy)
            ExecuteBuyOrder(order);
        else
            ExecuteSellOrder(order);
    }

    private void ExecuteBuyOrder(LimitOrder order)
    {
        if (_sellPriorityQueue.Head != null && _sellPriorityQueue.Head.Order.Price <= order.Price)
        {
            if (_sellPriorityQueue.Head.Order.Quantity == order.Quantity) // all matched
            {
                _sellPriorityQueue.RemoveHead();
            }
            else if (_sellPriorityQueue.Head.Order.Quantity < order.Quantity) // not enough sell orders
            {
                while (order.Quantity != 0 && _sellPriorityQueue.Head != null &&
                       _sellPriorityQueue.Head.Order.Price <= order.Price) // find all matchs
                {
                    if (_sellPriorityQueue.Head.Order.Quantity == order.Quantity) // all matched
                    {
                        _sellPriorityQueue.RemoveHead();
                    }
                    else if (_sellPriorityQueue.Head.Order.Quantity < order.Quantity) // not enough sell orders
                    {
                        order.Quantity -= _sellPriorityQueue.Head.Order.Quantity;
                        _sellPriorityQueue.RemoveHead();
                    }
                    else // deduction
                    {
                        _sellPriorityQueue.Head.Order.Quantity -= order.Quantity;
                        order.Quantity = 0;
                    }
                }

                if (order.Quantity != 0) _buyPriorityQueue.AddOrder(order);
            }
            else
            {
                _sellPriorityQueue.Head.Order.Quantity -= order.Quantity;
            }
        }
        else
        {
            _buyPriorityQueue.AddOrder(order);
        }
    }
    
    private void ExecuteSellOrder(LimitOrder order)
    {
        if (_buyPriorityQueue.Head != null && _buyPriorityQueue.Head.Order.Price >= order.Price)
        {
            if (_buyPriorityQueue.Head.Order.Quantity == order.Quantity) // all matched
            {
                _buyPriorityQueue.RemoveHead();
            }
            else if (_buyPriorityQueue.Head.Order.Quantity < order.Quantity) // not enough sell orders
            {
                while (order.Quantity != 0 && _buyPriorityQueue.Head != null &&
                       _buyPriorityQueue.Head.Order.Price >= order.Price) // find all matchs
                {
                    if (_buyPriorityQueue.Head.Order.Quantity == order.Quantity) // all matched
                    {
                        _buyPriorityQueue.RemoveHead();
                    }
                    else if (_buyPriorityQueue.Head.Order.Quantity < order.Quantity) // not enough sell orders
                    {
                        order.Quantity -= _buyPriorityQueue.Head.Order.Quantity;
                        _buyPriorityQueue.RemoveHead();
                    }
                    else // deduction
                    {
                        _buyPriorityQueue.Head.Order.Quantity -= order.Quantity;
                        order.Quantity = 0;
                    }
                }

                if (order.Quantity != 0) _sellPriorityQueue.AddOrder(order);
            }
            else
            {
                _buyPriorityQueue.Head.Order.Quantity -= order.Quantity;
            }
        }
        else
        {
            _sellPriorityQueue.AddOrder(order);
        }
    }

    private void HandleNewOrder(MarketOrder order)
    {
        throw new NotImplementedException();
    }
    
    // Note: Here is not obvious again.. OB can contain several orders for a ticker + as sell as buy.
    // Which one should be canceled? There is no enough information for decision.
    // So I'm canceling them all because I don't know proper way to make it
    private void HandleNewOrder(CancelOrder order)
    {
        _buyPriorityQueue.RemoveNode(order.OrderId);
        _sellPriorityQueue.RemoveNode(order.OrderId);
    }
}