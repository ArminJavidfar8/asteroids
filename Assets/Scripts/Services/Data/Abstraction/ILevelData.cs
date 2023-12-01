namespace Services.Data.Abstraction
{
    public interface ILevelData
    {
        int SmallAsteroids { get; }
        int MediumAsteroids { get; }
        int LevelSize { get; }
        int LargeAsteroids { get; }
        float AsteroidInstantiationRatio { get; }
        bool HasEnemySpaceship { get; }
    }
}