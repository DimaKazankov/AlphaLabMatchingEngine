using MatchingEngine.Domain;
using MatchingEngine.Domain.Orders;
using NUnit.Framework;

namespace MatchingEngine.Tests.Domain.Orders;

public class PriceSideComparerTests
{
    [TestCase(5, 5, Side.Buy, 0)]
    [TestCase(5, 15, Side.Buy, 1)]
    [TestCase(15, 5, Side.Buy, -1)]
    [TestCase(5, 5, Side.Sell, 0)]
    [TestCase(5, 15, Side.Sell, -1)]
    [TestCase(15, 5, Side.Sell, 1)]
    public void BuyOrdersCompareTest(int firstPrice, int secondPrice, Side side, int expectedResult)
    {
        var comparer = new PriceSideComparer(side);
        var result = comparer.Compare(firstPrice, secondPrice);
        
        Assert.AreEqual(expectedResult, result);
    }
}