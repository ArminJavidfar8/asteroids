using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.CoroutineSystem.Abstractio;
using Services.Data.Abstraction;
using Services.PoolSystem.Abstaction;
using UnityEngine;

namespace Management.Weapon
{
    public class SimpleBullet : MonoBehaviour, IBullet, IPoolable
    {
        private const float BULLET_SPEED = 50;
        public const string POOL_NAME = "SimpleBullet";
        
        [SerializeField] private Rigidbody2D _rigidbody;
        private Transform _transform;
        private IWeaponData _weaponData;
        private IPoolService _poolService;
        private ICoroutineService _coroutineService;
        private Coroutine _releasingToPoolRoutine;

        public string Name => POOL_NAME;

        public void Initialize()
        {
            _transform = transform;
            _poolService = ServiceHolder.ServiceProvider.GetService<IPoolService>();
            _coroutineService = ServiceHolder.ServiceProvider.GetService<ICoroutineService>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_weaponData.Damage);
                _poolService.ReleaseGameObject(gameObject);
            }
        }

        public void OnGetFromPool()
        {
            _rigidbody.velocity = Vector3.zero;
            if (_releasingToPoolRoutine != null)
            {
                _coroutineService.StopCoroutine(_releasingToPoolRoutine);
            }
            _releasingToPoolRoutine = _coroutineService.StartDelayedTask(3, () => { if (gameObject.activeSelf) _poolService.ReleaseGameObject(gameObject); }); // after 5 seconds, it's too far and can get back to pool
        }

        public void OnReleaseToPool()
        {
            if (_releasingToPoolRoutine != null)
            {
                _coroutineService.StopCoroutine(_releasingToPoolRoutine);
            }
        }

        public void SetPosition(Vector3 position)
        {
            _transform.position = position;
        }

        public void ShootBullet(IWeaponData weaponData, Vector3 direction)
        {
            _weaponData = weaponData;
            _rigidbody.AddForce(direction * BULLET_SPEED);
        }

        public void ChangeLayer(int layer)
        {
            gameObject.layer = layer;
        }
    }
}