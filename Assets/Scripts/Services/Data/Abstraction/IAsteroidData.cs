using Services.Abstraction;

namespace Services.Data.Abstraction
{
    public interface IAsteroidData
    {
        float DamageToPlayer {  get; }
        AsteroidType AsteroidType { get; }
        int MaxHealth { get; }
        int BreakDownScore { get; }
        int MoveForce { get; }
    }
}