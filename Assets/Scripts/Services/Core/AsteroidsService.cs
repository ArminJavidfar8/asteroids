using Management.Abstraction;
using Management.Asteroid;
using Services.Abstraction;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using Services.PoolSystem.Abstaction;
using Services.SpriteDatabaseSystem.Abstraction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services.Core
{
    public class AsteroidsService : IAsteroidsService
    {
        private readonly IPoolService _poolService;
        private readonly ISpriteDatabaseService _spriteDatabaseService;
        private readonly List<IAsteroid> _createdAsteroids;

        public AsteroidsService(IPoolService poolService, ISpriteDatabaseService spriteDatabaseService, IEventService eventService)
        {
            _poolService = poolService;
            _spriteDatabaseService = spriteDatabaseService;
            _createdAsteroids = new List<IAsteroid>();
            eventService.RegisterEvent(EventTypes.OnLevelStarted, LevelStarted);
        }

        public IAsteroid AddAsteroid(AsteroidType asteroidType)
        {
            IAsteroid asteroid = _poolService.GetGameObject(asteroidType.ToString()).GetComponent<IAsteroid>();
            _createdAsteroids.Add(asteroid);

            return asteroid;
        }

        public Sprite GetRandomAsteroidSprite(AsteroidType asteroidType)
        {
            string spriteName = $"{asteroidType.ToString()}Asteroid_{UnityEngine.Random.Range(0, 4)}";
            return _spriteDatabaseService.GetSpriteByName(spriteName);
        }

        public AsteroidType GetRandomAsteroidType()
        {
            Array types = Enum.GetValues(typeof(AsteroidType));
            int randomIndex = UnityEngine.Random.Range(1, types.Length);
            return (AsteroidType)types.GetValue(randomIndex);
        }

        public void RemoveAsteroid(IAsteroid asteroid)
        {
            _poolService.ReleaseGameObject(asteroid.TheGameObject);
        }

        private void LevelStarted()
        {
            RemoveAllAsteroids();
        }

        private void RemoveAllAsteroids()
        {
            while (_createdAsteroids.Count > 0)
            {
                RemoveAsteroid(_createdAsteroids[0]);
                _createdAsteroids.RemoveAt(0);
            }
        }

    }
}