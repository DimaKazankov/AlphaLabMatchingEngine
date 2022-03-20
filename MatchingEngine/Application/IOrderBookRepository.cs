using MatchingEngine.Domain;

namespace MatchingEngine.Application;

public interface IOrderBookRepository
{
    // Notes: In the task there is no difference between executing tickers.
    // I mean one ticker can execute with another once. Maybe I don't understand something but seams it's wrong.
    // Anyway I'd prefer have an abstraction to get proper OrderBook (for example it can be changed by ticker)
    // By current requirements I'll keep just one as a singleton in memory
    OrderBook Get();
}