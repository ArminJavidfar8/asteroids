using Management.Abstraction;
using Services.Abstraction;
using Services.CoroutineSystem.Abstractio;
using System.Collections;
using UnityEngine;

namespace Management.AI
{
    public class EnemyManager
    {
        private readonly IAsteroidsService _asteroidsService;
        private readonly ILevelService _levelService;

        public EnemyManager(IAsteroidsService asteroidsService, ICoroutineService coroutineService, ILevelService levelService)
        {
            _asteroidsService = asteroidsService;
            _levelService = levelService;
            coroutineService.StartCoroutine(GenerateAsteroids());
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
                
                ++createdAsteroids;
                yield return waitForSeconds;
            }
        }
    }
}