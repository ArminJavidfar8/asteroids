using Management.Abstraction;
using Management.Asteroid;
using Services.Abstraction;
using Services.PoolSystem.Abstaction;
using Services.SpriteDatabaseSystem.Abstraction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Core
{
    public class AsteroidsService : IAsteroidsService
    {
        private readonly IPoolService _poolService;
        private readonly ISpriteDatabaseService _spriteDatabaseService;

        public AsteroidsService(IPoolService poolService, ISpriteDatabaseService spriteDatabaseService)
        {
            _poolService = poolService;
            _spriteDatabaseService = spriteDatabaseService;
        }

        public IAsteroid AddAsteroid(AsteroidType asteroidType)
        {
            IAsteroid asteroid = _poolService.GetGameObject(asteroidType.ToString()).GetComponent<IAsteroid>();
            
            return asteroid;
        }

        public Sprite GetRandomAsteroidSprite(AsteroidType asteroidType)
        {
            string spriteName = $"{asteroidType.ToString()}Asteroid_{UnityEngine.Random.Range(0, 4)}";
            return _spriteDatabaseService.GetSpriteByName(spriteName);
        }

        public void RemoveAsteroid(IAsteroid asteroid)
        {
            
        }
    }
}