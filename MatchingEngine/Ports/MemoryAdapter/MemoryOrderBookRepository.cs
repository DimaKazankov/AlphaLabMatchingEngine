using MatchingEngine.Application;
using MatchingEngine.Domain;

namespace MatchingEngine.Ports.MemoryAdapter;

public sealed class MemoryOrderBookRepository : IOrderBookRepository
{
    private static MemoryOrderBookRepository? _instance;
    
    // Memory state of the OrderBookRepository
    private readonly OrderBook _orderBook;

    private MemoryOrderBookRepository()
    {
        _orderBook = new OrderBook();
    }

    public static MemoryOrderBookRepository Instance => _instance ??= new MemoryOrderBookRepository();
    public OrderBook Get()
    {
        return Instance._orderBook;
    }
}