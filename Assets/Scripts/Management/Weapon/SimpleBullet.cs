using Management.Abstraction;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Data.Abstraction;
using Services.PoolSystem.Abstaction;
using UnityEngine;

namespace Management.Weapon
{
    public class SimpleBullet : MonoBehaviour, IBullet, IPoolable
    {
        private const float BULLET_SPEED = 80;
        public const string POOL_NAME = "SimpleBullet";
        
        [SerializeField] private Rigidbody2D _rigidbody;
        private Transform _transform;
        private IWeaponData _weaponData;
        private IPoolService _poolService;

        public string Name => POOL_NAME;

        public void Initialize()
        {
            _transform = transform;
            _poolService = ServiceHolder.ServiceProvider.GetService<IPoolService>();
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
        }

        public void OnReleaseToPool()
        {

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
    }
}