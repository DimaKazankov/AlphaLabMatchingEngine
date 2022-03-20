using MatchingEngine.Application.Commands;

namespace MatchingEngine.Application;

public class MatchingEngineOrchestrator
{
    private readonly IOrderBookRepository _orderBookRepository;

    public MatchingEngineOrchestrator(OrderBookRepositoryFactory orderBookRepositoryFactory)
    {
        if (orderBookRepositoryFactory == null) throw new ArgumentNullException(nameof(orderBookRepositoryFactory));
        _orderBookRepository = orderBookRepositoryFactory.Get();
    }
    
    public void HandleNewOrder(NewOrderCommand command)
    {
        var order = command.GetDomainObject();
        var orderBook = _orderBookRepository.Get();
        orderBook.HandleNewOrder(order);
    }
}