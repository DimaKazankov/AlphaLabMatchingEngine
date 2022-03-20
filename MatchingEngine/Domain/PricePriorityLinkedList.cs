using System.Text;
using MatchingEngine.Domain.Orders;

namespace MatchingEngine.Domain;

public class PricePriorityLinkedList
{
    public class PricePriorityLinkedListNode
    {
        public LimitOrder Order { get; }
        public PricePriorityLinkedListNode? Next { get; set; } = null;
        public PricePriorityLinkedListNode? Prev { get; set; } = null;
    
        public PricePriorityLinkedListNode(LimitOrder order)
        {
            Order = order;
        }
    }
    
    private readonly Side _side;
    private readonly Dictionary<string, Queue<PricePriorityLinkedListNode>> _orders = new();

    public PricePriorityLinkedList(Side side)
    {
        _side = side;
    }
    
    public PricePriorityLinkedListNode? Head { get; private set; }
    public PricePriorityLinkedListNode? Tail { get; private set; }

    public string GetTree()
    {
        var builder = new StringBuilder();
        var h = Head;
        builder.Append($"{_side}: [");
        while (h != null)
        {
            if (h.Next == null)
            {
                builder.Append($"'{h.Order}'");
                break;
            }
            builder.Append($"'{h.Order}', ");
            h = h.Next;
        }
        builder.Append("]");
        return builder.ToString();
    }

    public void AddOrder(LimitOrder order)
    {
        var node = new PricePriorityLinkedListNode(order);
        if (Head == null)
        {
            Head = node;
            Tail = Head;
            var queue = new Queue<PricePriorityLinkedListNode>();
            queue.Enqueue(node);
            _orders[order.OrderId] = queue;
            return;
        }

        switch (_side)
        {
            case Side.Buy:
                if (order.Price > Head.Order.Price)
                {
                    Head.Prev = node;
                    node.Next = Head;
                    Head = node;
                }
                else if (order.Price <= Tail.Order.Price)
                {
                    Tail.Next = node;
                    node.Prev = Tail;
                    Tail = node;
                }
                else
                {
                    var priorityItem = Tail;
                    while (node.Order.Price > priorityItem.Order.Price)
                    {
                        priorityItem = priorityItem.Prev;
                    }

                    node.Next = priorityItem.Next;
                    priorityItem.Next = node;
                    node.Prev = priorityItem;
                    node.Next.Prev = node;
                }
                break;
            case Side.Sell:
                if (order.Price < Head.Order.Price)
                {
                    Head.Prev = node;
                    node.Next = Head;
                    Head = node;
                }
                else if (order.Price >= Tail.Order.Price)
                {
                    Tail.Next = node;
                    node.Prev = Tail;
                    Tail = node;
                }
                else
                {
                    var priorityItem = Tail;
                    while (node.Order.Price < priorityItem.Order.Price)
                    {
                        priorityItem = priorityItem.Prev;
                    }

                    node.Next = priorityItem.Next;
                    priorityItem.Next = node;
                    node.Prev = priorityItem;
                    node.Next.Prev = node;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (_orders.TryGetValue(order.OrderId, out var q))
        {
            q.Enqueue(node);
        }
        else
        {
            var queue = new Queue<PricePriorityLinkedListNode>();
            queue.Enqueue(node);
            _orders[order.OrderId] = queue;
        }
    }

    public void RemoveHead()
    {
        if (Head == null)
            return;
        
        if (Head.Next == null)
        {
            Head = null;
            Tail = Head;
            return;
        }

        Head = Head.Next;
    }

    public void RemoveTail()
    {
        if (Tail == null)
            return;

        if (Tail.Prev == null)
        {
            Head = null;
            Tail = Head;
            return;
        }

        Tail = Tail.Prev;
    }

    // Note: Don't think it is right to cancel all by ticker. But this is how I understood the requirements.
    public void RemoveNode(string orderId)
    {
        if (Head == null)
            return;

        if (_orders.TryGetValue(orderId, out var queue))
        {
            while (queue.TryDequeue(out var node))
            {
                if (node.Next == null)
                    node.Prev.Next = null;
                else
                    node.Next.Prev = node.Prev;

                if (node.Prev == null)
                    Head.Next = node.Next;
                else
                    node.Prev.Next = node.Next;
            }
        }
        _orders.Remove(orderId);
    }
}