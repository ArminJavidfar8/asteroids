using Management.Abstraction;
using UnityEngine;

namespace Services.Abstraction
{
    public enum AsteroidType
    {
        None,
        Small,
        Medium,
        Large
    }
    public interface IAsteroidsService
    {
        IAsteroid AddAsteroid(AsteroidType asteroidType);
        void RemoveAsteroid(IAsteroid asteroid);
        Sprite GetRandomAsteroidSprite(AsteroidType asteroidType);
        AsteroidType GetRandomAsteroidType();
    }
}