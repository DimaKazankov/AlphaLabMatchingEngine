using System;
using MatchingEngine.Domain;
using MatchingEngine.Domain.Orders;
using NUnit.Framework;

namespace MatchingEngine.Tests.Domain.Orders;

public class LimitOrderTests
{
    [Test]
    public void FromStringFactoryTest()
    {
        var order = LimitOrder.FromString("500@5#EURUSD", Side.Buy);
        Assert.AreEqual(500, order.Quantity);
        Assert.AreEqual(5, order.Price);
        Assert.AreEqual("EURUSD", order.OrderId);
        Assert.AreEqual(Side.Buy, order.Side);
    }

    [TestCase("")]
    [TestCase(null)]
    [TestCase("BLA@BLA#BLA")]
    public void FromStringFactoryArgumentExceptionTest(string input)
    {
        Assert.Throws<ArgumentException>(() => LimitOrder.FromString(input, Side.Buy));
    }

    [Test]
    public void FromStringsFactoryTest()
    {
        var order = LimitOrder.FromStrings("500", "4", "EURUSD", "B");
        Assert.AreEqual(500, order.Quantity);
        Assert.AreEqual(4, order.Price);
        Assert.AreEqual("EURUSD", order.OrderId);
        Assert.AreEqual(Side.Buy, order.Side);
    }

    [TestCase("", "", "", "")]
    [TestCase("DDD", "", "", "")]
    [TestCase("500", "", "", "")]
    [TestCase("500", "S", "", "")]
    [TestCase("500", "5", "EURUSD", "BB")]
    [TestCase("500", "5", "EURUSD", "")]
    [TestCase("500", "5", "EURUSD", "BB")]
    [TestCase(null, null, null, null)]
    [TestCase("500", null, null, null)]
    [TestCase("500", "5", null, null)]
    [TestCase("500", "5", "EURUSD", null)]
    public void FromStringsFactoryArgumentExceptionTest(string quantity, string price, string orderId, string side)
    {
        Assert.Throws<ArgumentException>(() => LimitOrder.FromStrings(quantity, price, orderId, side));
    }
}