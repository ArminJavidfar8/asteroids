using Management.Abstraction;
using Services.Abstraction;
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
        private Coroutine _generateAsteroidsRoutine;

        public EnemyManager(IAsteroidsService asteroidsService, ICoroutineService coroutineService, ILevelService levelService, IEventService eventService)
        {
            _asteroidsService = asteroidsService;
            _levelService = levelService;
            _coroutineService = coroutineService;
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
                yield return waitForSeconds;
            }
        }
    }
}