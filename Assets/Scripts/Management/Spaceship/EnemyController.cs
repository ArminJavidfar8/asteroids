using Common;
using Common.Extensions;
using Management.Abstraction;
using Management.Common;
using Management.Core;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.Abstraction.Spaceship;
using Services.CoroutineSystem.Abstractio;
using Services.Data;
using Services.Data.Abstraction;
using Services.EventSystem.Abstraction;
using Services.EventSystem.Extension;
using Services.PoolSystem.Abstaction;
using System;
using System.Collections;
using UnityEngine;

namespace Management.Spaceship
{
    [RequireComponent(typeof(ISpaceshipController))]
    public class EnemyController : MonoBehaviour, IPoolable, IDamageableDeathListener
    {
        public const string POOL_NAME = "EnemySpaceship";

        [SerializeField] private SpaceshipData _spaceshipData;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SimpleDamageable _damageable;
        private ICoroutineService _coroutineService;
        private ISpaceshipService _spaceshipService;
        private IEventService _eventService;
        private ISpaceshipController _spaceshipController;
        private IWeapon _weapon;
        private Coroutine _shootRoutine;
        private Transform _transform;

        public string Name => POOL_NAME;

        public void Initialize(IServiceProvider serviceProvider)
        {
            IWeaponService weaponService = serviceProvider.GetService<IWeaponService>();
            _spaceshipService = serviceProvider.GetService<ISpaceshipService>();
            _eventService = serviceProvider.GetService<IEventService>();
            _coroutineService = serviceProvider.GetService<ICoroutineService>();
            _spaceshipController = GetComponent<ISpaceshipController>();
            _spaceshipController.Initialize(_spaceshipData.ForwardMotorPower, _spaceshipData.RotationPower);
            _transform = transform;

            _weapon = weaponService.GetWeapon(WeaponType.Pistol, LayerMask.NameToLayer(Constants.LayerNames.ENEMY_BULLET));
        }

        public void OnGetFromPool()
        {
            _damageable.Setup(this, _spaceshipData.MaxHealth);
            _spaceshipController.OnGetFromPool();
            _coroutineService.StartDelayedTask(0.5f, MoveProperly);
            _shootRoutine = _coroutineService.StartCoroutine(ShootRoutine());
        }

        public void OnReleaseToPool()
        {
            _spaceshipController.OnReleaseToPool();
            if (_shootRoutine != null)
            {
                _coroutineService.StopCoroutine(_shootRoutine);
            }
        }

        public void MoveProperly()
        {
            Vector3 randomNoise = Utility.GetRandomVector2(-2, 2);
            Vector3 direction = (-_transform.position + randomNoise) * 20f;
            _spaceshipController.Move(direction);
        }

        public void OnDamageableDied(IDamageable damageable)
        {
            _eventService.BroadcastEvent(EventTypes.OnEnemySpaceshipDestoyed);
            _eventService.BroadcastEvent(EventTypes.OnDied, _spaceshipData.KillingScore);
            _spaceshipService.RemoveEnemy(gameObject);
        }

        private IEnumerator ShootRoutine()
        {
            while (_damageable.IsAlive)
            {
                yield return new WaitForSeconds(1.2f);
                Vector3 randomNoise = Utility.GetRandomVector2(-2, 2);
                Vector3 bulletDirection = (_spaceshipService.Player.Position - _transform.position + randomNoise).normalized;
                _weapon.Shoot(_transform.position, bulletDirection);
            }
        }

    }
}