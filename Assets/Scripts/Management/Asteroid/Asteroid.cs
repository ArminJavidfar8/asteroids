using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.CoroutineSystem.Abstractio;
using Services.Data;
using Services.Data.Abstraction;
using System.Collections;
using UnityEngine;

namespace Management.Asteroid
{
    public class Asteroid : MonoBehaviour, IAsteroid
    {
        [SerializeField] private AsteroidData _asteroidData;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody;
        private IAsteroidsService _asteroidsService;
        private ICoroutineService _coroutineService;
        private Transform _transform;
        private Coroutine _rangeCheckCoroutine;
        private int _levelSize;

        public string Name => _asteroidData.AsteroidType.ToString();
        public IAsteroidData AsteroidData => _asteroidData;
        public GameObject TheGameObject => gameObject;
        public Vector3 Position => _transform.position;

        public void Initialize()
        {
            _transform = transform;
            _asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();
            _coroutineService = ServiceHolder.ServiceProvider.GetService<ICoroutineService>();
            ILevelService levelService = ServiceHolder.ServiceProvider.GetService<ILevelService>();
            _levelSize = levelService.LevelSize;
        }

        public void OnGetFromPool()
        {
            SetSprite();
            if (_rangeCheckCoroutine != null )
            {
                _coroutineService.StopCoroutine(_rangeCheckCoroutine);
            }
            _rangeCheckCoroutine = _coroutineService.StartCoroutine(CheckAsteroidRange());
        }

        public void OnReleaseToPool()
        {
            if (_rangeCheckCoroutine != null)
            {
                _coroutineService.StopCoroutine(_rangeCheckCoroutine);
            }
        }

        public void MoveProperly()
        {
            Vector3 randomNoise = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            _rigidbody.AddForce((-transform.position + randomNoise) * _asteroidData.MoveForce);
        }

        public void BreakDown()
        {

        }

        private IEnumerator CheckAsteroidRange()
        {
            WaitForSeconds wait = new WaitForSeconds(0.5f);
            while (true) // TODO add asteroid health check to condition
            {
                yield return wait;
                // if the asteroid passed level size, take it back toward center of world
                if (_transform.position.sqrMagnitude > _levelSize * _levelSize)
                {
                    MoveProperly();
                }
            }
        }

        private void SetSprite()
        {
            _spriteRenderer.sprite = _asteroidsService.GetRandomAsteroidSprite(AsteroidData.AsteroidType);
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }
    }
}