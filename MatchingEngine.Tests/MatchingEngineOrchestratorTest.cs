using System.Threading.Tasks;
using MatchingEngine.Application;
using MatchingEngine.Application.Commands;
using NUnit.Framework;

namespace MatchingEngine.Tests;

public class MatchingEngineOrchestratorTest
{
    [Test]
    public void HandlingNewOrderSimple2Test()
    {
        var orderBookRepositoryFactory = new OrderBookRepositoryFactory();
        var matchingEngine = new MatchingEngineOrchestrator(orderBookRepositoryFactory);
        matchingEngine.HandleNewOrder(new NewOrderCommand("SUB LO B Ffuj 200 13"));
        matchingEngine.HandleNewOrder(new NewOrderCommand("SUB LO B Yy7P 150 11"));
        matchingEngine.HandleNewOrder(new NewOrderCommand("SUB LO B YuFU 100 13"));
        matchingEngine.HandleNewOrder(new NewOrderCommand("SUB LO S IpD8 150 14"));
        matchingEngine.HandleNewOrder(new NewOrderCommand("SUB LO S y93N 190 15"));
        matchingEngine.HandleNewOrder(new NewOrderCommand("SUB LO B Y5wb 230 14"));
        // matchingEngine.HandleNewOrder(new NewOrderCommand("SUB MO B IZLO 250"));
        matchingEngine.HandleNewOrder(new NewOrderCommand("CXL Ffuj"));
        matchingEngine.HandleNewOrder(new NewOrderCommand("CXL 49Ze"));

        var repository = orderBookRepositoryFactory.Get();
        var orderBook = repository.Get();
        
        Assert.AreEqual("[Sell: ['190@15#y93N']] --- [Buy: ['80@14#Y5wb', '100@13#YuFU', '150@11#Yy7P']]", orderBook.GetAsString());
    }
    
    [TestCase("")]
    public void HandlingNewOrderTest(string input)
    {
        var orderBookRepositoryFactory = new OrderBookRepositoryFactory();
        
        var matchingEngine1 = new MatchingEngineOrchestrator(new OrderBookRepositoryFactory());
        var matchingEngine2 = new MatchingEngineOrchestrator(orderBookRepositoryFactory);

        var tasks = new[]
        {
            new Task(() => matchingEngine1.HandleNewOrder(new NewOrderCommand(input)))
        };
    }
}