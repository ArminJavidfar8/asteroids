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

        public string Name => POOL_NAME;

        public void Initialize()
        {
            IWeaponService weaponService = ServiceHolder.ServiceProvider.GetService<IWeaponService>();
            _spaceshipService = ServiceHolder.ServiceProvider.GetService<ISpaceshipService>();
            _eventService = ServiceHolder.ServiceProvider.GetService<IEventService>();
            _coroutineService = ServiceHolder.ServiceProvider.GetService<ICoroutineService>();
            _spaceshipController = GetComponent<ISpaceshipController>();
            _spaceshipController.Initialize(_spaceshipData.ForwardMotorPower, _spaceshipData.RotationPower);

            _weapon = weaponService.GetWeapon(WeaponType.Pistol);
            _damageable.Setup(this, _spaceshipData.MaxHealth);
        }

        public void OnGetFromPool()
        {
            _spaceshipController.OnGetFromPool();
            _coroutineService.StartDelayedTask(0.5f, MoveProperly);
        }

        public void OnReleaseToPool()
        {
            _spaceshipController.OnReleaseToPool();
        }

        public void MoveProperly()
        {
            Vector3 randomNoise = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), 0);
            Vector3 direction = (-transform.position + randomNoise) * 20f;
            _spaceshipController.Move(direction);
        }

        public void OnDamageableDied(IDamageable damageable)
        {
            _eventService.BroadcastEvent(EventTypes.OnEnemySpaceshipDestoyed);
            _eventService.BroadcastEvent(EventTypes.OnEnemyDied, _spaceshipData.KillingScore);
            _spaceshipService.RemoveEnemy(gameObject);
        }
    }
}