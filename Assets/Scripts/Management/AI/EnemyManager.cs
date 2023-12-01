using Management.Abstraction;
using Services.Abstraction;
using Services.Abstraction.Spaceship;
using Services.CoroutineSystem.Abstractio;
using Services.Data.Abstraction;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Management.AI
{
    public class EnemyManager
    {
        private readonly IAsteroidsService _asteroidsService;
        private readonly ILevelService _levelService;
        private readonly ICoroutineService _coroutineService;
        private readonly ISpaceshipService _spaceshipService;
        private Coroutine _generateAsteroidsRoutine;
        private bool _enemyCreated;

        public EnemyManager(IAsteroidsService asteroidsService, ICoroutineService coroutineService, ILevelService levelService, IEventService eventService, ISpaceshipService spaceshipService)
        {
            _asteroidsService = asteroidsService;
            _levelService = levelService;
            _coroutineService = coroutineService;
            _spaceshipService = spaceshipService;
            eventService.RegisterEvent(EventTypes.OnLevelStarted, LevelStarted);
            eventService.RegisterEvent<bool>(EventTypes.OnLevelFinished, LevelFinished);
        }

        private void LevelFinished(bool won)
        {
            if (_generateAsteroidsRoutine != null)
            {
                _coroutineService.StopCoroutine(_generateAsteroidsRoutine);
            }
        }

        private void LevelStarted()
        {
            if (_generateAsteroidsRoutine != null)
            {
                _coroutineService.StopCoroutine(_generateAsteroidsRoutine);
            }
            _generateAsteroidsRoutine = _coroutineService.StartCoroutine(GenerateAsteroids());

            _enemyCreated = false;
        }

        private IEnumerator GenerateAsteroids()
        {
            ILevelData currentLevelData = _levelService.CurrentLevelData;
            List<AsteroidType> astroids = GetAstroidTypesList(currentLevelData);
            
            int levelSize = _levelService.CurrentLevelData.LevelSize;
            WaitForSeconds waitForSeconds = new WaitForSeconds(_levelService.CurrentLevelData.AsteroidInstantiationRatio);
            int createdAsteroids = 0;
            for (int i = 0; i < astroids.Count; i++)
            {
                IAsteroid asteroid = _asteroidsService.AddAsteroid(astroids[i]);

                Vector3 position = Random.insideUnitCircle.normalized * levelSize;
                position.z = 0;
                asteroid.SetPosition(position);
                asteroid.MoveProperly();

                ++createdAsteroids;

                if (_levelService.CurrentLevelData.HasEnemySpaceship && !_enemyCreated && createdAsteroids >= astroids.Count / 2)
                {
                    Vector2 enemyPosition = Random.insideUnitCircle.normalized * levelSize;
                    _spaceshipService.CreateEnemy(enemyPosition);
                    _enemyCreated = true;
                }
                yield return waitForSeconds;
            }
        }

        private List<AsteroidType> GetAstroidTypesList(ILevelData currentLevelData)
        {
            List<AsteroidType> astroids = new List<AsteroidType>();
            for (int i = 0; i < currentLevelData.LargeAsteroids; i++)
            {
                astroids.Add(AsteroidType.Large);
            }
            for (int i = 0; i < currentLevelData.MediumAsteroids; i++)
            {
                astroids.Add(AsteroidType.Medium);
            }
            for (int i = 0; i < currentLevelData.SmallAsteroids; i++)
            {
                astroids.Add(AsteroidType.Small);
            }
            return astroids;
        }
    }
}