using MatchingEngine.Domain;
using MatchingEngine.Domain.Orders;
using NUnit.Framework;

namespace MatchingEngine.Tests.Domain.Orders;

public class OrderFactoryTests
{
    [Test]
    public void ParsingLimitOrderTest()
    {
        var factory = new OrderFactory();
        var order = factory.Parse("SUB LO B EUR 200 12") as LimitOrder;
        Assert.AreEqual(200, order.Quantity);
        Assert.AreEqual(12, order.Price);
        Assert.AreEqual("EUR", order.OrderId);
        Assert.AreEqual(Side.Buy, order.Side);
    }
    
    [Test]
    public void ParsingMarketOrderTest()
    {
        var factory = new OrderFactory();
        var order = factory.Parse("SUB MO S EUR 200 12") as MarketOrder;
        Assert.AreEqual(200, order.Quantity);
        Assert.AreEqual("EUR", order.OrderId);
        Assert.AreEqual(Side.Sell, order.Side);
    }
    
    [Test]
    public void ParsingCancelOrderTest()
    {
        var factory = new OrderFactory();
        var order = factory.Parse("CXL EUR") as CancelOrder;
        Assert.AreEqual("EUR", order.OrderId);
    }
}