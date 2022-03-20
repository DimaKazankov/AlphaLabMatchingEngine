using MatchingEngine.Domain;
using MatchingEngine.Domain.Orders;
using NUnit.Framework;

namespace MatchingEngine.Tests.Domain;

public class PricePriorityLinkedListTests
{
    [Test]
    public void AddOrdersByBuyPricePriorityTest()
    {
        const Side side = Side.Buy;
        var limitOrder1 = new LimitOrder(200, 45, "AAAA", side);
        var limitOrder2 = new LimitOrder(1200, 45, "AAAA", side);
        var limitOrder3 = new LimitOrder(200, 35, "BBBB", side);
        var limitOrder4 = new LimitOrder(500, 55, "BBBB", side);
        var limitOrder5 = new LimitOrder(200, 25, "CCCC", side);
        var limitOrder6 = new LimitOrder(400, 145, "CcCc", side);
        
        var linkedList = new PricePriorityLinkedList(side);
        linkedList.AddOrder(limitOrder1);
        linkedList.AddOrder(limitOrder2);
        linkedList.AddOrder(limitOrder3);
        linkedList.AddOrder(limitOrder4);
        linkedList.AddOrder(limitOrder5);
        linkedList.AddOrder(limitOrder6);
        
        Assert.IsTrue(ReferenceEquals(limitOrder6, linkedList.Head.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder4, linkedList.Head.Next.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder1, linkedList.Head.Next.Next.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder2, linkedList.Head.Next.Next.Next.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder3, linkedList.Head.Next.Next.Next.Next.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder5, linkedList.Head.Next.Next.Next.Next.Next.Order));
    }
    
    [Test]
    public void AddOrdersBySellPricePriorityTest()
    {
        const Side side = Side.Sell;
        var limitOrder1 = new LimitOrder(200, 45, "AAAA", side);
        var limitOrder2 = new LimitOrder(1200, 45, "AAAA", side);
        var limitOrder3 = new LimitOrder(200, 35, "BBBB", side);
        var limitOrder4 = new LimitOrder(500, 55, "BBBB", side);
        var limitOrder5 = new LimitOrder(200, 25, "CCCC", side);
        var limitOrder6 = new LimitOrder(400, 145, "CcCc", side);
        
        var linkedList = new PricePriorityLinkedList(side);
        linkedList.AddOrder(limitOrder1);
        linkedList.AddOrder(limitOrder2);
        linkedList.AddOrder(limitOrder3);
        linkedList.AddOrder(limitOrder4);
        linkedList.AddOrder(limitOrder5);
        linkedList.AddOrder(limitOrder6);
        
        Assert.IsTrue(ReferenceEquals(limitOrder5, linkedList.Head.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder3, linkedList.Head.Next.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder1, linkedList.Head.Next.Next.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder2, linkedList.Head.Next.Next.Next.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder4, linkedList.Head.Next.Next.Next.Next.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder6, linkedList.Head.Next.Next.Next.Next.Next.Order));
    }
    
    [Test]
    public void RemoceOrdersBySellPricePriorityTest()
    {
        const Side side = Side.Sell;
        var limitOrder1 = new LimitOrder(200, 45, "AAAA", side);
        var limitOrder2 = new LimitOrder(1200, 45, "AAAA", side);
        var limitOrder3 = new LimitOrder(200, 35, "BBBB", side);
        var limitOrder4 = new LimitOrder(500, 55, "BBBB", side);
        var limitOrder5 = new LimitOrder(200, 25, "CCCC", side);
        var limitOrder6 = new LimitOrder(400, 145, "CcCc", side);
        
        var linkedList = new PricePriorityLinkedList(side);
        linkedList.AddOrder(limitOrder1);
        linkedList.AddOrder(limitOrder2);
        linkedList.AddOrder(limitOrder3);
        linkedList.AddOrder(limitOrder4);
        linkedList.AddOrder(limitOrder5);
        linkedList.AddOrder(limitOrder6);
        
        linkedList.RemoveNode(limitOrder2.OrderId);
        
        Assert.IsTrue(ReferenceEquals(limitOrder5, linkedList.Head.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder3, linkedList.Head.Next.Order));
        // Assert.IsTrue(ReferenceEquals(limitOrder1, linkedList.Head.Next.Next.Order));
        // Assert.IsTrue(ReferenceEquals(limitOrder2, linkedList.Head.Next.Next.Next.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder4, linkedList.Head.Next.Next.Order));
        Assert.IsTrue(ReferenceEquals(limitOrder6, linkedList.Head.Next.Next.Next.Order));
    }
}