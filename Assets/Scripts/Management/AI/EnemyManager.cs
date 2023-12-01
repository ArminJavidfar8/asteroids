using Management.Abstraction;
using Services.Abstraction;
using Services.Abstraction.Spaceship;
using Services.CoroutineSystem.Abstractio;
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
            eventService.RegisterEvent(EventTypes.OnLevelFinished, LevelFinished);
        }

        private void LevelFinished()
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
            int totalAsteroids = _levelService.TotalAsteroids;
            int levelSize = _levelService.LevelSize;
            WaitForSeconds waitForSeconds = new WaitForSeconds(_levelService.AsteroidInstantiationRatio);
            int createdAsteroids = 0;
            while (createdAsteroids < totalAsteroids)
            {
                AsteroidType randomType = _asteroidsService.GetRandomAsteroidType();
                IAsteroid asteroid = _asteroidsService.AddAsteroid(randomType);

                Vector3 position = Random.insideUnitCircle.normalized * levelSize;
                position.z = 0;
                asteroid.SetPosition(position);
                asteroid.MoveProperly();

                ++createdAsteroids;

                if (_levelService.HasEnemySpaceship && !_enemyCreated && createdAsteroids >= totalAsteroids / 2)
                {
                    Vector2 enemyPosition = Random.insideUnitCircle.normalized * levelSize;
                    _spaceshipService.CreateEnemy(enemyPosition);
                    _enemyCreated = true;
                }
                yield return waitForSeconds;
            }
        }
    }
}