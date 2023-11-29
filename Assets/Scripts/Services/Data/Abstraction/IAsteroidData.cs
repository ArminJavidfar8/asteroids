using Services.Abstraction;

namespace Services.Data.Abstraction
{
    public interface IAsteroidData
    {
        AsteroidType AsteroidType { get; }
        int MaxHealth { get; }
        int BreakDownScore { get; }
    }
}