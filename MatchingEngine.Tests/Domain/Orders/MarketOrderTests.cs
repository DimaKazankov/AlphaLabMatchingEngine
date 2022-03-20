using System;
using MatchingEngine.Domain;
using MatchingEngine.Domain.Orders;
using NUnit.Framework;

namespace MatchingEngine.Tests.Domain.Orders;

public class MarketOrderTests
{
    [Test]
    public void FromStringFactoryTest()
    {
        var order = MarketOrder.FromStrings("500", "EURUSD", "B");
        Assert.AreEqual(new MarketOrder(500, "EURUSD", Side.Buy), order);
    }
    
    [TestCase("", "", "")]
    [TestCase("DDD", "", "")]
    [TestCase("500", "", "")]
    [TestCase("500", "EURUSD", "")]
    [TestCase("500", "EURUSD", "BB")]
    [TestCase(null, null, null)]
    [TestCase("500", null, null)]
    [TestCase("500", "EURUSD", null)]
    public void FromStringFactoryArgumentExceptionTest(string quantity, string orderId, string side)
    {
        Assert.Throws<ArgumentException>(() => MarketOrder.FromStrings(quantity, orderId, side));
    }
}