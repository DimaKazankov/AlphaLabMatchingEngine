using MatchingEngine.Application;
using NUnit.Framework;

namespace MatchingEngine.Tests.Ports.MemoryAdapter;

public class OrderBookRepositoryTest
{
    [Test]
    public void SingletonStateTest()
    {
        var firstOrderBookRepository = new OrderBookRepositoryFactory().Get();
        var secondOrderBookRepository = new OrderBookRepositoryFactory().Get();
        
        Assert.IsTrue(ReferenceEquals(firstOrderBookRepository.Get(), secondOrderBookRepository.Get()));
    }
}