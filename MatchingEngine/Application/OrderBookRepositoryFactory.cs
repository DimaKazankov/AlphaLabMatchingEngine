using MatchingEngine.Ports.MemoryAdapter;

namespace MatchingEngine.Application;

public sealed class OrderBookRepositoryFactory
{
    public IOrderBookRepository Get()
    {
        return MemoryOrderBookRepository.Instance;
    }
}