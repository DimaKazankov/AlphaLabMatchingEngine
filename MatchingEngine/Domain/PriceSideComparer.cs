namespace MatchingEngine.Domain;

public class PriceSideComparer : IComparer<int>
{
    private readonly Side _side;

    public PriceSideComparer(Side side)
    {
        _side = side;
    }
    public int Compare(int x, int y)
    {
        var compareResult = _side == Side.Buy ? 1 : -1;
        return x == y ? 0 : x < y ? compareResult : -1 * compareResult;
    }
}