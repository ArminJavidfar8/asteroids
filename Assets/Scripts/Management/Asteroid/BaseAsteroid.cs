using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.CoroutineSystem.Abstractio;
using Services.Data;
using Services.Data.Abstraction;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using Services.PoolSystem.Abstaction;
using Services.PoolSystem.Core;
using System.Collections;
using UnityEngine;

namespace Management.Asteroid
{
    public abstract class BaseAsteroid : MonoBehaviour, IAsteroid, IDamageable, IPoolable
    {
        [SerializeField] private AsteroidData _asteroidData;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Rigidbody2D _rigidbody;
        private ICoroutineService _coroutineService;
        protected IAsteroidsService _asteroidsService;
        protected IEventService _eventService;
        private Transform _transform;
        private Coroutine _rangeCheckCoroutine;
        private int _levelSize;
        private float _health;

        public string Name => _asteroidData.AsteroidType.ToString();
        public IAsteroidData AsteroidData => _asteroidData;
        public GameObject TheGameObject => gameObject;
        public Vector3 Position => _transform.position;

        public float MaxHealth => _asteroidData.MaxHealth;
        public float Health { get => _health; set => _health = value; }
        public bool IsAlive => Health > 0;

        public abstract void Die();

        public void Initialize()
        {
            _transform = transform;
            _asteroidsService = ServiceHolder.ServiceProvider.GetService<IAsteroidsService>();
            _coroutineService = ServiceHolder.ServiceProvider.GetService<ICoroutineService>();
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
            ILevelService levelService = ServiceHolder.ServiceProvider.GetService<ILevelService>();
            _levelSize = levelService.CurrentLevelData.LevelSize;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_asteroidData.DamageToPlayer);
                Die();
            }
        }

        public void OnGetFromPool()
        {
            Health = MaxHealth;
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

        public void Stop()
        {
            _rigidbody.velocity = Vector2.zero;
        }

        private IEnumerator CheckAsteroidRange()
        {
            WaitForSeconds wait = new WaitForSeconds(0.5f);
            while (IsAlive)
            {
                yield return wait;
                // if the asteroid passed level size, take it back
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

        public bool TakeDamage(float damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                Die();
                _eventService.BroadcastEvent(EventTypes.OnAstroidDestroyed, _asteroidData.BreakDownScore);
                return true;
            }
            return false;
        }

    }
}